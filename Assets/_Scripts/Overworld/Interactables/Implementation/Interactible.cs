using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Interactible : MonoBehaviour, Iinteractable
{

    public string interactibleName;
    [TextArea] 
    public string interactibleText;
    bool canJoin = true;

    public InteractibleData interactibleData;

    public void dialogueLoad()
    {
        interactibleName = interactibleData.name;
        interactibleText = interactibleData.dialogue;
    }
    public async UniTask InteractWith()
    {
        Debug.Log("coucou C moi");
        await DialogueBoxDisplay.Instance.OpenDialogueBox(interactibleName, interactibleText);
        await DialogueBoxDisplay.Instance.IsSkipping();

        if (interactibleData.actionType == "Join" && canJoin)
        {
            GameStat.AddCharacter(new CharacterState("Mat�o"));
            GameStat.DisplayTeam();
            canJoin = false;
            gameObject.SetActive(false);
        }
        else if (interactibleData.actionType == "Heal")
        {
            Debug.Log("PlayerParty heal");

        }
    }

    public void OnSelected()
    {
        transform.localScale = Vector3.one*1.2f;
    }

    public void OnUnselected()
    {
        transform.localScale = Vector3.one;
    }
}
