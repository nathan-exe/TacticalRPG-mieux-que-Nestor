using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ce monobehaviour contient un TileAstarNode pour que les petits bonhommes puissent 
/// se déplacer sur la tilemap en utilisant l'algo Astar
/// </summary>
public class CombatTile : MonoBehaviour
{
    public enum State { empty,clickable,dangerous};

    public TileAstarNode node = new();
    public Vector2Int pose => transform.position.RoundToV2Int();

    public State CurrentState { get; private set; }

    [Header("Asset References")]
    [SerializeField] CombatTileData _data;
    
    [Header("Scene References")]
    [SerializeField] MeshRenderer _renderer;

    private void Awake()
    {
        //Astar setup
        node.MonoBehaviour = this;
        node.resetNode();
    }

    public void SetState(State newState)
    {
        switch (newState)
        {
            case State.empty:
                _renderer.material = _data.Material_Default;
                break;

            case State.clickable:
                _renderer.material = _data.Material_Clickable;
                break;

            case State.dangerous:
                _renderer.material = _data.Material_Danger;
                break;

        }
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
