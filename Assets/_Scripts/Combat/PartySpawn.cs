using System.Collections.Generic;
using UnityEngine;

public class PartySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _partyPrefab;
    [SerializeField] private List<Transform> _potentialSpawn = new();
    [SerializeField] private CombatManager _combatManager;

    private void Awake()
    {
        if (PartyData.Instance == null ) { _combatManager.Play(); return; } //au cas ou...

        foreach(CharacterState Teammate in PartyData.Instance.TeamState)
        {
            GameObject newTeammate = Instantiate(_partyPrefab); //nouveau pote

            newTeammate.GetComponentInChildren<CombatEntityUI>().Name.text = Teammate.Name; //nom
            newTeammate.GetComponent<HealthComponent>().MaxHP = Teammate.HP; //pv

            int pos = Random.Range(0, _potentialSpawn.Count); //positionne
            newTeammate.transform.position = _potentialSpawn[pos].position;
            _potentialSpawn.Remove(_potentialSpawn[pos]);

            _combatManager.Entities.Add(newTeammate.GetComponent<CombatEntity>());
        }
    }
}
