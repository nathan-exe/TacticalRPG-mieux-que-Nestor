using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiCombatEntity : CombatEntity
{
    public static List<AiCombatEntity> Instances = new List<AiCombatEntity>();
    protected override void Awake()
    {
        base.Awake();
        Instances.Add(this);
        //Health.HP = Data.MaxHP;
    }
    private void OnDestroy()
    {
        Instances.Remove(this);
    }


    public override async UniTask PlayTurn()
    {
        //compute and show floodFill
        Vector2Int originTile = _floodFill.GetOriginTile();

        _floodFill.UpdateTileHighlighting(originTile);

        //pick random spell
        SpellData chosenSpell = Data.Spells.PickRandom(); //@ToDo : take mana intoAccount

        //pick random target unit
        Vector2Int SpellTargetTile = PlayerCombatEntity.Instances.PickRandom().transform.position.RoundToV2Int();

        //choose best orientation
        int Orientation = ChooseClosestDirectionTowardPosition(new Vector3(SpellTargetTile.x, 0, SpellTargetTile.y));


        //choose SpellCast origin
        Vector2Int? TargetTile = null;
        foreach(Vector2Int Offset in chosenSpell.AffectedTiles) //pour toutes les cases rouges
        {
            Offset.rotate90(Orientation);//@ToDo : fix orientation

            Vector2Int castOrigin = SpellTargetTile - Offset;
            if (_floodFill.HighlightedTiles.Contains(Graph.Instance.Nodes[castOrigin]))
            {
                if (chosenSpell.IsOccludedByWalls)
                {
                    //raycast
                    Vector3 TileToCastOriginWS = (castOrigin - SpellTargetTile).X0Y();
                    Debug.DrawRay(SpellTargetTile.X0Y() + Vector3.up * transform.position.y, TileToCastOriginWS, Color.red, .1f);
                    if (!Physics.Raycast(SpellTargetTile.X0Y() + transform.position.y * Vector3.up, TileToCastOriginWS, TileToCastOriginWS.magnitude, LayerMask.GetMask("solid")))
                    {
                        TargetTile = castOrigin;
                        break;
                    }
                }
                else
                {
                    TargetTile = castOrigin;
                    break;
                }
            }
        }
        if(TargetTile ==null) TargetTile = _floodFill.HighlightedTiles.ToList().PickRandom().pose; //affreux

        await UniTask.Delay(1000);

        _floodFill.ResetTilesHighlighting();

        await _movement.GoTo(TargetTile.Value);

        SpellCaster.Orientation = Orientation;
        SpellCaster.SelectedSpellData = chosenSpell;
        await UniTask.Delay(1000);

        await SpellCaster.CastSelectedSpell();//@ToDo : cast spell only if tile wasnt picked at Random
    }

    int ChooseClosestDirectionTowardPosition(Vector3 target)//@ToDo : fix orientation
    {
        float bestAngle = 181;
        byte bestIndex = 0;
        for (byte i = 0; i < 3; i++)
        {
            Vector3 offset = target - transform.position;
            float angle = Vector3.Angle(offset, VectorExtensions.All4Directions[i]);
            if (angle < bestAngle)
            {
                bestAngle = angle;
                bestIndex = i;
            }
        }
        return bestIndex;
    }



    

    async UniTask ChooseSpell()
    {
        _floodFill.ResetTilesHighlighting();
        await CombatUI.Instance.SpellSelectionPanel.SelectEntitySpell(this);
    }
}
