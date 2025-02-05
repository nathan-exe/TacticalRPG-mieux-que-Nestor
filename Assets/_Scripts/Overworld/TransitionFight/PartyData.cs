using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script qui récupert les données de l'équipe
/// </summary>
public class PartyData : MonoBehaviour
{
    public List<CharacterState> TeamState { get; private set; } = new();
    public static PartyData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public PartyData()
    {
        TeamState = new List<CharacterState>();
    }

    public void AddCharacter(CharacterState character)
    {
        TeamState.Add(character);
    }

    public void DisplayTeam()
    {
        foreach (var character in TeamState)
        {
            print($"{character.Name} - PV: {character.HP}, Mana: {character.Mana}");
        }
    }
}
