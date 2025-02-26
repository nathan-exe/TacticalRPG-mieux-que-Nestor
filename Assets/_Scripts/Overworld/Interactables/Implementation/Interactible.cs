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

    public async UniTask InteractWith()
    {
        await DialogueBoxDisplay.Instance.OpenDialogueBox(interactibleData.name, interactibleData.dialogue);
        await DialogueBoxDisplay.Instance.IsSkipping();

        //Non fonctionnel, ajoute des actions suites au dialogue
        if (interactibleData.actionType == "Join" && canJoin)
        {
            GameState.AddCharacter(new CharacterState("Matéo"));
            GameState.DisplayTeam();
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
