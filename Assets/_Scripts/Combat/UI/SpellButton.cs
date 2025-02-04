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
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_interactable) return;

        transform.DOScale(Vector3.one * 1.2f, TweenDuration);
        _spell.PreviewSpellEffect(_currentEntity);
        _infoBubble.ShowSpellInfo(_spell);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_interactable) return;

        transform.DOScale(Vector3.one, TweenDuration);
        _spell.CancelPreview(_currentEntity);
        _infoBubble.Hide();
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
