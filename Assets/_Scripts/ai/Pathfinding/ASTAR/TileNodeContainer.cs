using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ce monobehaviour contient un TileAstarNode pour que les petits bonhommes puissent 
/// se déplacer sur la tilemap en utilisant l'algo Astar
/// </summary>
public class NodeContainer : MonoBehaviour
{
    public TileAstarNode node = new();
    public Vector2Int pose => transform.position.RoundToV2Int();

    private void Awake()
    {
        node.MonoBehaviour = this;
        node.resetNode();
    }

    //----------- Guizmos -----------

     private void OnDrawGizmosSelected()
     {
         Gizmos.color = Color.blue;
        foreach (TileAstarNode n in node.Neighbours.Cast<TileAstarNode>())
        {
             if(n.isActive())   Gizmos.DrawLine(transform.position, n.transform.position);
        }
     }

     private void OnDrawGizmos()
     {
        Gizmos.color = new Color(1, .5f, 1, .3f);
        Gizmos.color = Color.blue;
        foreach (TileAstarNode n in node.Neighbours.Cast<TileAstarNode>())
        {
            if (n.isActive()) Gizmos.DrawLine(transform.position, n.transform.position);
        }

     }
}
