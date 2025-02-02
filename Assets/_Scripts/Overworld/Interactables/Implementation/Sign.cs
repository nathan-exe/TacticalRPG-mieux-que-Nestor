using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Sign : MonoBehaviour, Iinteractable
{

    public string signName = "Sign";
    [TextArea] public string signText;
    
    public async UniTask InteractWith()
    {
        Debug.Log("coucou C moi");
        await DialogueBoxDisplay.Instance.OpenDialogueBox(signName, signText);
        await DialogueBoxDisplay.Instance.IsSkipping();

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
