using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatEntityMovement : MonoBehaviour
{
    [Header("references")]
    [SerializeField] Astar _pathfinder;

    [Header("Parameters")]
    [SerializeField] float _speed;

    TileAstarNode _currentNode;


    UniTask _currentMovementTask;


    private void Start()
    {
        _currentNode = Graph.Instance.Nodes[transform.position.RoundToV2IntXZ()];
    }


    public async UniTask MoveToPoint(Vector3 from,Vector3 to, float duration, bool smoothstep) 
    {
        

        from.y = to.y = transform.position.y;

        float StartTime = Time.time;
        float EndTime = Time.time + duration;
        while (Time.time < EndTime)
        {
            float alpha = Mathf.InverseLerp(StartTime, EndTime, Time.time);
            if(smoothstep) alpha = Mathf.SmoothStep(0,1,alpha);
            transform.position = Vector3.Lerp(from, to, alpha);
            await UniTask.Yield();
            Debug.Log("moving");
        }
        transform.position = to;
        
    }


    public async UniTask GoTo(Vector2Int p)
    {
        Graph.Instance.ActivateNode(_currentNode);
        Stack<AstarNode> path = _pathfinder.ComputePath(_currentNode, Graph.Instance.Nodes[p]);
        while(path.Count>0)
        {
            TileAstarNode targetNode = (TileAstarNode)path.Pop(); 
            await MoveToPoint((_currentNode).MonoBehaviour.transform.position,((TileAstarNode)targetNode).MonoBehaviour.transform.position, 1f/ _speed,false);
            _currentNode = targetNode;
        }
        Graph.Instance.DisableNode(_currentNode);
    }


    private void OnDrawGizmos()
    {

        if(_currentNode != null && _currentNode.MonoBehaviour!=null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_currentNode.MonoBehaviour.transform.position, .5f);
        }
        
    }
}
