using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script qui gère les rencontres face aux monstres
/// </summary>
public class Encouters : MonoBehaviour
{
    [SerializeField] private Animation _anim; //L'animation fight
    [SerializeField] private List<string> _monsters = new(); //Liste des montres à croisé

    private async void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerOverworldController>() != null)
        {
            _anim.Play();
            GameStat.DisplayTeam(); //Debug de qui est dans la team

            string ZoneName = gameObject.name;

            foreach (var monster in _monsters) //On se prépare à faire spawn les monstres dans la prochaine scene
            {
                MonsterData.Instance.ListOfMonsterName.Add(monster);
            }

            GameStat.SetTeamPosition(other.transform.position); //Save la position du joueur
            print(other.transform.position);

            if (!GameStat.EncountersDico.ContainsKey(ZoneName)) //On regarde si dans le dico qui réportorie toutes les zones de combats si notre zone existe déjà
            {
                GameStat.SetZoneName(ZoneName);
                GameStat.EncountersDico.Add(ZoneName, false); // false = la zone n'a pas été réalisée
            }

            PostProcessController.instance.FadeOut.play();
            await UniTask.Delay(600);
            SceneManager.LoadScene(2);
        }
    }
}
