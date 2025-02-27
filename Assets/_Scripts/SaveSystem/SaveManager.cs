using System.IO;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public class SaveManager : MonoBehaviour
{

    public string CurrentFileUse = "Save1";

    //singleton wish
    public static SaveManager Instance;
    void Awake()
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

    /// <summary>
    /// Saves the current gamestate values to a save file.
    /// </summary>
    public void SaveCurrentGameState(string SaveName = "Save1")
    {

        string directoryPath = Path.Combine(Application.persistentDataPath, "Saves");

        GameState.SetTeamPosition(GameState.GetPlayer().transform.position);

        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        string filePath = Path.Combine(directoryPath, SaveName);

        using (var filestream = File.Open(filePath, FileMode.Create))
        {
            using (BinaryWriter w = new(filestream))
            {
                //teamstate
                w.Write((Int32)GameState.TeamState.Count);
                foreach (CharacterState state in GameState.TeamState)
                {
                    w.Write(state.HP);
                    w.Write(state.DataFileName);
                }

                //dico
                w.Write((Int32)GameState.EncountersDico.Count);
                foreach (var kv in GameState.EncountersDico)
                {
                    w.Write(kv.Key);
                    w.Write(kv.Value);
                }

                //playerPosition
                w.Write(GameState.TeamPosition.x);
                w.Write(GameState.TeamPosition.y);
                w.Write(GameState.TeamPosition.z);

            }

            filestream.Close();
        }

        Debug.Log("Saved data to file : \n" + filePath);
    }

    /// <summary>
    /// reads a save file and applies its values to the GameState static class
    /// </summary>
    /// <param name="SaveName"></param>
    public void LoadGame(string SaveName = "Save1")
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, "Saves");
        string filePath = Path.Combine(directoryPath, SaveName);

        Assert.IsTrue(Directory.Exists(directoryPath), "No save found");
        Assert.IsTrue(File.Exists(filePath), "No save found");

        Debug.Log("Reading save file :\n" + filePath);

        using (var filestream = File.Open(filePath, FileMode.Open))
        {
            using (BinaryReader r = new(filestream))
            {

                //teamstate
                GameState.TeamState.Clear();
                int n = r.ReadInt32();
                for(int i = 0; i < n;i++)
                {
                    float hp = r.ReadSingle();
                    string filename = r.ReadString();
                    GameState.TeamState.Add(new(filename,hp));
                }

                //dico
                GameState.EncountersDico.Clear();
                n = r.ReadInt32();
                for (int i = 0; i < n;i++)
                {
                    GameState.EncountersDico.Add(r.ReadString(), r.ReadBoolean());
                }

                //player position
                GameState.SetTeamPosition(new Vector3(r.ReadSingle(), r.ReadSingle(), r.ReadSingle()));

                GameState.DisplayTeam();
                GameState.DisplayEncounters();
                Debug.Log("Overworld position : " + GameState.TeamPosition);
                
            }
        }
    }

}
