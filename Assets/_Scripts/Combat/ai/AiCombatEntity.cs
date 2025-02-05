using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCombatEntity : CombatEntity
{
    public override async UniTask PlayTurn()
    {
        Vector2Int t = await ChooseDestination();
        await _movement.GoTo(t);

        Spell ChosenSpell = await ChooseSpell();
        if (ChosenSpell != null) await CastSpell(ChosenSpell);
    }

    async UniTask<Vector2Int> ChooseDestination()
    {
        Vector2Int output = Vector2Int.zero;

        Vector2Int originTile = _floodFill.GetOriginTile();
        _floodFill.UpdateTileHighlighting(originTile);

        TileAstarNode SelectedTile = null;

        return output;

    }
    async UniTask<Spell> ChooseSpell()
    {
        _floodFill.ResetTilesHighlighting();
        return await CombatUI.Instance.SpellSelectionPanel.SelectEntitySpell(this);
    }
}
