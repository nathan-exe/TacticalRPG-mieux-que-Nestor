using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


public class NPC : MonoBehaviour
{
    [SerializeField] NPCData npcData;
    [SerializeField] DialogueBoxDisplay dialogueBoxDisplay;

    private void Start()
    {
        Debug.Log($"Hey, je suis {npcData.npcName} et voici mon dialogue : {npcData.npcDialogueText}");
    }

    public void CallDialogueBox()
    {
        dialogueBoxDisplay.OpenDialogueBox(npcData.npcName, npcData.npcDialogueText);
    }
}
