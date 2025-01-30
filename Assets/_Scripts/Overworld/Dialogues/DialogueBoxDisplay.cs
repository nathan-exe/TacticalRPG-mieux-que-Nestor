using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI boxName;
    [SerializeField] TextMeshProUGUI boxDialogueText;
    
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenDialogueBox(string npcName, string npcDialogue)
    {
        gameObject.SetActive(true);
        boxName.text = npcName;
        boxDialogueText.text = npcDialogue;
    }
}
