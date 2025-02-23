using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using UnityEditor.SceneManagement;

public class AssignDialogue : EditorWindow
{
    private static string folderPath = "Assets/_Scripts/Overworld/NPC/Interactible_Data";


    [MenuItem("Dialogues/2-Assigner Dialogues")]
    public static void AssignDialogues()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (var npc in npcs)
        {
            var path = Path.Combine(folderPath, npc.name + ".asset");
            InteractibleData interactibleData = AssetDatabase.LoadAssetAtPath<InteractibleData>(path);

            if (interactibleData != null)
            {
                Interactible interactible = npc.GetComponent<Interactible>();
                if (interactible != null)
                {
                    // Assigne le scriptable à l'objet trouvé
                    interactible.interactibleData = interactibleData;
                    interactible.dialogueLoad();
                    Debug.Log($"{npc.name} à maintenant le dialogue de {interactibleData.name}");
                }
                else
                {
                    Debug.LogWarning($"{npc.name} n'a pas de script Interactible.");
                }
            }
            else
            {
                Debug.LogWarning($"Pas de dialogue trouvé pour NPC: {npc.name}");
            }
        }

        //Save les changements
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        AssetDatabase.SaveAssets();
    }
}
