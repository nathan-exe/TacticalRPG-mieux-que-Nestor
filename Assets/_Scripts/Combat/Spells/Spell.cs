using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[Serializable]
public /*abstract*/ class Spell
{
    public SpellData Data;
    List<Vector2Int> TargetableTiles;
    List<CombatEntity> HittableObjects;

    public int Orientation;

    /// <summary>
    /// trouve toutes les tiles du graph qui seront affectées par le sort.
    /// </summary>
    /// <param name="owner"></param>
    void RecomputeTargetableTiles(CombatEntity owner)
    {
        TargetableTiles.Clear();
        foreach (Vector2Int v in Data.AffectedTiles)
        {
            Vector2Int offset = v;
            offset = offset.rotate90(Orientation);
            Vector2Int TargetTile = owner.transform.position.RoundToV2Int() + offset;
            if (Graph.Instance.Bounds.Contains(TargetTile))
            {
                if (Data.IsOccludedByWalls)
                {
                    Vector3 TileworldPos = new Vector3(TargetTile.x, owner.transform.position.y, TargetTile.y);
                    Vector3 TileToOwner = owner.transform.position- TileworldPos;
                    Debug.DrawRay(TileworldPos, TileToOwner, Color.red,3);
                    if (!Physics.Raycast(TileworldPos, TileToOwner, TileToOwner.magnitude,LayerMask.GetMask("solid"))) 
                    {
                        TargetableTiles.Add(TargetTile);
                    }
                }
                else
                {
                    TargetableTiles.Add(TargetTile);
                }
            }
        } 

        foreach(Vector2Int targetTile in TargetableTiles)
        {
            Debug.DrawRay(new Vector3(targetTile.x, 0, targetTile.y), Vector3.up * 1, Color.red, 1);
        }
    }

    public void PreviewSpellEffect(CombatEntity owner)
    {
        RecomputeTargetableTiles(owner);
        Debug.Log("bidule");
        //preview tiles
        foreach (Vector2Int targetTile in TargetableTiles)
        {
            Graph.Instance.Nodes[targetTile].MonoBehaviour.SetState(CombatTile.State.dangerous);
        }

        //preview mana loss
        owner.UI.PreviewManaLoss(Data.ManaCost);

        //preview ennemi Loss
        GetAllHittableEntitiesOnTiles_NoAlloc(TargetableTiles, ref HittableObjects);
        foreach ( CombatEntity o in HittableObjects)
        {
            o.UI.PreviewHPLoss(Data.Damage);
        }
    }

    public void CancelPreview(CombatEntity owner)
    {
        owner.UI.CancelManaLossPreview();

        foreach (Vector2Int targetTile in TargetableTiles)
        {
            Graph.Instance.Nodes[targetTile].MonoBehaviour.SetState(CombatTile.State.empty);
        }
        TargetableTiles.Clear();

        foreach (CombatEntity o in HittableObjects)
        {
            o.UI.CancelHPLossPreview();
        }
        HittableObjects.Clear();
    }

    public async UniTask Execute(CombatEntity owner)
    {
        RecomputeTargetableTiles(owner);

        GetAllHittableEntitiesOnTiles_NoAlloc(TargetableTiles, ref HittableObjects);
        foreach (CombatEntity o in HittableObjects)
        {
            o.Health.TakeDamage(Data.Damage);
        }

    }

    private void GetAllHittableEntitiesOnTiles_NoAlloc(List<Vector2Int> tiles, ref List<CombatEntity> healthComponentsList)
    {
        healthComponentsList.Clear();
        foreach (Vector2Int targetTile in TargetableTiles)
        {
            Vector3 worldPos = new Vector3(targetTile.x, .5f, targetTile.y);
            if (Physics.SphereCast(new Vector3(worldPos.x, 50, worldPos.z), .4f, Vector3.down, out RaycastHit hit, 100))
            {
                if (hit.collider.TryGetComponent(out CombatEntity hitEntity))
                {
                    healthComponentsList.Add(hitEntity);
                }
            }
        }
    }

}

