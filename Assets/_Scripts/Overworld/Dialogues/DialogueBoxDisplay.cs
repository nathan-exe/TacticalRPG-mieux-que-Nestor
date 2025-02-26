using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Cysharp.Threading.Tasks;
using UnityEngine.TextCore.Text;

public class DialogueBoxDisplay : MonoBehaviour
{
    public static DialogueBoxDisplay Instance { get; private set; }
    [SerializeField] TextMeshProUGUI boxName;
    [SerializeField] TextMeshProUGUI boxDialogueText;
    [SerializeField] PlayerOverworldController playerOverworldController;

    bool isDialogueOpen = false;
    bool isSkipping = false;

    public int letterDelay = 50;
    public int punctuationDelay = 200;

    private bool IsPunctuation(char c) => c is '?' or '.' or '!' or ',';

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


    public async UniTask OpenDialogueBox(string name, string dialogue) //ouvre la boite de dialogue et désactive les controles du joueur
    {
        playerOverworldController.enabled = false;
        isSkipping = false;
        isDialogueOpen = true;
        UiManager.Instance.ShowPanel(UiManager.Instance.DialoguePanel);
        boxName.text = name;
        boxDialogueText.text = "";

        UniTask SkipText = IsSkipping();

        foreach (char c in dialogue)
        {
            if (isSkipping) // Si action du joueur, tout le texte défile rapidement
            {
                boxDialogueText.text = dialogue;
                break;
            }
            boxDialogueText.text += c; //effet d'écriture
            if (IsPunctuation(c)){ // Si ponctuation, alors on attends un peu plus
                await UniTask.Delay(punctuationDelay);
            }
            await UniTask.Delay(letterDelay); //on attend légérement entre chaque caractère
        }

        await PlayerInput(); //on attend que le joueur ai finit de lire

        CloseDialogueBox(); //on ferme la boite
    }

    public void CloseDialogueBox() //ferme la boite puis réactive le controller du joueur
    {
        UiManager.Instance.HideCurrentPanel();
        isDialogueOpen = false;
        playerOverworldController.enabled = true;
    }

    private async UniTask PlayerInput()
    {
        while (!Input.anyKeyDown || PauseManager.Instance.isPaused)
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