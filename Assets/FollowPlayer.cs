using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public PlayerOverworldController player;
    public float distanceBetween = 2f;
    public float followSpeed = 5f;

    private GameObject previousObject;
    private NavMeshAgent navAgent;
    private bool isFollowing = true;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        if (previousObject == null)
        {
            previousObject = player.gameObject;
        }
    }

    void Update()
    {
        if (player.HasComeToDestination)
        {
            isFollowing = false;
        }
        else
        {
            isFollowing = true;
        }

        if (isFollowing)
        {
            Vector3 targetPosition = previousObject.transform.position - previousObject.transform.forward * distanceBetween;
            navAgent.speed = 10f;
            navAgent.SetDestination(targetPosition);
        }
        else
        {
            navAgent.ResetPath();
        }
    }

    public void SetPreviousObject(GameObject obj)
    {
        previousObject = obj;
    }
}
