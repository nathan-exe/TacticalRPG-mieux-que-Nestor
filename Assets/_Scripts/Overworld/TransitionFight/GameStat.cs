using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script qui récupert les données de l'équipe
/// </summary>
public static class GameStat
{
    public static List<CharacterState> TeamState { get; private set; } = new();
    //pos joueur

    public static void AddCharacter(CharacterState character)
    {
        TeamState.Add(character);
        Debug.Log($"{character.Name} rejoint votre équipe. {character.HP} HP & {character.Mana} PM");
    }

    public static void DisplayTeam()
    {
        foreach (var character in TeamState)
        {
            Debug.Log($"- Nom: {character.Name}, PV: {character.HP}, Mana: {character.Mana}");
        }
    }

}
