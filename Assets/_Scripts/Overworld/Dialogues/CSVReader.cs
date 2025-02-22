using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile;
    public string folderPath = "Assets/_Scripts/Overworld/NPC/Interactible_Data";

    void Start() => LoadCSV();

    public void LoadCSV()
    {
        var lines = csvFile.text.Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Skip(1);

        foreach (var line in lines)
        {
            var data = ParseCSVLine(line);
            if (data.Count > 0)
            {
                UpdateOrCreateDialogueData(data.ToArray());
            }
        }
    }

    private List<string> ParseCSVLine(string line)
    {
        List<string> fields = new List<string>();
        StringBuilder currentField = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"') 
                {
                    currentField.Append('"');
                    i++; 
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (line[i] == ',' && !inQuotes)
            {
                fields.Add(currentField.ToString().Trim());
                currentField.Clear();
            }
            else
            {
                currentField.Append(line[i]);
            }
        }

        fields.Add(currentField.ToString().Trim());

        for (int i = 0; i < fields.Count; i++)
        {
            string field = fields[i];
            if (field.StartsWith("\"") && field.EndsWith("\""))
            {
                fields[i] = field.Substring(1, field.Length - 2);
            }
        }

        return fields;
    }

    private void UpdateOrCreateDialogueData(string[] data)
    {
        var path = Path.Combine(folderPath, $"{data[0]}.asset");
        var newData = CreateDialogueData(data);

        Directory.CreateDirectory(folderPath);

        var existingData = AssetDatabase.LoadAssetAtPath<InteractibleData>(path);
        if (existingData != null)
        {
            if (UpdateExistingData(existingData, newData))
            {
                EditorUtility.SetDirty(existingData);
                AssetDatabase.SaveAssets();
                Debug.Log($"Updated existing ScriptableObject with id: {data[0]}");
            }
            else
            {
                Debug.Log($"No changes for id: {data[0]}");
            }
        }
        else
        {
            AssetDatabase.CreateAsset(newData, path);
            AssetDatabase.SaveAssets();
            Debug.Log($"Created new ScriptableObject for id: {data[0]}");
        }
    }

    private InteractibleData CreateDialogueData(string[] data)
    {
        var dialogueData = ScriptableObject.CreateInstance<InteractibleData>();
        dialogueData.id = data[0];
        dialogueData.name = data[1];
        dialogueData.dialogue = data[2];
        dialogueData.mood = data[3];
        dialogueData.portrait = data[4];
        dialogueData.sound = data[5];
        dialogueData.nextLineID = data[6];
        dialogueData.choice1 = data[7];
        dialogueData.choice2 = data[8];
        dialogueData.actionType = data[9];
        return dialogueData;
    }

    private bool UpdateExistingData(InteractibleData existing, InteractibleData newData)
    {
        bool hasChanges = false;

        void UpdateField<T>(ref T field, T newValue)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                hasChanges = true;
            }
        }

        UpdateField(ref existing.name, newData.name);
        UpdateField(ref existing.dialogue, newData.dialogue);
        UpdateField(ref existing.mood, newData.mood);
        UpdateField(ref existing.portrait, newData.portrait);
        UpdateField(ref existing.sound, newData.sound);
        UpdateField(ref existing.nextLineID, newData.nextLineID);
        UpdateField(ref existing.choice1, newData.choice1);
        UpdateField(ref existing.choice2, newData.choice2);
        UpdateField(ref existing.actionType, newData.actionType);

        return hasChanges;
    }
}