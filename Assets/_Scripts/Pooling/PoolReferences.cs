using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReferences : MonoBehaviour
{
    public Pool BombPool;

    //singleton
    private static PoolReferences _instance;
    public static PoolReferences Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance!=this) Destroy(this);
        _instance = this;
    }

}
