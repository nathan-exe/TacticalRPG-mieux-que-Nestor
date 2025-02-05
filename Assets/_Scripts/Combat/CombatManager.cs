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
    }

    void EndFight()
    {
        _isPlaying = false;
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
