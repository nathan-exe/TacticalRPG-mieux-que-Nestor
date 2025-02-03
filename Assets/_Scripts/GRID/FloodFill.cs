using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    [SerializeField] private int mouvementRange = 3;
    [SerializeField] private int spellRange = 5;
    [SerializeField] private Material spellMaterial, playerMaterial, resetMaterial;
    [SerializeField] private Transform playerTransform;
    [Tooltip("D�termine si la souris est l'origine de la zone")] [SerializeField] private bool useMouseOrigin = true;

    private HashSet<TileAstarNode> highlightedTiles = new(), lockedTiles = new();
    private Vector2Int lastOriginTile = new(int.MinValue, int.MinValue); //�l�ment de comparaison pour savoir si la tile d'origine a boug�.

    void Update() //@ToDo : Enlever �a de Update au secours
    {
        Vector2Int originTile = GetOriginTile(); //D�termine la tile � l'origine de floodfill

        if (originTile != lastOriginTile) //Si les tiles d'origines sont diff�rents alors c'est que la souris ou le joueur � boug�s donc il faut modifier l'affichage
        {
            lastOriginTile = originTile;
            UpdateTileHighlighting(originTile);
        }

        if (Input.GetMouseButtonDown(0) && useMouseOrigin) //pour un sort
            StartCoroutine(LockTilesTemporarily(1f));
    }

    /// <summary>
    /// D�termine la position de l'origine du floodfill (soit la souris pour le sort, soit l'entit�).
    /// </summary>
    private Vector2Int GetOriginTile()
    {
        if (!useMouseOrigin) return playerTransform.position.RoundToV2Int(); //Si c'est le joueur le centre alors sa tile est l'origine.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) ? hit.point.RoundToV2Int() : lastOriginTile;
    }

    /// <summary>
    /// Met � jour l'affichage des tuiles en fonction de la port�e du sort.
    /// </summary>
    private void UpdateTileHighlighting(Vector2Int originTile)
    {
        if (!Graph.Instance.Nodes.TryGetValue(originTile, out TileAstarNode startNode)) return;

        Material activeMaterial = useMouseOrigin ? spellMaterial : playerMaterial;
        HashSet<TileAstarNode> newHighlight = new(GetReachableTiles(startNode, spellRange));

        //------------  R�initialise les anciennes tuiles non verrouill�es--------------------------
        foreach (var tile in highlightedTiles)
            if (!newHighlight.Contains(tile) && !lockedTiles.Contains(tile))
                tile.MonoBehaviour.GetComponentInChildren<Renderer>().material = resetMaterial;

        //------------Applique le mat�riau actif aux nouvelles tuiles------------------------------
        foreach (var tile in newHighlight)
            if (!lockedTiles.Contains(tile))
                tile.MonoBehaviour.GetComponentInChildren<Renderer>().material = activeMaterial;

        //-----------------------------------------------------------------------------------------
        highlightedTiles = newHighlight;
    }

    /// <summary>
    /// Garde temporairement les tuiles avant de les r�initialiser (utilis� par les spells).
    /// </summary>
    private IEnumerator LockTilesTemporarily(float duration)
    {
        lockedTiles = new HashSet<TileAstarNode>(highlightedTiles);
        yield return new WaitForSeconds(duration); //attends

        //------------  R�initialise les anciennes tuiles non verrouill�es--------------------------
        foreach (var tile in lockedTiles)
            if (!highlightedTiles.Contains(tile))
                tile.MonoBehaviour.GetComponentInChildren<Renderer>().material = resetMaterial;
        //------------------------------------------------------------------------------------------

        lockedTiles.Clear();
    }

    /// <summary>
    /// Retourne les tuiles accessibles dans un certain rayon en utilisant une recherche en largeur (BFS).
    /// </summary>
    private List<TileAstarNode> GetReachableTiles(TileAstarNode startNode, int range) //On part d'un node de d�part avec une range donn� pour regard� les voisins
    {
        if (!useMouseOrigin) { range = mouvementRange; } //On ne veux pas forc�ment la meme value entre mouvement et spell

        List<TileAstarNode> result = new();
        Queue<(TileAstarNode, int)> queue = new();
        HashSet<TileAstarNode> visited = new() { startNode };

        queue.Enqueue((startNode, 0));

        while (queue.Count > 0)
        {
            var (node, distance) = queue.Dequeue();
            result.Add(node);

            if (distance < range)
                foreach (var neighbor in node.Neighbours) //Comme pour un Astar on regarde les voisins sauf que TOUT les voisin (en haut, bas, droite, gauche) sont ajout�s
                    if (neighbor is TileAstarNode tile && !visited.Contains(tile) && tile.isActive() /*on �vite les murs*/)
                    {
                        queue.Enqueue((tile, distance + 1));
                        visited.Add(tile);
                    }
        }
        return result;
    }
}
