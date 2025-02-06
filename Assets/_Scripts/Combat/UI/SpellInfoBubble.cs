using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellInfoBubble : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float _tweeningDuration;
    public void ShowSpellInfo(SpellData spell)
    {
        transform.DOScale(Vector3.one, _tweeningDuration);
        _text.text = $"-{spell.ManaCost} MP\n{spell.Damage} DMG";
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, _tweeningDuration);
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }
}
