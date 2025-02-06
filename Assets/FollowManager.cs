using UnityEngine;

public class FollowManager : MonoBehaviour
{
    public PlayerOverworldController player;
    public GameObject[] followers;

    void Awake()
    {
        for (int i = 0; i < followers.Length; i++)
        {
            if (i == 0)
            {
                followers[i].GetComponent<FollowPlayer>().player = player;
            }
            else
            {
                followers[i].GetComponent<FollowPlayer>().SetPreviousObject(followers[i - 1]);
            }
        }
    }
}
