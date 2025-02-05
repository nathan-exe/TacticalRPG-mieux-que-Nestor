using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private const float TweenDuration = .2f;

    public Button Button;
    [field: SerializeField] public SpellInfoBubble _infoBubble;

    Spell _spell;
    CombatEntity _currentEntity;

    bool _interactable;

    bool _isOverlappedByMouse;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_interactable) return;

        transform.DOScale(Vector3.one * 1.2f, TweenDuration);
        _spell.PreviewSpellEffect(_currentEntity);
        _infoBubble.ShowSpellInfo(_spell);
        _isOverlappedByMouse = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_interactable) return;

        transform.DOScale(Vector3.one, TweenDuration);
        _spell.CancelPreview(_currentEntity);
        _infoBubble.Hide();
        _isOverlappedByMouse = false;
    }

    public void OnSpellRotationMessageReceived(int Orientation)
    {
        if (_isOverlappedByMouse)
        {
            _spell.Orientation = Orientation;
            _spell.CancelPreview(_currentEntity);
            _spell.PreviewSpellEffect(_currentEntity);
        }
        
    }

    public void Initialize(CombatEntity entity,int spellID)
    {
        _currentEntity = entity;
        _spell = entity.Data.Spells[spellID] ;

        _interactable = entity.Mana >= _spell.Data.ManaCost;
        Button.interactable = _interactable;

        Image i = (Image)Button.targetGraphic;
        i.sprite = _spell.Data.Sprite;
        i.color = _interactable ? Color.white : Color.grey;
        print("testttt");
    }

    public void Disable()
    {
        _interactable = false;
        Button.interactable = false;
        transform.DOScale(Vector3.one, TweenDuration);
        _spell.CancelPreview(_currentEntity);
        _infoBubble.Hide();
    }


}
