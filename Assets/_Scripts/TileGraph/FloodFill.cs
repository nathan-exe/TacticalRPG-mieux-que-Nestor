using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    [SerializeField] private int _mouvementRange = 3;
    [SerializeField] private int _spellRange = 5;
    //[SerializeField] private Material _spellMaterial, _playerMaterial, _resetMaterial;
    [SerializeField] private Transform _playerTransform;
    [Tooltip("Détermine si la souris est l'origine de la zone")][SerializeField] public bool UseMouseOrigin = true;

    public HashSet<TileAstarNode> HighlightedTiles { get; private set; } = new();
    private HashSet<TileAstarNode> _lockedTiles = new();
    private Vector2Int _lastOriginTile = new(int.MinValue, int.MinValue); //élément de comparaison pour savoir si la tile d'origine a bougé.  ll

    void Update() //@ToDo : Enlever ça de Update au secours
    {
        if (Input.GetMouseButtonDown(0) && UseMouseOrigin) //pour un sort
            StartCoroutine(LockTilesTemporarily(1f));
    }

    /// <summary>
    /// Détermine la position de l'origine du floodfill (soit la souris pour le sort, soit l'entité).
    /// </summary>
    public Vector2Int GetOriginTile()
    {
        if (!UseMouseOrigin) return _playerTransform.position.RoundToV2Int(); //Si c'est le joueur le centre alors sa tile est l'origine.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) ? hit.point.RoundToV2Int() : _lastOriginTile;
    }

    /// <summary>
    /// Met à jour l'affichage des tuiles en fonction de la portée du sort.
    /// </summary>
    public void UpdateTileHighlighting(Vector2Int originTile)
    {
        if (!Graph.Instance.Nodes.TryGetValue(originTile, out TileAstarNode startNode)) return;

        //Material activeMaterial = UseMouseOrigin ? _spellMaterial : _playerMaterial;
        HashSet<TileAstarNode> newHighlight = new(GetReachableTiles(startNode, _spellRange));

        //------------  Réinitialise les anciennes tuiles --------------------------
        foreach (var tile in HighlightedTiles)
            if (!newHighlight.Contains(tile) && !_lockedTiles.Contains(tile))
                tile.MonoBehaviour.SetState(CombatTile.State.empty) ;

        //------------Applique le matériau actif aux nouvelles tuiles------------------------------
        foreach (var tile in newHighlight)
            if (!_lockedTiles.Contains(tile))
                tile.MonoBehaviour.SetState(CombatTile.State.clickable);

        //-----------------------------------------------------------------------------------------
        HighlightedTiles = newHighlight;
    }

    /// <summary>
    /// Garde temporairement les tuiles avant de les réinitialiser (utilisé par les spells).
    /// </summary>
    public IEnumerator LockTilesTemporarily(float duration)
    {
        _lockedTiles = new HashSet<TileAstarNode>(HighlightedTiles);
        yield return new WaitForSeconds(duration); //attends

        //------------  Réinitialise les anciennes tuiles non verrouillées--------------------------
        foreach (var tile in _lockedTiles)
            if (!HighlightedTiles.Contains(tile))
                tile.MonoBehaviour.SetState(CombatTile.State.empty);
        //------------------------------------------------------------------------------------------

        _lockedTiles.Clear();
    }

    #region ResetFunction
    /// <summary>
    /// Réinitialise les tuiles après un temps donné.
    /// </summary>
    public void ResetTilesHighlighting()
    {
        foreach (var tile in HighlightedTiles)
            tile.MonoBehaviour.SetState(CombatTile.State.empty);
        HighlightedTiles.Clear();
    }

    #endregion

    /// <summary>
    /// Retourne les tuiles accessibles dans un certain rayon en utilisant une recherche en largeur (BFS).
    /// </summary>
    public List<TileAstarNode> GetReachableTiles(TileAstarNode startNode, int range) //On part d'un node de départ avec une range donné pour regardé les voisins
    {
        if (!UseMouseOrigin) { range = _mouvementRange; } //On ne veux pas forcément la meme value entre mouvement et spell

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
                    if (neighbor is TileAstarNode tile && !visited.Contains(tile) && tile.isActive() /*on évite les murs*/)
                    {
                        queue.Enqueue((tile, distance + 1));
                        visited.Add(tile);
                    }
        }
        return result;
    }

    public void CheckElement()
    {

    }
}
