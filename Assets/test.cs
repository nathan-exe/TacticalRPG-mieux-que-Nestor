using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        GameStat.AddCharacter(new CharacterState("Matéo"));
        GameStat.AddCharacter(new CharacterState("Nestor"));
    }
}
