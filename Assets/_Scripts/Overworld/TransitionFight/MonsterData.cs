using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterData : MonoBehaviour
{
    public List<string> ListOfMonsterName = new();
    public List<EntityData> ScriptableMonsters = new();
    public static MonsterData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
