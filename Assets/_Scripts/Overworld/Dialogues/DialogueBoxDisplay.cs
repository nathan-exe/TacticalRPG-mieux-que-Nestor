using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxDisplay : MonoBehaviour
{
    [SerializeField] NPCData npcData;

    [SerializeField] TextMeshProUGUI boxName;
    [SerializeField] TextMeshProUGUI boxDialogueText;
    
    void Start()
    {
        boxName.text = npcData.npcName;
        boxDialogueText.text = npcData.npcDialogueText;
    }

}
