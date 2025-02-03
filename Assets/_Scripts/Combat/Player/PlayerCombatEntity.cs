using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerCombatEntity : CombatEntity
{

    [SerializeField] CombatEntityMovement _movement;
    FloodFill _floodFill;

    private void Awake()
    {
        TryGetComponent<FloodFill>(out _floodFill);
    }

    public override async UniTask PlayTurn()
    {
        Vector2Int t = await ChooseDestination();
        await _movement.GoTo(t);

        Spell ChosenSpell = await ChooseSpell();
        await CastSpell(ChosenSpell);

    }

    async UniTask<Vector2Int> ChooseDestination()
    {
        bool waiting = true;

        Vector2Int output = Vector2Int.zero;

        Vector2Int originTile = _floodFill.GetOriginTile();
        _floodFill.UpdateTileHighlighting(originTile);

        //@ToDo
        //PreviewTiles();

        while (waiting)
        {
            await UniTask.Yield();
            Debug.Log("waiting for click");
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) // @TODO faire un singleton de la camera plutot que Camera.main
                {
                    Vector2Int tilePos = hit.point.RoundToV2Int();
                    Graph.Instance.Nodes.TryGetValue(tilePos, out TileAstarNode tile); //Comme TileAstarNode n'est pas un mono impossible de GetComponnent
                    if (tile != null && _floodFill.HighlightedTiles.Contains(tile)) //Si on clique sur une tile blanche :
                    {
                        Debug.DrawRay(hit.point, Vector3.up, Color.magenta, 2);
                        Vector2Int t = hit.point.RoundToV2Int();
                        Debug.DrawRay(new Vector3(t.x, hit.point.y, t.y), Vector3.up, Color.red, 1);
                        if (Graph.Instance.Nodes.ContainsKey(t))
                        {
                            waiting = false;
                            output = t;
                        }
                    }

                }
            }

        }

        return output;

    }
    async UniTask<Spell> ChooseSpell()
    {
        _floodFill.ResetTilesHighlighting();
        return await CombatUI.Instance.SpellSelectionPanel.SelectEntitySpell(this);
    }


}
