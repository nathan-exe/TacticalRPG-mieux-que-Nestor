using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public string npcDialogueText;
}
