using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Toutes les datas
[Serializable]
public struct Dico
{
    ////GameStat.EncountersDico
}

[Serializable]
public class SaveData
{
    public Vector3 TeamPosition;  // Position de l'équipe
    public List<CharacterState> TeamState;  // Liste des personnages
}