using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public List<Transform> wayPoint;

    [SerializeField]
    NavMeshAgent navMeshAgent;

    [SerializeField]
    Rigidbody rb;

    public int currentWaypointIndex = 0;

    [SerializeField]
    private Transform player;

    public bool SeePlayer;
    public bool HasTouchPlayer;

    public float speed;
    public float speedRun;

    void Update()
    {
        if (HasTouchPlayer)
        {
            navMeshAgent.isStopped = true;
            return;
        }

        if (SeePlayer == false)
        {
            rb.velocity = Vector3.zero;
            Walking();
        }
        else
        {
            FollowPlayer();
        }
    }

    private void Walking()
    {
        if (wayPoint.Count == 0)
        {

            return;
        }


        float distanceToWaypoint = Vector3.Distance(wayPoint[currentWaypointIndex].position, transform.position);

        if (distanceToWaypoint <= 2)
        {

            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoint.Count;
        }

        navMeshAgent.SetDestination(wayPoint[currentWaypointIndex].position);
        navMeshAgent.speed = speed;
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        navMeshAgent.speed = speedRun; 
        navMeshAgent.SetDestination(player.transform.position);
    }
}
