using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// ce Monobehaviour permet de générer le graph de pathfinding rapidement dans Unity.
/// </summary>
public class GraphBuilder : MonoBehaviour
{
    [SerializeField] Vector2Int _size = new Vector2Int(10, 10);
    [SerializeField] Vector2Int _offset = new Vector2Int(10, 10);
    [SerializeField] Graph _graph;
    [SerializeField] NodeContainer _nodePrefab;

    public const string solidLayer = "Solid";
    public void BuildGraph()
    {
        _graph.Nodes.Clear();

        //detruit tous les noeuds précédents
        int c = transform.childCount;
        for(int i =0; i < c; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        //itere sur toutes les tiles de la zone de jeu définie
        for (int x = _offset.x; x < _size.x + _offset.x; x++)
        {
            for (int y= _offset.y; y < _size.y+ _offset.y; y++)
            {
                bool collision = Physics2D.OverlapPoint(new Vector2(x, y), LayerMask.GetMask(solidLayer)); //le node sera desactivé si il y'avait un objet sur la case avant qu'il ne spawn

                NodeContainer newObject = Instantiate(_nodePrefab, new Vector2(x, y), Quaternion.identity, transform);
                TileAstarNode newNode = newObject.node;
                newNode.monoBehaviour = newObject;

                newNode.monoBehaviour.gameObject.name = $"Node({x},{y})";
                _graph.Nodes.Add(new Vector2Int(x, y), newNode);
                if(!collision) _graph.FreeNodes.Add(newNode);

                newNode.monoBehaviour.gameObject.SetActive(!collision);
            }
        }

        //link nodes with eachother
        foreach (KeyValuePair<Vector2Int,TileAstarNode> pair in _graph.Nodes)
        {
            Vector2Int pose = pair.Key+ Vector2Int.up;
            if(_graph.Nodes.ContainsKey(pose)) pair.Value.Neighbours.Add(_graph.Nodes[pose]);

            pose = pair.Key + Vector2Int.down;
            if (_graph.Nodes.ContainsKey(pose)) pair.Value.Neighbours.Add(_graph.Nodes[pose]);

            pose = pair.Key + Vector2Int.right;
            if (_graph.Nodes.ContainsKey(pose)) pair.Value.Neighbours.Add(_graph.Nodes[pose]);

            pose = pair.Key + Vector2Int.left;
            if (_graph.Nodes.ContainsKey(pose)) pair.Value.Neighbours.Add(_graph.Nodes[pose]);
        }

    }

    //gizmos
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector3)(Vector2)(_offset+_size/2- Vector2.one*0.5f), (Vector3)(Vector2)(_size));
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(GraphBuilder))]
public class GraphBuilder_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Rebuild Graph"))
        {
            ((GraphBuilder)target).BuildGraph();
        }
    }
}

#endif