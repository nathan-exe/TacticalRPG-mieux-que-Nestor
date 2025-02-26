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
    private static SaveManager Instance;

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

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Load");
            LoadGame();
        }
    }

    public void SaveCurrentGameState(string SaveName = "Save1")
    {

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.NewLineOnAttributes = false;

        string directoryPath = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        string filePath = Path.Combine(directoryPath, SaveName + ".xml");

        using (XmlWriter writer = XmlWriter.Create(filePath, settings))
        {
            writer.WriteStartDocument();
            writer.WriteComment(SaveName);
            writer.WriteStartElement("Data");

            //time
            WriteValue(writer, "SaveDate", System.DateTime.Now.ToShortDateString());

            //player overworld position
            WriteValue(writer, "PlayerPosition", GameState.TeamPosition);

            //encounters
            writer.WriteStartElement("EncountersDico");
            foreach (KeyValuePair<string, bool> keyValuePair in GameState.EncountersDico)
            {
                WriteValue(writer, "KeyValuePair", keyValuePair);
            }
            writer.WriteEndElement();

            //team state
            writer.WriteStartElement("TeamState");
            foreach (CharacterState character in GameState.TeamState)
            {
                writer.WriteStartElement("Character");

                WriteValue(writer, "DataFileName", character.DataFileName);
                WriteValue(writer, "HP", character.HP);

                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        Debug.Log("Saved xml data to file : \n" + filePath);
    }


    public void LoadGame(string SaveName = "Save1")
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, "Saves");
        string filePath = Path.Combine(directoryPath, SaveName + ".xml");

        Assert.IsTrue(Directory.Exists(directoryPath), "No save found");
        Assert.IsTrue(File.Exists(filePath), "No save found");

        Debug.Log("Reading save file :\n" + filePath);

        using (XmlReader reader = XmlReader.Create(filePath))
        {
            reader.ReadStartElement("Data");

            //date
            reader.ReadToFollowing("SaveDate");
            Debug.Log("Save date : " + reader.ReadElementContentAsString());

            //position
            reader.ReadToFollowing("PlayerPosition");
            GameState.SetTeamPosition(reader.ReadElementContentAsVector3());

            //dico
            reader.ReadToFollowing("EncountersDico");
            reader.ReadStartElement();
            GameState.EncountersDico.Clear();
            while (reader.Read() && (reader.Name == "KeyValuePair"))
            {
                KeyValuePair<string, bool> p = reader.ReadElementContentAsStringBoolPair();
                GameState.EncountersDico.Add(p.Key, p.Value);
            }
            reader.ReadEndElement();

            //teamstate
            reader.ReadToFollowing("TeamState");
            Debug.Log("-");
            Debug.Log(reader.NodeType.ToString() + " " + reader.Name.ToString());

            GameState.TeamState.Clear();
            if (!reader.IsEmptyElement)
            {
                reader.ReadToFollowing("Character");
                Debug.Log(reader.NodeType.ToString() + " " + reader.Name.ToString());
                while (reader.Name == "Character") 
                {

                    //reader.ReadToFollowing("Character");
                    //reader.ReadStartElement();

                    reader.ReadToFollowing("HP");
                    Debug.Log(reader.NodeType.ToString() + " " + reader.Name.ToString());
                    //Debug.Log(reader.ReadContentAsString());
                    float hp = reader.ReadElementContentAsFloat();

                    reader.ReadToFollowing("DataFileName");
                    Debug.Log(reader.NodeType.ToString() + " " + reader.Name.ToString());
                    string fileName = reader.ReadElementContentAsString();

                    reader.Read();
                    //reader.ReadToFollowing("Character");
                    GameState.TeamState.Add(new(fileName, hp));
                } 
            }
                
            reader.ReadEndElement();


            reader.ReadEndElement();


            GameState.DisplayTeam();
            GameState.DisplayEncounters();
            Debug.Log(GameState.TeamPosition);
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
        foreach(string s2 in s) {Debug.Log(s2);}
        return new Vector3(float.Parse(s[0], CultureInfo.InvariantCulture), float.Parse(s[1], CultureInfo.InvariantCulture), float.Parse(s[2], CultureInfo.InvariantCulture));


    }

    public static KeyValuePair<string, bool> ReadElementContentAsStringBoolPair(this XmlReader reader) //nul mais pas le temps d'apprendre une lib
    {
        string v = reader.ReadElementContentAsString();
        string[] s = v.Substring(1,v.Length - 2).Split(',');
        return new KeyValuePair<string, bool>(s[0], bool.Parse(s[1]));
    }
}