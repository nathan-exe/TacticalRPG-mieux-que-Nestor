using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatEntity : MonoBehaviour
{
    public const float MaxManaPerEntity = 10;

    [Header("References")]
    [SerializeField] public EntityData Data;
    public CombatEntityUI UI;
    public HealthComponent Health;

    [Header("Values")]
    private bool _isDead;

    float _HP;
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
        private set
        {
            _mana = value;
            UI.ManaSlider.Value = _mana;
        }
    }

    


    void Start()
    {
        HP = Data.MaxHP;
        Mana = MaxManaPerEntity;
    }


    public abstract UniTask PlayTurn();

    protected async UniTask CastSpell(Spell spell)
    {
        Mana-=spell.Data.ManaCost;
        await spell.Execute(this);
    }

}
