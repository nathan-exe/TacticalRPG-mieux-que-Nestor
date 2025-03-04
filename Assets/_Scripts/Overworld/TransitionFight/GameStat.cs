using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script qui r�cupert les donn�es de l'�quipe
/// </summary>
public static class GameState
{
    public static List<CharacterState> TeamState = new(); //Liste des personnages de l'�quipe
    public static Dictionary<string, bool> EncountersDico { get; private set; } = new(); //Dictionnaire des noms de zones ainsi que d'un bool pour savoir si elles sont clear ou non.
    public static Vector3 TeamPosition { get; private set; } = Vector3.zero; //Position de l'�quipe dans l'overworld



    public static string ZoneName { get; private set; } //Nom de la zone actuel (pour le systeme de combat)

    public static void SetTeamPosition(Vector3 position) => TeamPosition = position;
    public static void SetZoneName(string zoneName) => ZoneName = zoneName;
    public static void AddCharacter(CharacterState character)
    {
        TeamState.Add(character);
        Debug.Log($"{character.EntityData.name} rejoint votre �quipe. {character.HP} HP & {character.Mana} PM");
    }
    public static GameObject GetPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player;
    }

    public static void DisplayTeam()
    {
        Debug.Log("Team :");
        foreach (var character in TeamState)
        {
            Debug.Log($"- Nom: {character.EntityData.name}, PV: {character.HP}, Mana: {character.Mana}");
        }
    }

    public static void DisplayEncounters()
    {
        Debug.Log("Dico :");
        foreach (var v in EncountersDico)
        {
            Debug.Log($"- key: {v.Key}, Value: {v.Value}");
        }
    }

}
