using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GameData
{
    public List<CharacterState> teamState;
    public string ZoneName;
    public Dictionary<string, bool> encountersDico;
    public Vector3 teamPosition;
}
