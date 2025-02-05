using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySpawn : MonoBehaviour
{
    [SerializeField]
    private PartyData _partyData;
    [SerializeField] private GameObject _partyPrefab;
    [SerializeField] private List<Transform> _potentialSpawn = new();
    [SerializeField] private CombatManager _combatManager;
    private void Awake()
    {
        _partyData = PartyData.Instance;
        if (_partyData.TeamState.Count == 0 ) { return; }
        foreach(CharacterState Teammate in _partyData.TeamState)
        {
            GameObject newTeammate = Instantiate(_partyPrefab);
            int pos = Random.Range(0, _potentialSpawn.Count);
            newTeammate.transform.position = _potentialSpawn[pos].position;
            _potentialSpawn.Remove(_potentialSpawn[pos]);
            _combatManager.Entities.Add(newTeammate.GetComponent<CombatEntity>());
        }
        _combatManager.Play();
    }
}
