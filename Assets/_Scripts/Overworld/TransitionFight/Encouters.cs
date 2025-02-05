using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Encouters : MonoBehaviour
{
    [SerializeField] private Animation _anim;
    [SerializeField] private int _nomberMonster;
    [SerializeField] private GameObject _enemyPrefab;
    private void Start()
    {
        PartyData.Instance.AddCharacter(new CharacterState("Nestor", 10, 100));
        PartyData.Instance.AddCharacter(new CharacterState("Matéo", 5, 50));
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerOverworldController>() != null)
        {
            _anim.Play();
            PartyData.Instance.DisplayTeam();
            await Task.Delay(600);
            SceneManager.LoadScene("Scene Matéo");
        }
    }
}
