using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public List<CombatEntity> Entities { get; private set; } = new List<CombatEntity>(); 

    int _currentEntityIndex = 0;

    bool _isPlaying = false;

    [SerializeField] CameraBehaviour _camera;

    public static CombatManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        if (_camera == null) _camera = FindObjectOfType<CameraBehaviour>();
    }

    private IEnumerator Start()
    {
        yield return 1;
        if (Entities.Count > 0) Play();
    }

    /// <summary>
    /// retire du jeu toutes les entitées mortes.
    /// </summary>
    void FindAndRemoveCorpses()
    {
        List<CombatEntity> deadEntities = new();
        foreach (CombatEntity entity in Entities) if (entity.Health.HP <= 0) deadEntities.Add(entity);
        foreach (CombatEntity entity in deadEntities) HandleEntityDeath(entity.gameObject);
    }

    /// <summary>
    /// gère la mort d'une entité
    /// </summary>
    /// <param name="EntityDeath"></param>
    /// <param name="DeadEntities"></param>
    void HandleEntityDeath(GameObject EntityDeath) //Fonction qui supprime les morts du combats, PROBLEME DE TOURS LORS D UN REMOVE DE LA LISTE
    {

        if (EntityDeath.GetComponent<PlayerCombatEntity>()) //Pour les joueurs
        {
            PlayerCombatEntity.Instances.Remove(EntityDeath.GetComponent<PlayerCombatEntity>());
            Entities.Remove(EntityDeath.GetComponent<PlayerCombatEntity>());
            EntityDeath.SetActive(false);
            print("Player mort");

            //GameOver
            if (PlayerCombatEntity.Instances.Count == 0)
            {
                _isPlaying = false;
                TimeManager.instance.StopTime(.5f);
                UiManager.Instance.ShowPanel(UiManager.Instance.GameOverPanel);
            }
        }
        if (EntityDeath.GetComponent<AiCombatEntity>()) // Pour les ennemis
        {
            AiCombatEntity.Instances.Remove(EntityDeath.GetComponent<AiCombatEntity>());
            Entities.Remove(EntityDeath.GetComponent<AiCombatEntity>());
            EntityDeath.SetActive(false);
            print("Méchant mort");
            if (AiCombatEntity.Instances.Count == 0) //win
            {
                foreach (CharacterState Teammate in GameState.TeamState)
                {
                    foreach (PlayerCombatEntity Entity in PlayerCombatEntity.Instances)
                    {
                        if (Entity.GetComponentInChildren<CombatEntityUI>().Name.text == Teammate.EntityData.name)
                        {
                            print(Teammate.EntityData.name);
                            print(Entity.GetComponent<HealthComponent>().HP);
                            Teammate.HP = Entity.GetComponent<HealthComponent>().HP;
                            //@SAVEMAN SaveManager.Instance.SaveCurrentGameState(SaveManager.Instance.CurrentFileName);
                        }
                    }
                }
                _isPlaying = false;
                CompleteEncounter(GameState.ZoneName);
                TimeManager.instance.StopTime(.5f);
                UiManager.Instance.ShowPanel(UiManager.Instance.WinPanel);
            }
        }

    }

    public void CompleteEncounter(string encounterID)
    {
        if (GameState.EncountersDico.ContainsKey(encounterID)) { GameState.EncountersDico[encounterID] = true; } // Marquer la zone comme faite
    }

    /// <summary>
    /// boucle de jeu principale
    /// </summary>
    /// <returns></returns>
    public async UniTask Play()
    {
        _isPlaying = true;
        while(_isPlaying)
        {
            _camera.StartFollowingTarget(Entities[_currentEntityIndex].transform);
            await Entities[_currentEntityIndex].PlayTurn();

            FindAndRemoveCorpses();

            _currentEntityIndex++;
            _currentEntityIndex = _currentEntityIndex%Entities.Count;
        }
    }
}
