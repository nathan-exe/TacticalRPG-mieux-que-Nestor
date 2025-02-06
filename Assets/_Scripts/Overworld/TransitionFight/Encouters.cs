using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Encouters : MonoBehaviour
{
    [SerializeField] private Animation _anim;
    [SerializeField] private List<string> _monsters = new();

    private void Start()
    {
        GameStat.AddCharacter(new CharacterState(10, 100, "Matéo"));
        GameStat.AddCharacter(new CharacterState(5, 50, "Nestor"));
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerOverworldController>() != null)
        {
            _anim.Play();
            GameStat.DisplayTeam();
            foreach (var monster in _monsters)
            {
                MonsterData.Instance.ListOfMonsterName.Add(monster);
            }
            await Task.Delay(600);
            SceneManager.LoadScene("SceneMatéo");
        }
    }
}
