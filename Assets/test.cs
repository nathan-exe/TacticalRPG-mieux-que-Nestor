using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        GameStat.AddCharacter(new CharacterState(10, 100, "Mat�o"));
        GameStat.AddCharacter(new CharacterState(5, 50, "Nestor"));
    }
}
