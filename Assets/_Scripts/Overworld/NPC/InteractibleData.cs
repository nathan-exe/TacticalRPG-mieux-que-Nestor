using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New interactible", menuName = "Interactible")]
public class InteractibleData : ScriptableObject
{
    public string id;           
    public string name;         
    [TextArea] public string dialogue;     
    public string mood;         
    public string portrait;     
    public string sound;        
    public string nextLineID;   
    public string choice1;      
    public string choice2;       
    public string actionType;   
}
