using DG.Tweening;
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
    public Vector2Int pose => transform.position.RoundToV2IntXZ();

    public State CurrentState { get; private set; }

    [Header("Asset References")]
    [SerializeField] CombatTileData _data;
    
    [Header("Scene References")]
    [SerializeField] MeshRenderer _renderer;

    //visuals
    Vector3 _rendererBaseScale;
    const float TweeningDuration = 0.25f;

    private void Awake()
    {
        //Astar setup
        node.MonoBehaviour = this;
        node.resetNode();
        _rendererBaseScale = _renderer.transform.localScale;

    }

    private void Start()
    {
        _renderer.transform.localScale = Vector3.zero;
        SetState(State.empty);

    }

    public void SetState(State newState)
    {
        switch (newState)
        {
            case State.empty:
                _renderer.transform.DOScale(Vector3.zero, TweeningDuration);
                break;

            case State.clickable:
                _renderer.material = _data.Material_Clickable;
                _renderer.transform.DOScale(_rendererBaseScale, TweeningDuration);
                break;

            case State.dangerous:
                _renderer.material = _data.Material_Danger;
                _renderer.transform.DOScale(_rendererBaseScale, TweeningDuration);
                break;

        }
    }

    public void OnMouseHover()
    {
        _renderer.transform.DOScale(_rendererBaseScale*1.5f, TweeningDuration);
    }

    public void OnMouseLeave()
    {
        Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHH");
        _renderer.transform.DOScale(_rendererBaseScale, TweeningDuration);
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

     /*private void OnDrawGizmos()
     {
        Gizmos.color = new Color(1, .5f, 1, .3f);
        Gizmos.color = Color.blue;
        foreach (TileAstarNode n in node.Neighbours.Cast<TileAstarNode>())
        {
            if (n.isActive()) Gizmos.DrawLine(transform.position, n.transform.position);
        }

     }*/
}
