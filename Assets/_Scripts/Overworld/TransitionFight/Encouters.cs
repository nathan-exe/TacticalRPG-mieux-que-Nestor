using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script qui g�re les rencontres face aux monstres
/// </summary>
public class Encouters : MonoBehaviour
{
    [SerializeField] private Animation _anim; //L'animation fight
    [SerializeField] private List<string> _monsters = new(); //Liste des montres � crois�


    BoxCollider _collider;
    private BoxCollider Collider { get { if (_collider == null) TryGetComponent(out _collider); return _collider; } set => _collider = value; }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerOverworldController>() != null)
        {
            _anim.Play();
            GameState.DisplayTeam(); //Debug de qui est dans la team

            string ZoneName = gameObject.name;

            foreach (var monster in _monsters) //On se pr�pare � faire spawn les monstres dans la prochaine scene
            {
                MonsterData.Instance.ListOfMonsterName.Add(monster);
            }

            GameState.SetTeamPosition(other.transform.position); //Save la position du joueur
            print(other.transform.position);

            if (!GameState.EncountersDico.ContainsKey(ZoneName)) //On regarde si dans le dico qui r�portorie toutes les zones de combats si notre zone existe d�j�
            {
                GameState.SetZoneName(ZoneName);
                GameState.EncountersDico.Add(ZoneName, false); // false = la zone n'a pas �t� r�alis�e
            }

            PostProcessController.instance.FadeOut.play();
            await UniTask.Delay(600);
            SceneManager.LoadScene(2);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(.95f,.7f,.7f,.75f);
        Gizmos.DrawCube((transform.TransformPoint(Collider.center)), transform.localToWorldMatrix * Collider.size);
    }
}
