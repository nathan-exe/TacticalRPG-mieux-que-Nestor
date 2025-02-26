using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script qui récupert les données de l'équipe
/// </summary>
public static class GameState
{
    public static List<CharacterState> TeamState = new(); //Liste des personnages de l'équipe
    public static Dictionary<string, bool> EncountersDico { get; private set; } = new(); //Dictionnaire des noms de zones ainsi que d'un bool pour savoir si elles sont clear ou non.
    public static Vector3 TeamPosition { get; private set; } = Vector3.zero; //Position de l'équipe dans l'overworld


    public static string ZoneName { get; private set; } //Nom de la zone actuel (pour le systeme de combat)

    public static void SetTeamPosition(Vector3 position) => TeamPosition = position;
    public static void SetZoneName(string zoneName) => ZoneName = zoneName;
    public static void AddCharacter(CharacterState character)
    {
        TeamState.Add(character);
        Debug.Log($"{character.EntityData.name} rejoint votre équipe. {character.HP} HP & {character.Mana} PM");
    }

    public static void DisplayTeam()
    {
        foreach (var character in TeamState)
        {
            Debug.Log($"- Nom: {character.EntityData.name}, PV: {character.HP}, Mana: {character.Mana}");
        }
    }

}
