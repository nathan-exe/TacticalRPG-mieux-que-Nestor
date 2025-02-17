using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New interactible", menuName = "Interactible")]
public class InteractibleData : ScriptableObject
{
    public string interactibleName;
    [TextArea] public string interactibleDialogueText;
    public bool canJoinThePlayer;
    public bool canHealThePlayer;
}
