using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

    protected override void Start()
    {
        base.Start();
        Health.OnDamageTaken += () => PostProcessController.instance.E_ExposureFlash.play();
        Health.OnDamageTaken += () => PostProcessController.instance.E_ScreenDistortion.play();
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

        //pick closest target unit
        Vector2Int TargetedUnitPosition = FindClosestEnemyEntity().transform.position.RoundToV2IntXZ();//PlayerCombatEntity.Instances.PickRandom().transform.position.RoundToV2Int();

        //choose best orientation
        int Orientation = ChooseClosestDirectionTowardPosition(new Vector3(TargetedUnitPosition.x, 0, TargetedUnitPosition.y));


        //choose SpellCast origin
        Vector2Int? TargetTile = null;
        
        foreach(Vector2Int o in chosenSpell.AffectedTiles) //pour toutes les cases rouges
        {
            Vector2Int Offset = o;

            Offset = Offset.rotate90(Orientation);//@ToDo : fix orientation
            //Offset *= -1;
            //Offset *= -1;

            Vector2Int castOrigin = TargetedUnitPosition - Offset;
            Debug.DrawRay(castOrigin.X0Y() + transform.position.y *Vector3.up, Vector3.up, Color.red, .1f);

            if (Graph.Instance.Nodes.TryGetValue(castOrigin, out TileAstarNode node) && _floodFill.HighlightedTiles.Contains(node))
            {
                if (chosenSpell.IsOccludedByWalls)
                {
                    //raycast
                    Vector3 TileToCastOriginWS = (castOrigin - TargetedUnitPosition).X0Y();
                    Debug.DrawRay(TargetedUnitPosition.X0Y() + Vector3.up * transform.position.y, Vector3.up, Color.red, .1f);
                    Debug.DrawRay(TargetedUnitPosition.X0Y() + Vector3.up * transform.position.y, TileToCastOriginWS, Color.grey, .1f);
                    
                    if (!Physics.Raycast(TargetedUnitPosition.X0Y() + transform.position.y * Vector3.up, TileToCastOriginWS, TileToCastOriginWS.magnitude, LayerMask.GetMask("solid")))
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
        bool foundTile =TargetTile != null;
        if (!foundTile) TargetTile = FindClosestTileToPositionInList(_floodFill.HighlightedTiles, TargetedUnitPosition).pose;//_floodFill.HighlightedTiles.ToList().PickRandom().pose; //affreux
        
        //EditorApplication.isPaused = true;
        await UniTask.Delay(1000);

        _floodFill.ResetTilesHighlighting();

        await _movement.GoTo(TargetTile.Value);

        if (foundTile)
        {
            SpellCaster.Orientation = Orientation;
            SpellCaster.SelectedSpellData = chosenSpell;
            await UniTask.Delay(1000);
            await SpellCaster.CastSelectedSpell();//@ToDo : cast spell only if tile wasnt picked at Random
        }else await UniTask.Delay(1000);

    }

    int ChooseClosestDirectionTowardPosition(Vector3 target)//@ToDo : fix orientation
    {
        float bestAngle = 181;
        byte bestIndex = 0;
        Vector3 offset = target - transform.position;
        Debug.DrawRay(transform.position, offset, Color.white);
        for (byte i = 0; i < 4; i++)
        {
            
            float angle = Vector3.Angle(offset.normalized*5, VectorExtensions.All4Directions[i]);
            Debug.DrawRay(transform.position, VectorExtensions.All4Directions[i]*5,Color.blue);
            if (angle < bestAngle)
            {
                bestAngle = angle;
                bestIndex = i;
            }
        }

        Debug.DrawRay(transform.position, VectorExtensions.All4Directions[bestIndex]*5, Color.red);
        Debug.Log("Direction choisie : " + VectorExtensions.All4Directions[bestIndex]);
        //EditorApplication.isPaused = true;
        return bestIndex;
    }

    async UniTask ChooseSpell()
    {
        _floodFill.ResetTilesHighlighting();
        await CombatUI.Instance.SpellSelectionPanel.SelectEntitySpell(this);
    }

    /// <summary>
    /// retourne le noeud le plus proche de la position donnée parmi la liste de noeuds.
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    private TileAstarNode FindClosestTileToPositionInList(HashSet<TileAstarNode> tiles, Vector2Int targetPosition)
    {
        float minDistanceSqrd = Mathf.Infinity;
        TileAstarNode clostestTile = null;

        foreach (TileAstarNode tile in tiles)
        {
            float distanceSqrd = tile.pose.SqrDistanceTo(targetPosition);
            if (clostestTile == null || distanceSqrd < minDistanceSqrd)
            {
                minDistanceSqrd = distanceSqrd;
                clostestTile = tile;
            }
        }

        return clostestTile;
    }


    /// <summary>
    /// retourne l'entité ennemie la plus proche
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    private PlayerCombatEntity FindClosestEnemyEntity()
    {
        float minDistanceSqrd = Mathf.Infinity;
        PlayerCombatEntity clostestEntity = null;

        foreach (PlayerCombatEntity entity in PlayerCombatEntity.Instances)
        {
            float distanceSqrd = entity.transform.position.SqrDistanceTo(transform.position);
            if (clostestEntity == null || distanceSqrd < minDistanceSqrd)
            {
                minDistanceSqrd = distanceSqrd;
                clostestEntity = entity;
            }
        }

        return clostestEntity;
    }
}
