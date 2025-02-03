using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// ce Monobehaviour permet de générer le graph de pathfinding rapidement dans Unity.
/// </summary>
public class GraphBuilder : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] LayerMask _solidLayerMask;
    

    [Header("Scene References")]
    [SerializeField] Graph _graph;

    [Header("Asset References")]
    [SerializeField] CombatTile _nodePrefab;

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
        for (int x = _graph.Bounds.min.x; x < _graph.Bounds.max.x ; x++)
        {
            for (int z= _graph.Bounds.min.y ; z < _graph.Bounds.max.y; z++)
            {
                bool collision = Physics.SphereCast(new Vector3(x,50,z),.4f,Vector3.down,out RaycastHit hit,100, _solidLayerMask); //le node sera desactivé si il y'avait un objet sur la case avant qu'il ne spawn

                CombatTile newObject = PrefabUtility.InstantiatePrefab(_nodePrefab, transform).GetComponent<CombatTile>();
                newObject.transform.position = new Vector3(x, hit.point.y, z);
                TileAstarNode newNode = newObject.node;
                newNode.MonoBehaviour = newObject;

                newNode.MonoBehaviour.gameObject.name = $"Node({x},{z})";
                _graph.Nodes.Add(new Vector2Int(x, z), newNode);
                if(!collision) _graph.FreeNodes.Add(newNode);

                newNode.MonoBehaviour.gameObject.SetActive(!collision);
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