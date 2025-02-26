using System.IO;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;

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
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Save");
            SaveCurrentGameState();
        }
    }

    public void SaveCurrentGameState(string SaveName = "Save1")
    {

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.NewLineOnAttributes = false;

        string directoryPath = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        string filePath = Path.Combine(directoryPath, SaveName+".xml");

        using (XmlWriter writer = XmlWriter.Create(filePath,settings))
        {
            writer.WriteStartDocument();
            writer.WriteComment(SaveName);
            writer.WriteStartElement("Data");

            //time
            WriteValue(writer, "SaveDate", System.DateTime.Now.ToShortTimeString());

            //player overworld position
            WriteValue(writer,"PlayerPosition", GameState.TeamPosition);

            //encounters
            writer.WriteStartElement("EncountersDico");
            foreach(KeyValuePair<string,bool> keyValuePair in GameState.EncountersDico)
            {
                writer.WriteStartElement("Key");
                writer.WriteString(keyValuePair.Key);
                writer.WriteEndElement();
                
                writer.WriteStartElement("Value");
                writer.WriteValue(keyValuePair.Value);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            //team state
            writer.WriteStartElement("TeamState");
            foreach(CharacterState character in GameState.TeamState)
            {
                writer.WriteStartElement("Character");
                
                writer.WriteStartElement("HP");
                writer.WriteValue(character.HP);
                writer.WriteEndElement();

                writer.WriteStartElement("DataFileName");
                writer.WriteString(character.DataFileName);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        Debug.Log("Saved xml data to file : \n" + filePath);
    }


    public void LoadGame()
    {
        string path = Path.Combine(Application.dataPath, "saveRomainQuest.xml");

        if (File.Exists(path))
        {
            //Load
        }
        else
        {
            Debug.LogError("FDP");
        }
    }

    void WriteValue<T>(XmlWriter writer,string name,T value) 
    {
        writer.WriteStartElement(name);
        writer.WriteAttributeString("Type", typeof(T).ToString());
        writer.WriteString(value.ToString());
        writer.WriteEndElement();
    }

}
