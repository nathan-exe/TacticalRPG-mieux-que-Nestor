using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomlyDieOnAwake : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    float DestroyChance = .1f;
    // Start is called before the first frame update
    void Awake()
    {
        if(Random.value<=DestroyChance)Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
