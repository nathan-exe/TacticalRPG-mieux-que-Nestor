using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float HP { get; private set; }

    [Header("References")]
    [SerializeField] private EntityData _data;

    public void Init()
    {
        
    }
    public async UniTask PlayTurn()
    {

    }
    
}
