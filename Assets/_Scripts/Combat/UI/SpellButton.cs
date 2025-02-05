using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private const float TweenDuration = .2f;

    public Button Button;
    [field: SerializeField] public SpellInfoBubble _infoBubble;

    SpellCaster _spellCaster;
    CombatEntity _currentEntity;

    bool _interactable;

    bool _isOverlappedByMouse;

    public event Action OnMouseHover;
    public event Action OnMouseHoverExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_interactable) return;

        transform.DOScale(Vector3.one * 1.2f, TweenDuration);

        OnMouseHover?.Invoke();
        _infoBubble.ShowSpellInfo(_spellCaster.SelectedSpellData);
        _isOverlappedByMouse = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_interactable) return;

        transform.DOScale(Vector3.one, TweenDuration);
        OnMouseHoverExit?.Invoke();
        _infoBubble.Hide();
        _isOverlappedByMouse = false;
    }

    public void OnSpellRotationMessageReceived(int Orientation)
    {
        if (_isOverlappedByMouse)
        {
            _spellCaster.Orientation = Orientation;
        }
        
    }

    public void Initialize(CombatEntity entity,int spellID)
    {
        _currentEntity = entity;
        _spellCaster = entity.SpellCaster;

        _interactable = entity.Mana >= entity.Data.Spells[spellID].ManaCost;
        Button.interactable = _interactable;

        Image i = (Image)Button.targetGraphic;
        i.sprite = entity.Data.Spells[spellID].Sprite;
        i.color = _interactable ? Color.white : Color.grey;
    }

    public void Disable()
    {
        _interactable = false;
        Button.interactable = false;

        foreach (Action d in OnMouseHover.GetInvocationList()) OnMouseHover -= d;
        foreach (Action d in OnMouseHoverExit.GetInvocationList()) OnMouseHoverExit -= d;


        transform.DOScale(Vector3.one, TweenDuration);
        _infoBubble.Hide();
    }


}
