using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PartySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _partyPrefab;
    [SerializeField] private List<Transform> _potentialSpawn = new();
    [SerializeField] private CombatManager _combatManager;

    private void Awake()
    {
        foreach (CharacterState Teammate in GameState.TeamState)
        {
            GameObject newTeammate = Instantiate(_partyPrefab); //nouveau pote
            newTeammate.GetComponent<PlayerCombatEntity>().Data = Teammate.EntityData;

            newTeammate.GetComponentInChildren<CombatEntityUI>().Name.text = Teammate.EntityData.name; //nom
            Debug.Log($"Les pv sont de: {Teammate.HP}");
            newTeammate.GetComponent<HealthComponent>().MaxHP = Teammate.EntityData.MaxHP; //pv
            newTeammate.GetComponent<HealthComponent>().HP = Teammate.HP; //pv

            int pos = Random.Range(0, _potentialSpawn.Count); //positionne
            newTeammate.transform.position = _potentialSpawn[pos].position;
            _potentialSpawn.Remove(_potentialSpawn[pos]);

            _combatManager.Entities.Add(newTeammate.GetComponent<CombatEntity>());
        }
    }
}
