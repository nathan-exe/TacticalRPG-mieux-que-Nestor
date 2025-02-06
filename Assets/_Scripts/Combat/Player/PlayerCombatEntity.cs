using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class PlayerCombatEntity : CombatEntity
{
    public static List<PlayerCombatEntity> Instances = new List<PlayerCombatEntity>();

    protected override void Awake()
    {
        base.Awake();
        Instances.Add(this);
    }

    private void OnDestroy()
    {
        Instances.Remove(this);
    }

    public override async UniTask PlayTurn()
    {
        Vector2Int t = await ChooseDestination();
        await _movement.GoTo(t);

        await ChooseSpell();
        if(SpellCaster.SelectedSpellData!=null) await SpellCaster.CastSelectedSpell() ;
    }

    async UniTask<Vector2Int> ChooseDestination()
    {
        //set up
        bool waiting = true;
        Vector2Int output = Vector2Int.zero;
        TileAstarNode SelectedTile = null;

        //floodfill
        Vector2Int originTile = _floodFill.GetOriginTile();
         _floodFill.UpdateTileHighlighting(originTile);

        
        while (waiting) //wait for the player to choose a tile
        {
            await UniTask.Yield();

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, ~LayerMask.GetMask("solid"))) // @TODO faire un singleton de la camera plutot que Camera.main
            {
                Vector2Int tilePos = hit.point.RoundToV2Int();
                Graph.Instance.Nodes.TryGetValue(tilePos, out TileAstarNode HitTile); //Comme TileAstarNode n'est pas un mono impossible de GetComponnent
                
                //Si on clique sur une tile blanche :
                if (HitTile != null && _floodFill.HighlightedTiles.Contains(HitTile)) 
                {

                    //Tile Mouse events
                    if(HitTile!= SelectedTile)
                    {
                        if (SelectedTile != null) SelectedTile.MonoBehaviour.OnMouseLeave();
                        HitTile.MonoBehaviour.OnMouseHover();
                        SelectedTile = HitTile;
                    }

                    //OnClick :
                    if (Input.GetMouseButtonDown(0))
                    {
                        waiting = false;
                        output = tilePos;
                    }

                }
            }
            

        }
        _floodFill.ResetTilesHighlighting();
        return output;

    }
    async UniTask ChooseSpell()
    {
        
        await CombatUI.Instance.SpellSelectionPanel.SelectEntitySpell(this);
    }


}
