using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

/// <summary>
/// ce monobehaviour sert à gérer les points de vie d'une unité.
/// </summary>
public class HealthComponent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] bool _destroyGameObjectOnDeath;

    [Header("Scene References")]
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
            if(HP==0)OnDeath?.Invoke(this.gameObject);
        }
    }

    //notifiers
    public event Action<float> OnHealthUpdated;
    public event Action<GameObject> OnDeath;
    public event Action OnDamageTaken;

    /// <summary>
    /// fait des dégats au joueur.
    /// </summary>
    /// <param name="damage"></param>
    public async UniTask TakeDamage(float damage)
    {
        OnDamageTaken?.Invoke();
        HP = HP - Mathf.Abs(damage);

        //feedbacks async
        await _entity.Visuals.shake();
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
        if (_destroyGameObjectOnDeath) OnDeath += (GameObject EntityDeath) =>
        {
            if (TryGetComponent<PooledObject>(out PooledObject asPooledObject)) asPooledObject.GoBackIntoPool();
            else Destroy(gameObject);
        };

        
    }

    private void Start()
    {
        MaxHP = _entity.Data.MaxHP;
        //HP = MaxHP;
        OnDamageTaken += ()=> PoolManager.Instance.VfxHitPool.PullObjectFromPool(transform.position);
        OnDeath+= (GameObject o) => PoolManager.Instance.VFXDeathPool.PullObjectFromPool(transform.position);
    }
}
