using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        //GameState.AddCharacter(new CharacterState("Mat�o"));
        GameState.AddCharacter(new CharacterState("Nestor"));
        GameState.AddCharacter(new CharacterState("Alex"));
    }
}
