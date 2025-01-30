using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatEntity : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EntityData _data;

    public Spell Spells;

    private bool _isDead;
    public float HP { get; private set; }

    public void Init()
    {
        
    }
    public abstract UniTask PlayTurn();


}
