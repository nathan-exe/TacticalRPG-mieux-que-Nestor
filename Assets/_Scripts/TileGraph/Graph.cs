using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// ce MonoBehaviour contient tous les noeuds utilisés par le A* pour le pathfinding sur la tilemap.
/// il y'a des noeuds sur absolument tout la zone du jeu,
/// mais ceux positionnés sur des tiles solides de la tilemap sont désactivés.
/// </summary>
public class Graph : MonoBehaviour
{

    [SerializeField] GraphBuilder _graphBuilder;

    /// <summary>
    /// le dictionnaire qui contient tous les noeuds.
    /// la clé est un vector2Int qui représente la position du noeud dans le monde.
    /// </summary>
    public Dictionary<Vector2Int, TileAstarNode> Nodes = new();

    /// <summary>
    /// la liste de tous les noeuds libres
    /// </summary>
    public List<TileAstarNode> FreeNodes { get; private set; } = new();

    public const string NodeLayer = "graph";

    public RectInt Bounds;

    //singleton
    private static Graph _instance;
    public static Graph Instance => _instance;

    private void Awake()
    {
        if(_instance != null && _instance!=this) Destroy(_instance.gameObject); 
        _instance = this;

        //génération du dictionnaire de noeuds
        Nodes.Clear();
        FreeNodes.Clear();
        _graphBuilder.BuildGraph();
    }

    //fonctions pratiques
    public void ActivateNodeAtPosition(Vector2Int position)
    {
        Nodes[position].MonoBehaviour.gameObject.SetActive(true);
        if (!FreeNodes.Contains(Nodes[position]))FreeNodes.Add(Nodes[position]);
    }

    public bool HasNodeAtPosition(Vector2Int position)
    {
        return Nodes.ContainsKey(position) && Nodes[position].gameObject.activeSelf;
    }

    public void DisableNode(TileAstarNode node)
    {
        node.MonoBehaviour.gameObject.SetActive(false);
        FreeNodes.Remove(node);
    }

    public void DisableNode(Vector2Int position)
    {
        if(Nodes.ContainsKey(position)) DisableNode (Nodes[position]);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Bounds.center.X0Y(), Bounds.size.X0Y());
    }
}
