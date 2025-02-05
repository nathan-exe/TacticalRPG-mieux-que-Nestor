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

    float _HP;

    //notifiers

    public float HP
    {
        get { return _HP; }
        private set
        {
            _HP = value;
            UI.HealthSlider.Value = _HP;
        }
    }

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
        HP = Data.MaxHP;
        Mana = MaxManaPerEntity;
    }

    public abstract UniTask PlayTurn();


}
