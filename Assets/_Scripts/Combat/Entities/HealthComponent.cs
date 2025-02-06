using System;
using UnityEngine;

/// <summary>
/// ce monobehaviour sert à gérer les points de vie d'une unité.
/// </summary>
public class HealthComponent : MonoBehaviour
{

    [SerializeField] bool _destroyGameObjectOnDeath;
    [SerializeField] CombatEntity _entity;

    public float MaxHP = 100;

    //HP getters et setters
    public float _HP;
    public float HP
    {
        get { return _HP; }
        set
        {
            _HP = Mathf.Clamp(value, 0, MaxHP);
            OnHealthUpdated?.Invoke(_HP);
            if(HP==0)OnDeath?.Invoke();
        }
    }

    //notifiers
    public event Action<float> OnHealthUpdated;
    public event Action OnDeath;
    public event Action OnDamageTaken;

    /// <summary>
    /// fait des dégats au joueur.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        OnDamageTaken?.Invoke();
        HP = HP - Mathf.Abs(damage);
    }

    /// <summary>
    /// soigne le joueur.
    /// </summary>
    /// <param name="heal"></param>
    public void Heal(int heal)
    {
        HP = HP + Mathf.Abs(heal);
    }


    private void Awake()
    {
        if(_entity ==null) TryGetComponent(out _entity);

        //Destroy object on death
        if (_destroyGameObjectOnDeath) OnDeath += () =>
        {
            if (TryGetComponent<PooledObject>(out PooledObject asPooledObject)) asPooledObject.GoBackIntoPool();
            else Destroy(gameObject);
        };

        MaxHP = _entity.Data.MaxHP;
        HP = MaxHP;
    }

}
