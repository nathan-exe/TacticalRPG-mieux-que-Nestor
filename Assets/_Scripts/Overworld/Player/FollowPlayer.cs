using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public PlayerOverworldController player;
    public float distanceBetween = 2f;

    private GameObject previousObject;

    void Start()
    {
        if (previousObject == null)
        {
            previousObject = player.gameObject;
        }
    }

    void Update()
    {
        var B = gameObject.transform.position;
        var A = previousObject.transform.position;
        var AB = B - A;
        AB = Vector3.ClampMagnitude(AB, distanceBetween);
        B = A + AB;
        gameObject.transform.position = B;
    }

    public void SetPreviousObject(GameObject obj)
    {
        previousObject = obj;
    }
}
