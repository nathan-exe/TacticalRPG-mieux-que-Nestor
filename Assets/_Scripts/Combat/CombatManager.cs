using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<CombatEntity> Entities = new List<CombatEntity>();

    int _currentEntityIndex = 0;

    bool _isPlaying = false;

    private void Start()
    {
        if (Entities.Count > 0) Play();
        foreach (CombatEntity entity in Entities)
        {
            entity.Health.OnDeath += RemoveList;
        }
    }

    void EndFight()
    {
        _isPlaying = false;
    }

    void RemoveList(GameObject EntityDeath)
    {
        if (EntityDeath.GetComponent<PlayerCombatEntity>())
        {
            PlayerCombatEntity.Instances.Remove(EntityDeath.GetComponent<PlayerCombatEntity>());
            print("Player mort");

            if (PlayerCombatEntity.Instances.Count == 0)
            {
                print("GameOver");
            }
        }
        if (EntityDeath.GetComponent<AiCombatEntity>())
        {
            AiCombatEntity.Instances.Remove(EntityDeath.GetComponent<AiCombatEntity>());
            print("Méchant mort");

            if (AiCombatEntity.Instances.Count == 0)
            {
                print("Victoire");
            }
        }
    }

    public async UniTask Play()
    {
        _isPlaying = true;
        while(_isPlaying)
        {
            await Entities[_currentEntityIndex].PlayTurn();
            _currentEntityIndex++;
            _currentEntityIndex = _currentEntityIndex%Entities.Count;
        }
    }
}
