using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowManager : MonoBehaviour
{
    public PlayerOverworldController player;
    public List<GameObject> followers;

    void Awake()
    {
        for (int i = 0; i < followers.Count; i++)
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

    public void AddFollowers(GameObject gameobject)
    {
        followers.Add(gameobject);
        followers[followers.Count].GetComponent<FollowPlayer>().SetPreviousObject(followers[followers.Count - 1]);
    }
}
