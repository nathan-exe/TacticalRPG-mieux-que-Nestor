using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        if ((GameState.TeamState.Count == 0))
        {
            GameState.AddCharacter(new CharacterState("Nestor"));
            GameState.AddCharacter(new CharacterState("Alex"));
        }
    }
}
