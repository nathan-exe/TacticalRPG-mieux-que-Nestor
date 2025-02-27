using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public class CSVReader : EditorWindow
{
    private static string folderPath = "Assets/_Scripts/Overworld/NPC/Interactible_Data";
    private static string csvFilePath = "Assets/_Data/Spreadsheets/Dialogues.csv";

    [MenuItem("Dialogues/1-Charger CSV")]
    public static void LoadCSV()
    {
        // V�rifie si le fichier CSV existe
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError($"Fichier CSV introuvable : {csvFilePath}");
            return;
        }

        // Lit le fichier CSV en texte
        string csvText = File.ReadAllText(csvFilePath);

        var lines = csvText.Split('\n') // S�pare en lignes
            .Where(line => !string.IsNullOrWhiteSpace(line)) // Supprime les lignes vides
            .Skip(1); // Ignore la premi�re ligne (en-t�te)

        foreach (var line in lines)
        {
            var data = ParseCSVLine(line);
            UpdateOrCreateDialogueData(data.ToArray());
        }

        Debug.Log("CSV charg� avec succ�s");
    }

    // Analyse une ligne CSV tout en g�rant les champs entre guillemets
    private static List<string> ParseCSVLine(string line)
    {
        List<string> fields = new List<string>();
        StringBuilder currentField = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"') // G�re les guillemets doubles ""
                {
                    currentField.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (line[i] == ',' && !inQuotes) // S�parateur de champ
            {
                fields.Add(currentField.ToString().Trim());
                currentField.Clear();
            }
            else
            {
                currentField.Append(line[i]);
            }
        }

        fields.Add(currentField.ToString().Trim()); // Ajoute le dernier champ

        // Retire les guillemets des champs
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

    // Cr�e ou met � jour un ScriptableObject � partir des donn�es CSV
    private static void UpdateOrCreateDialogueData(string[] data)
    {
        var path = Path.Combine(folderPath, $"{data[0]}.asset"); // Chemin de sauvegarde du ScriptableObject
        var newData = CreateDialogueData(data);

        // V�rifie si un fichier de donn�es existe d�j�
        var existingData = AssetDatabase.LoadAssetAtPath<InteractibleData>(path);
        if (existingData != null)
        {
            if (UpdateExistingData(existingData, newData)) // Met � jour uniquement si des changements sont d�tect�s
            {
                EditorUtility.SetDirty(existingData);
                AssetDatabase.SaveAssets();
                Debug.Log($"Mis � jour : {data[0]}");
            }
            else
            {
                Debug.Log($"Aucune modification : {data[0]}");
            }
        }
        else // Cr�e un nouveau fichier de donn�es
        {
            AssetDatabase.CreateAsset(newData, path);
            AssetDatabase.SaveAssets();
            Debug.Log($"Nouveau fichier cr�� : {data[0]}");
        }
    }

    // Cr�e un nouveau scriptable
    private static InteractibleData CreateDialogueData(string[] data)
    {
        var dialogueData = ScriptableObject.CreateInstance<InteractibleData>();

        // Remplit les champs avec les donn�es du CSV
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

    // Met � jour les donn�es si des changements sont d�tect�s
    private static bool UpdateExistingData(InteractibleData existing, InteractibleData newData)
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

        // V�rifie et met � jour chaque champ
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
