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

    /// <summary>
    /// trouve toutes les tiles du graph qui seront affectées par le sort.
    /// </summary>
    /// <param name="owner"></param>
    void RecomputeTargetableTiles(CombatEntity owner)
    {
        TargetableTiles.Clear();
        foreach (Vector2Int offset in Data.AffectedTiles)
        {
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

        //preview tiles
        foreach (Vector2Int targetTile in TargetableTiles)
        {
            Graph.Instance.Nodes[targetTile].MonoBehaviour.SetState(CombatTile.State.dangerous);
        }

        //preview mana loss
        owner.UI.PreviewManaLoss(Data.ManaCost);

        //@ToDo : preview ennemi HP loss
    }

    public void CancelPreview(CombatEntity owner)
    {
        owner.UI.CancelManaLossPreview();

        foreach (Vector2Int targetTile in TargetableTiles)
        {
            Graph.Instance.Nodes[targetTile].MonoBehaviour.SetState(CombatTile.State.empty);
        }
    }

    public async UniTask Execute(CombatEntity owner)
    {
        //@ToDo : appliquer degats sur toutes les tiles
            
        //bool collision = Physics.SphereCast(new Vector3(worldPos.x, 50, worldPos.z), .4f, Vector3.down, out RaycastHit hit, 100); //le node sera desactivé si il y'avait un objet sur la case avant qu'il ne spawn

    }
}
