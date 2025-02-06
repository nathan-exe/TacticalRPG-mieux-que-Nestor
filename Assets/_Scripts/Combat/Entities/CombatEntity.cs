using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatEntity : MonoBehaviour
{
    public const float MaxManaPerEntity = 10;

    [Header("References")]
    [SerializeField] public EntityData Data;
    [SerializeField] protected CombatEntityMovement _movement;
    public CombatEntityUI UI;
    public HealthComponent Health;
    public SpellCaster SpellCaster;
    protected FloodFill _floodFill;

    [Header("Values")]
    private bool _isDead;


    float _mana;
    public float Mana { get { return _mana; }
        set
        {
            _mana = value;
            UI.ManaSlider.Value = _mana;
        }
    }


    protected virtual void Awake()
    {
        TryGetComponent<FloodFill>(out _floodFill);
        _floodFill.MovementRange = Data.MovementRangePerTurn;
        Mana = MaxManaPerEntity;
    }

    private void Start()
    {
        Health.HP = Data.MaxHP;
    }

    public abstract UniTask PlayTurn();


}
