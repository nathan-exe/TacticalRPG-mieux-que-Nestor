using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    [Header("Scene references")]
    public List<CombatEntity> Entities = new List<CombatEntity>();
<<<<<<< Updated upstream
=======
    [SerializeField] CameraBehaviour _camera;

>>>>>>> Stashed changes
    int _currentEntityIndex = 0;

    bool _isPlaying = false;

    void EndFight()
    {
        _isPlaying = false;
    }

<<<<<<< Updated upstream

    async UniTask Play()
=======
    void RemoveList(GameObject EntityDeath) //Fonction qui supprime les morts du combats, @todo PROBLEME DE TOURS LORS D UN REMOVE DE LA LISTE
    {
        if (EntityDeath.GetComponent<PlayerCombatEntity>()) //Pour les joueurs
        {
            PlayerCombatEntity.Instances.Remove(EntityDeath.GetComponent<PlayerCombatEntity>());
            Entities.Remove(EntityDeath.GetComponent<PlayerCombatEntity>());
            EntityDeath.SetActive(false);
            print("Player mort");

            if (PlayerCombatEntity.Instances.Count == 0)
            {
                print("GameOver");
            }
        }
        if (EntityDeath.GetComponent<AiCombatEntity>()) // Pour les ennemis
        {
            AiCombatEntity.Instances.Remove(EntityDeath.GetComponent<AiCombatEntity>());
            Entities.Remove(EntityDeath.GetComponent<AiCombatEntity>());
            EntityDeath.SetActive(false);
            print("Méchant mort");

            if (AiCombatEntity.Instances.Count == 0)
            {
                CompleteEncounter(GameStat.ZoneName);
                print("Victoire");
                SceneManager.LoadScene("SceneOverMatéo");
            }
        }
    }

    public void CompleteEncounter(string encounterID)
    {
        if (GameStat.EncountersDico.ContainsKey(encounterID)) { GameStat.EncountersDico[encounterID] = true; } // Marquer la zone comme faite
    }

    /// <summary>
    /// boucle de jeu principale
    /// </summary>
    /// <returns></returns>
    public async UniTask Play()
>>>>>>> Stashed changes
    {
        _isPlaying = true;
        while(_isPlaying)
        {
            _camera.StartFollowingTarget(Entities[_currentEntityIndex].transform);
            await Entities[_currentEntityIndex].PlayTurn();
            

            _currentEntityIndex++;
            _currentEntityIndex = _currentEntityIndex%Entities.Count;
        }
    }

    private void Start()
    {
        Play();
    }
}
