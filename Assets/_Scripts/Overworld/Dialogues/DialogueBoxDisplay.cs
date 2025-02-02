using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Cysharp.Threading.Tasks;

public class DialogueBoxDisplay : MonoBehaviour
{
    public static DialogueBoxDisplay Instance { get; private set; }
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI boxName;
    [SerializeField] TextMeshProUGUI boxDialogueText;
    [SerializeField] PlayerOverworldController playerOverworldController;

    bool isDialogueOpen = false;
    bool isSkipping = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        dialogueBox.SetActive(false);
    }

    public async UniTask OpenDialogueBox(string name, string dialogue)
    {
        playerOverworldController.enabled = false;
        isSkipping = false;
        isDialogueOpen = true;
        dialogueBox.SetActive(true);
        boxName.text = name;
        boxDialogueText.text = "";

        UniTask SkipText = IsSkipping();

        foreach (char c in dialogue)
        {
            if (isSkipping)
            {
                boxDialogueText.text = dialogue;
                break;
            }
            boxDialogueText.text += c;
            await UniTask.Delay(50); 
        }

        await PlayerInput();

        CloseDialogueBox();
    }

    public void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        isDialogueOpen = false;
        playerOverworldController.enabled = true;
    }

    private async UniTask PlayerInput()
    {
        while (!Input.anyKeyDown)
        {
            await UniTask.Yield();
        }
    }

    public async UniTask IsSkipping()
    {
        while (isDialogueOpen)
        {
            if (Input.anyKeyDown)
            {
                isSkipping = true;
                return;
            }
            await UniTask.Yield();
        }
    }
}