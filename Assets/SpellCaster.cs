using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions;

public class SpellCaster : MonoBehaviour
{
    [Header("Asset References")]
    private SpellData _selectedSpellData;
    public SpellData SelectedSpellData
    {
        get { return _selectedSpellData; }
        set { 
            _selectedSpellData = value;
            if (value != null) PreviewSpellEffect(); else CancelPreview();
        }
    }

    [Header("Scene References")]
    public CombatEntity Owner;

    List<Vector2Int> TargetableTiles = new();
    List<CombatEntity> HittableObjects = new();

    private int _orientation;
    public int Orientation 
    { 
        get { return _orientation; } 
        set {  _orientation = value; if (SelectedSpellData != null) { CancelPreview(); PreviewSpellEffect(); } }
    }

    /// <summary>
    /// trouve toutes les tiles du graph qui seront affectées par le sort.
    /// </summary>
    /// <param name="owner"></param>
   
    void RecomputeTargetableTiles()
    {
        TargetableTiles.Clear();
        foreach (Vector2Int v in SelectedSpellData.AffectedTiles)
        {
            //rotation du sort
            Vector2Int offset = v;
            offset = offset.rotate90(Orientation);
            Vector2Int TargetTile = transform.position.RoundToV2Int() + offset;

            //empty tile check
            if (Graph.Instance.Bounds.Contains(TargetTile))
            {

                if (SelectedSpellData.IsOccludedByWalls)
                {
                    //wall collision check
                    Vector3 TileworldPos = new Vector3(TargetTile.x, transform.position.y, TargetTile.y);
                    Vector3 TileToOwner = transform.position - TileworldPos;
                    Debug.DrawRay(TileworldPos, TileToOwner, Color.red, .1f);
                    if (!Physics.Raycast(TileworldPos, TileToOwner, TileToOwner.magnitude, LayerMask.GetMask("solid")))
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
    }

    /// <summary>
    /// preview les effets du sort
    /// </summary>
    public void PreviewSpellEffect()
    {

        //preview tiles
        RecomputeTargetableTiles();
        foreach (Vector2Int targetTile in TargetableTiles)
        {
            Graph.Instance.Nodes[targetTile].MonoBehaviour.SetState(CombatTile.State.dangerous);
        }

        //preview mana loss
        Owner.UI.PreviewManaLoss(SelectedSpellData.ManaCost);

        //preview ennemi HP Loss
        GetAllHittableEntitiesOnTiles_NoAlloc(TargetableTiles, ref HittableObjects);
        foreach (CombatEntity o in HittableObjects)
        {
            o.UI.PreviewHPLoss(SelectedSpellData.Damage);
        }
    }

    /// <summary>
    /// annule la preview
    /// </summary>
    public void CancelPreview()
    {
        Owner.UI.CancelManaLossPreview();

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

    /// <summary>
    /// execute le spell selectionné
    /// </summary>
    /// <returns></returns>
    public async UniTask CastSelectedSpell()
    {
        Assert.IsNotNull(SelectedSpellData,"y'a pas de spell à lancer là...");

        Owner.Mana -= SelectedSpellData.ManaCost;

        RecomputeTargetableTiles();

        GetAllHittableEntitiesOnTiles_NoAlloc(TargetableTiles, ref HittableObjects);
        foreach (CombatEntity o in HittableObjects)
        {
            o.Health.TakeDamage(SelectedSpellData.Damage);
        }

        SelectedSpellData = null;

    }

    /// <summary>
    /// trouve toues les entités sur la liste de tiles donnée
    /// </summary>
    /// <param name="tiles"></param>
    /// <param name="healthComponentsList"></param>
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
