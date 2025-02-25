using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System;
using System.Globalization;

public class XMLmanager : FileManager
{
    const string FilePath = "Assets/TestXML.xml";

    void WriteXML<T>(XmlWriter writer, string key, T value)
    {
        writer.WriteStartElement(key);
        writer.WriteValue(value);
        writer.WriteEndElement();
    }

    public override GameData LoadData()
    {
        if (!System.IO.File.Exists(FilePath)) throw new System.IO.FileNotFoundException();

        XmlDocument saveFile = new();
        saveFile.Load(FilePath);
        GameData data = new();

        foreach (XmlNode node in saveFile.DocumentElement.ChildNodes)
        {
            switch (node.Name)
            {
                case "ZoneName":
                    data.ZoneName = node.InnerText;
                    break;

                case "teamPosition":
                    float x = float.Parse(node["x"].InnerText, CultureInfo.InvariantCulture);
                    float y = float.Parse(node["y"].InnerText, CultureInfo.InvariantCulture);
                    float z = float.Parse(node["z"].InnerText, CultureInfo.InvariantCulture);
                    data.teamPosition = new Vector3(x, y, z);
                    break;

                case "encountersDico":
                    data.encountersDico = new Dictionary<string, bool>();
                    foreach (XmlNode entry in node.ChildNodes)
                    {
                        string key = entry["Key"].InnerText;
                        bool value = bool.Parse(entry["Value"].InnerText);
                        data.encountersDico[key] = value;
                    }
                    break;

                case "teamState":
                    data.teamState = new List<CharacterState>();
                    foreach (XmlNode charNode in node.ChildNodes)
                    {
                        CharacterState character = new(charNode["Name"].InnerText);
                        character.Name = charNode["Name"].InnerText;
                        character.HP = int.Parse(charNode["HP"].InnerText);
                        character.Mana = int.Parse(charNode["Mana"].InnerText);
                        data.teamState.Add(character);
                    }
                    break;
            }
        }
        return data;
    }

    public override void SaveData(GameData data)
    {
        XmlWriterSettings settings = new XmlWriterSettings { Indent = true, NewLineOnAttributes = true };
        using XmlWriter writer = XmlWriter.Create(FilePath, settings);

        writer.WriteStartDocument();
        writer.WriteStartElement("Data");

        WriteXML(writer, "ZoneName", data.ZoneName);

        writer.WriteStartElement("teamPosition");
        WriteXML(writer, "x", data.teamPosition.x);
        WriteXML(writer, "y", data.teamPosition.y);
        WriteXML(writer, "z", data.teamPosition.z);
        writer.WriteEndElement();

        writer.WriteStartElement("encountersDico");
        foreach (var entry in data.encountersDico)
        {
            writer.WriteStartElement("Entry");
            WriteXML(writer, "Key", entry.Key);
            WriteXML(writer, "Value", entry.Value);
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteStartElement("teamState");
        foreach (var character in data.teamState)
        {
            writer.WriteStartElement("Character");
            WriteXML(writer, "Name", character.Name);
            WriteXML(writer, "HP", character.HP);
            WriteXML(writer, "Mana", character.Mana);
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteEndElement();
        writer.WriteEndDocument();
    }
}
