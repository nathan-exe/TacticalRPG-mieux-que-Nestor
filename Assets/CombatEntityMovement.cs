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

    Stack<AstarNode> _path;

    UniTask _currentMovementTask;


    private void Start()
    {
        _currentNode = Graph.Instance.Nodes[transform.position.RoundToV2Int()];
        
        SetDestinationTile(new Vector3(Random.Range(-11,-11+24),0,Random.Range(-9,-9+20)));
    }


    public async UniTask MoveToPoint(Vector3 from,Vector3 to, float duration, bool smoothstep) 
    {
        float StartTime = Time.time;
        float EndTime = Time.time + duration;
        while (Time.time < EndTime)
        {
            float alpha = Mathf.InverseLerp(StartTime, EndTime, Time.time);
            if(smoothstep) alpha = Mathf.SmoothStep(0,1,alpha);
            transform.position = Vector3.Lerp(from, to, alpha);
            await UniTask.Yield();
        }
        transform.position = to;
    }


    public void SetDestinationTile(Vector3 tile) => SetDestinationTile(tile.RoundToV2Int());
    public void SetDestinationTile(Vector2Int tile) => SetDestinationTile(Graph.Instance.Nodes[tile]);
    public void SetDestinationTile(NodeContainer tile) => SetDestinationTile(tile.node);
    public void SetDestinationTile(AstarNode tile)
    {
        _path = _pathfinder.ComputePath(_currentNode, tile);
    }

    void FollowPath()
    {
        if (_currentMovementTask.Status != UniTaskStatus.Pending && _path.Count > 0)
        {
            TileAstarNode target = (TileAstarNode)_path.Pop();
            _currentMovementTask = MoveToPoint(_currentNode.MonoBehaviour.transform.position.setY(transform.position.y), target.MonoBehaviour.transform.position.setY(transform.position.y), 1f/ _speed, false);
            _currentNode = target;
        }
    }

    void HandleInputs() // @TODO à mettre dans un autre script
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hit)) // @TODO faire un singleton de la camera plutot que Camera.main
            {
                Debug.DrawRay(hit.point, Vector3.up, Color.magenta, 2);
                Vector2Int t = hit.point.RoundToV2Int();
                Debug.DrawRay(new Vector3(t.x,hit.point.y,t.y), Vector3.up, Color.red, 1);
                if (Graph.Instance.Nodes.ContainsKey(t)) SetDestinationTile(t);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        FollowPath();
        
    }
    private void OnDrawGizmos()
    {

        if (_path != null)
        {
            Gizmos.color = Color.white;
            AstarNode[] n = _path.ToArray();
            for (int i = 0; i < n.Length - 1; i++)
            {
                Debug.Log("salope");
                Gizmos.DrawWireCube(((TileAstarNode)n[i]).MonoBehaviour.transform.position, Vector3.one * .4f);
                //Gizmos.DrawLine(((TileAstarNode)n[i]).MonoBehaviour.transform.position+Vector3.up, ((TileAstarNode)n[i]).MonoBehaviour.transform.position+Vector3.up);
            }

        }

        if(_currentNode != null && _currentNode.MonoBehaviour!=null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_currentNode.MonoBehaviour.transform.position, .5f);
        }
        
    }
}
