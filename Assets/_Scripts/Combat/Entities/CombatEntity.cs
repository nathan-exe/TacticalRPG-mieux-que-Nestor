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
    [SerializeField] public EntityVisuals Visuals;

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
            _mana = Mathf.Clamp(value, 0, MaxManaPerEntity);
            UI.ManaSlider.Value = _mana;
        }
    }


    protected virtual void Awake()
    {
        if (_floodFill == null) TryGetComponent(out _floodFill);
        if(Visuals==null) TryGetComponent(out Visuals);

        
        Mana = MaxManaPerEntity;
    }

    protected virtual void Start()
    {
        _floodFill.MovementRange = Data.MovementRangePerTurn;
    }

    public abstract UniTask PlayTurn();


}
