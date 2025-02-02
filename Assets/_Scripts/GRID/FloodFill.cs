using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    [SerializeField] private int mouvementRange = 3;
    [SerializeField] private int spellRange = 5;
    [SerializeField] private Material spellMaterial, playerMaterial, resetMaterial;
    [SerializeField] private Transform playerTransform;
    [Tooltip("Détermine si la souris est l'origine de la zone")] [SerializeField] private bool useMouseOrigin = true;

    private HashSet<TileAstarNode> highlightedTiles = new(), lockedTiles = new();
    private Vector2Int lastOriginTile = new(int.MinValue, int.MinValue);

    void Update()
    {
        Vector2Int originTile = GetOriginTile();
        if (originTile != lastOriginTile)
        {
            lastOriginTile = originTile;
            UpdateTileHighlighting(originTile);
        }

        if (Input.GetMouseButtonDown(0) && useMouseOrigin)
            StartCoroutine(LockTilesTemporarily(1f));
    }

    /// <summary>
    /// Détermine la position de l'origine du floodfill (soit la souris pour le sort, soit l'entité).
    /// </summary>
    private Vector2Int GetOriginTile()
    {
        if (!useMouseOrigin) return playerTransform.position.RoundToV2Int();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) ?/*if*/ hit.point.RoundToV2Int() :/*else*/ lastOriginTile; // return Physics.Raycast(ray, out RaycastHit hit) si hit.point.RoundToV2Int()
    }

    /// <summary>
    /// Met à jour l'affichage des tuiles en fonction de la portée du sort.
    /// </summary>
    private void UpdateTileHighlighting(Vector2Int originTile)
    {
        if (!Graph.Instance.Nodes.TryGetValue(originTile, out TileAstarNode startNode)) return;

        Material activeMaterial = useMouseOrigin ?/*if*/ spellMaterial :/*else*/ playerMaterial;
        HashSet<TileAstarNode> newHighlight = new(GetReachableTiles(startNode, spellRange));
        // Réinitialise les anciennes tuiles non verrouillées
        foreach (var tile in highlightedTiles)
            if (!newHighlight.Contains(tile) && !lockedTiles.Contains(tile))
                tile.MonoBehaviour.GetComponentInChildren<Renderer>().material = resetMaterial;

        // Applique le matériau actif aux nouvelles tuiles
        foreach (var tile in newHighlight)
            if (!lockedTiles.Contains(tile))
                tile.MonoBehaviour.GetComponentInChildren<Renderer>().material = activeMaterial;

        highlightedTiles = newHighlight;
    }

    /// <summary>
    /// Garde temporairement les tuiles avant de les réinitialiser (utilisé par les spells).
    /// </summary>
    private IEnumerator LockTilesTemporarily(float duration)
    {
        lockedTiles = new HashSet<TileAstarNode>(highlightedTiles);
        yield return new WaitForSeconds(duration);

        foreach (var tile in lockedTiles)
            if (!highlightedTiles.Contains(tile))
                tile.MonoBehaviour.GetComponentInChildren<Renderer>().material = resetMaterial;

        lockedTiles.Clear();
    }

    /// <summary>
    /// Retourne les tuiles accessibles dans un certain rayon en utilisant une recherche en largeur (BFS).
    /// </summary>
    private List<TileAstarNode> GetReachableTiles(TileAstarNode startNode, int range) //On part d'un node de départ avec une range donné pour regardé les voisins
    {
        if (!useMouseOrigin) { range = mouvementRange; }
        List<TileAstarNode> result = new();
        Queue<(TileAstarNode, int)> queue = new();
        HashSet<TileAstarNode> visited = new() { startNode };

        queue.Enqueue((startNode, 0));

        while (queue.Count > 0)
        {
            var (node, distance) = queue.Dequeue();
            result.Add(node);

            if (distance < range)
                foreach (var neighbor in node.Neighbours) //Comme pour un Astar on regarde les voisins sauf que TOUT les voisin (en haut, bas, droite, gauche) sont ajoutés
                    if (neighbor is TileAstarNode tile && !visited.Contains(tile) && tile.isActive())
                    {
                        queue.Enqueue((tile, distance + 1));
                        visited.Add(tile);
                    }
        }
        return result;
    }
}
