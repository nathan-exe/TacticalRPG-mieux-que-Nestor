using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class SpellSelectionPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<SpellButton> _buttons;
    [SerializeField] CanvasGroup _canvasGroup;



    bool Waiting = true;

    //spell rotation
    public event Action<int> OnSpellRotated;
    private int _orientation;
    public int SpellOrientation { get => _orientation; set { _orientation = value % 4; OnSpellRotated?.Invoke(SpellOrientation); } }

    bool shouldSkip = false;
    public void skip()
    {
        shouldSkip = true;
    }

    private void Start()
    {
        transform.localScale = Vector3.one;
        _canvasGroup.alpha = 0;
    }

    public async UniTask SelectEntitySpell(CombatEntity Entity)
    {
        Assert.IsTrue(Entity.Data.Spells.Count == 3 , "Il devrait y avoir 3 spells par personnage jouable");

        transform.DOScale(Vector3.one, .15f);
        _canvasGroup.DOFade(1, .15f);

        Waiting = true;
        SetUpButtons(Entity);

        //attend de recevoir un event de la part des boutons
        while (Waiting)
        {
            await UniTask.Yield();
            
            if (Input.GetMouseButtonDown(1)) SpellOrientation++; //rotation du spell
            if (shouldSkip) { shouldSkip = false; Waiting = false; }
        }

        DisableButtons();

        transform.DOScale(Vector3.zero, .15f);
        _canvasGroup.DOFade(0, .15f);


    }

    
    void SetUpButtons(CombatEntity entity)
    {
        for (int i = 0; i < entity.Data.Spells.Count; i++) 
        {
            _buttons[i].Initialize(entity, i);

            //events
            int su = i;
            _buttons[i].Button.onClick.RemoveAllListeners();
            _buttons[i].OnMouseHover += () => entity.SpellCaster.SelectedSpellData = entity.Data.Spells[su];
            _buttons[i].OnMouseHoverExit += () => entity.SpellCaster.SelectedSpellData = null;
            _buttons[i].Button.onClick.AddListener(() => Waiting = false);
            OnSpellRotated += _buttons[i].OnSpellRotationMessageReceived;
        }
    }

    private void DisableButtons()
    {
        for (int i = 0; i < 3; i++)
        {
            OnSpellRotated -= _buttons[i].OnSpellRotationMessageReceived;
            _buttons[i].Disable();
        }
    }

}
