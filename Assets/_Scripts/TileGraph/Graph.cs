using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// ce MonoBehaviour contient tous les noeuds utilis�s par le A* pour le pathfinding sur la tilemap.
/// il y'a des noeuds sur absolument tout la zone du jeu,
/// mais ceux positionn�s sur des tiles solides de la tilemap sont d�sactiv�s.
/// </summary>
public class Graph : MonoBehaviour
{

    [SerializeField] GraphBuilder _graphBuilder;

    /// <summary>
    /// le dictionnaire qui contient tous les noeuds.
    /// la cl� est un vector2Int qui repr�sente la position du noeud dans le monde.
    /// </summary>
    public Dictionary<Vector2Int, TileAstarNode> Nodes = new();

    /// <summary>
    /// la liste de tous les noeuds libres, sans
    /// </summary>
    public List<TileAstarNode> FreeNodes { get; private set; } = new();

    public const string NodeLayer = "graph";


    //singleton
    private static Graph _instance;
    public static Graph Instance => _instance;

    private void Awake()
    {
        if(_instance != null && _instance!=this) Destroy(_instance.gameObject); 
        _instance = this;

        //g�n�ration du dictionnaire de noeuds
        Nodes.Clear();
        FreeNodes.Clear();
        _graphBuilder.BuildGraph();
    }

    //fonctions pratiques
    public void ActivateNodeAtPosition(Vector2Int position)
    {
        Nodes[position].monoBehaviour.gameObject.SetActive(true);
        if (!FreeNodes.Contains(Nodes[position]))FreeNodes.Add(Nodes[position]);
    }

    public bool HasNodeAtPosition(Vector2Int position)
    {
        return Nodes.ContainsKey(position) && Nodes[position].gameObject.activeSelf;
    }

    public void DisableNode(TileAstarNode node)
    {
        node.monoBehaviour.gameObject.SetActive(false);
        FreeNodes.Remove(node);
    }

    public void DisableNode(Vector2Int position)
    {
        if(Nodes.ContainsKey(position)) DisableNode (Nodes[position]);
    }

}
