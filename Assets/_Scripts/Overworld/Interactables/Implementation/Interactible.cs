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
    private bool canJoin;

    public InteractibleData interactibleData;


    private void Start()
    {
        interactibleName = interactibleData.name;
        interactibleText = interactibleData.interactibleDialogueText;
        canJoin = interactibleData.canJoinThePlayer;
    }
    public async UniTask InteractWith()
    {
        Debug.Log("coucou C moi");
        await DialogueBoxDisplay.Instance.OpenDialogueBox(interactibleName, interactibleText);
        await DialogueBoxDisplay.Instance.IsSkipping();

        if (canJoin)
        {
            GameStat.AddCharacter(new CharacterState(10, 100, "Matéo"));
            GameStat.DisplayTeam();
            canJoin = false;
            gameObject.SetActive(false);
        }
        if (interactibleData.canHealThePlayer)
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
