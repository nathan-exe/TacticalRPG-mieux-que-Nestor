using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _monsterPrefab;
    [SerializeField] private List<Transform> _potentialSpawn = new();
    [SerializeField] private CombatManager _combatManager;

    private void Awake()
    {
       foreach (string name in MonsterData.Instance.ListOfMonsterName)
        {
            EntityData foundMonster = MonsterData.Instance.ScriptableMonsters.Find(entity => entity.Name == name);

            GameObject newMonstre = Instantiate(_monsterPrefab); //nouveau monstre
            newMonstre.GetComponent<AiCombatEntity>().Data = foundMonster;//attribution de son type

            newMonstre.GetComponentInChildren<CombatEntityUI>().Name.text = foundMonster.Name; //nom
            newMonstre.GetComponent<HealthComponent>().HP = foundMonster.MaxHP; //pv

            int pos = Random.Range(0, _potentialSpawn.Count); //positionne
            newMonstre.transform.position = _potentialSpawn[pos].position;
            _potentialSpawn.Remove(_potentialSpawn[pos]);

            _combatManager.Entities.Add(newMonstre.GetComponent<CombatEntity>());
        }
    }
}
