using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class XMLmanager
{
    public void WriteXML<T>(XmlWriter writer, string key, T value)
    {
        writer.WriteStartElement(key);
        writer.WriteValue(value);
        writer.WriteEndElement();
    }

    public void SerializeTeamState(List<CharacterState> teamState, XmlWriter writer)
    {
        writer.WriteStartElement("TeamState");
        foreach (var character in teamState)
        {
            writer.WriteStartElement("Character");
            writer.WriteElementString("Name", character.EntityData.name);
            writer.WriteElementString("HP", character.HP.ToString());
            writer.WriteElementString("Mana", character.Mana.ToString());
            writer.WriteEndElement();  // </Character>
        }
        writer.WriteEndElement(); // </TeamState>
    }
}
