using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartyOverworld : MonoBehaviour
{
    public static List<PlayerPartyOverworld> Instances = new List<PlayerPartyOverworld>();
    void Start()
    {
        GameStat.AddCharacter(new CharacterState(5, 50, "Nestor"));
    }

}
