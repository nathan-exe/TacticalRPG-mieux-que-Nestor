using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System;
using Unity.VisualScripting.FullSerializer;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Globalization;

public class XMLmanager : FileManager 
{
    const string FilePath = "Assets/TestXML.xml";
    
    void WriteXML<T>(XmlWriter writer,string key,T value)
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

        foreach(XmlNode node in saveFile.ChildNodes[1])
        {
            switch (node.Name)
            {
                case "ClickCount":
                    data.clickCount = int.Parse(node.InnerText);
                    break;

                case "PlayTime":
                    data.playTime = float.Parse(node.InnerText, CultureInfo.InvariantCulture);
                    break;

                case "PlayerName":
                    data.playerName = node.InnerText;
                    break;
            }
        }

        return data;
    }

    public override void SaveData(GameData data)
    {

        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            NewLineOnAttributes = true,
            //ConformanceLevel = ConformanceLevel.Auto
        };
        XmlWriter writer = XmlWriter.Create(FilePath, settings);

        writer.WriteStartDocument();
        writer.WriteStartElement("Data");
        
        WriteXML(writer, "ClickCount" ,data.clickCount);
        WriteXML(writer, "PlayTime" ,data.playTime);
        WriteXML(writer, "PlayerName" ,data.playerName);
        
        writer.WriteEndElement();
        writer.WriteEndDocument();

        writer.Close();
        //write data
    }

}
