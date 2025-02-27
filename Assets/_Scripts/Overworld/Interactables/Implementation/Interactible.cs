using Cysharp.Threading.Tasks;
using UnityEngine;

public class Interactible : MonoBehaviour, Iinteractable
{
    [SerializeField]
    private GameObject _capsuleMateo;
    public string interactibleName;
    [TextArea] 
    public string interactibleText;
    bool canJoin = true;

    public InteractibleData interactibleData;

    public async UniTask InteractWith()
    {
        await DialogueBoxDisplay.Instance.OpenDialogueBox(interactibleData.name, interactibleData.dialogue);
        await DialogueBoxDisplay.Instance.IsSkipping();

        if (interactibleData.actionType == "Join" && canJoin)
        {
            GameState.AddCharacter(new CharacterState("Matéo"));
            GameState.DisplayTeam();
            canJoin = false;
            _capsuleMateo.SetActive(true);
            gameObject.SetActive(false);
            //ajout capsule
        }
        else if (interactibleData.actionType == "Heal")
        {
            foreach(var character in GameState.TeamState)
            {
                character.HP = character.EntityData.MaxHP;
                gameObject.SetActive(false);
            }
        }
        else if(interactibleData.actionType == "Save")
        {
            SaveManager.Instance.SaveCurrentGameState(SaveManager.Instance.CurrentFileUse);
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
