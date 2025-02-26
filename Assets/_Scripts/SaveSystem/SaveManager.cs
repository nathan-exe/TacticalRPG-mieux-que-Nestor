using System.IO;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;
using UnityEngine.Assertions;
using System.Collections;
using Unity.Mathematics;
using System;
using System.Globalization;

public class SaveManager : MonoBehaviour
{
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

    private void Update()
    {
        /*if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Save");
            SaveCurrentGameState();
        }*/

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Save");
            SaveCurrentGameState();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Load");
            LoadGame();
        }
    }

    public void SaveCurrentGameState(string SaveName = "Save1")
    {

        string directoryPath = Path.Combine(Application.persistentDataPath, "Saves");
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



    void WriteValue<T>(XmlWriter writer, string name, T value)
    {
        writer.WriteStartElement(name);
        //writer.WriteAttributeString("Type", typeof(T).ToString());
        writer.WriteValue(value);
        writer.WriteEndElement();
    }




}
public static class xmlReaderExtension
{
    public static Vector3 ReadElementContentAsVector3(this XmlReader reader) //nul mais pas le temps d'apprendre une lib
    {
        string v = reader.ReadElementContentAsString();
        string[] s = v.Substring(1,v.Length - 2).Split(',');
        return new Vector3(float.Parse(s[0], CultureInfo.InvariantCulture), float.Parse(s[1], CultureInfo.InvariantCulture), float.Parse(s[2], CultureInfo.InvariantCulture));


    }

    public static KeyValuePair<string, bool> ReadElementContentAsStringBoolPair(this XmlReader reader) //nul mais pas le temps d'apprendre une lib
    {
        string v = reader.ReadElementContentAsString();
        string[] s = v.Substring(1,v.Length - 2).Split(',');
        return new KeyValuePair<string, bool>(s[0], bool.Parse(s[1]));
    }
}