using System.IO;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

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
            SaveGame();
        }
    }

    public void SaveGame()
    {
        string path = Path.Combine(Application.dataPath, "saveRomainQuest.xml");

        SaveData saveData = new SaveData
        {
            TeamPosition = GameStat.TeamPosition,
            TeamState = GameStat.TeamState
        };

        SerializeToXML(saveData, path);
    }

    private void SerializeToXML(SaveData saveData, string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        using (FileStream fileStream = new FileStream(path, FileMode.Create)) 
        {
            serializer.Serialize(fileStream, saveData);
        }
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

}
