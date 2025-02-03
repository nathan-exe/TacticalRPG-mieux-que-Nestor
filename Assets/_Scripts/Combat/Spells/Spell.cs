using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spell
{
    public SpellData Data;
    public float DamageModifier;

    public void PreviewSpellEffect(CombatEntity owner)
    {
        //@ToDo : preview tiles
        owner.UI.PreviewManaLoss(Data.ManaCost);
    }

    public void CancelPreview(CombatEntity owner)
    {
        owner.UI.CancelManaLossPreview();
        //@ToDo : cancel tiles preview
    }

    public async UniTask Execute(CombatEntity owner)
    {
        //@ToDo : appliquer degats sur toutes les tiles
        foreach (Vector2Int offset in Data.AffectedTiles)
        {
            //Vector2Int TargetTile = owner.transform.position
        }
    }
}
