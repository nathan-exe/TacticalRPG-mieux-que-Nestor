using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pools
{
    Bullet
}

public class PoolManager : MonoBehaviour
{
    //singleton
    private static PoolManager instance;

    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Pool Manager");
                instance = go.AddComponent<PoolManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }



    [Header("VFXs")]
    public Pool VfxHitPool, VFXDeathPool,VfxHealPool;


}
