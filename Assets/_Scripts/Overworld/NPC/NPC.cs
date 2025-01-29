using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour

{
    [SerializeField] NPCData npcData;


    private void Start()
    {
        Debug.Log($"Hey, je suis {npcData.npcName} et voici mon dialogue : {npcData.npcDialogueText}");
    }
}
