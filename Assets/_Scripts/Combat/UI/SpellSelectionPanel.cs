using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class SpellSelectionPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<SpellButton> _buttons;
    [SerializeField] CanvasGroup _canvasGroup;

    int _selectedSpell = -1;

    private void Start()
    {
        transform.localScale = Vector3.one;
        _canvasGroup.alpha = 0;
    }

    public async UniTask<Spell> SelectEntitySpell(CombatEntity Entity)
    {
        Assert.IsTrue(Entity.Data.Spells.Count == 3 , "Il devrait y avoir 3 spells par personnage jouable");
        _selectedSpell = -1;

        SetUpButtons(Entity);
        transform.DOScale(Vector3.one, .15f);
        _canvasGroup.DOFade(1, .15f);

        while (_selectedSpell == -1)
        {
            await UniTask.Yield();
            Debug.Log("Waiting For Spell button click");
        }

        DisableButtons();

        transform.DOScale(Vector3.zero, .15f);
        _canvasGroup.DOFade(0, .15f);

        return Entity.Data.Spells[_selectedSpell];
        //return 0;
    }

    
    void SetUpButtons(CombatEntity entity)
    {
        for (int i = 0; i < entity.Data.Spells.Count; i++) 
        {
            _buttons[i].Initialize(entity, i);

            int su = i;
            _buttons[i].Button.onClick.RemoveAllListeners();
            _buttons[i].Button.onClick.AddListener(() => _selectedSpell = su);
        }
    }

    private void DisableButtons()
    {
        for (int i = 0; i < 3; i++)
        {
            _buttons[i].Disable();
        }
    }

}
