using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float distanceBetween = 2f;
    public float followSpeed = 5f;

    private GameObject previousObject;
    private bool isFollowing = true;

    void Start()
    {
        if (previousObject == null)
        {
            previousObject = player;
        }
    }

    void Update()
    {
        if (player.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
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
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
            transform.rotation = Quaternion.LookRotation(previousObject.transform.forward);
        }
    }

    public void SetPreviousObject(GameObject obj)
    {
        previousObject = obj;
    }
}
