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

        await ChooseSpell();
        await SpellCaster.CastSelectedSpell();
    }

    async UniTask<Vector2Int> ChooseDestination()
    {
        Vector2Int output = Vector2Int.zero;

        Vector2Int originTile = _floodFill.GetOriginTile();
        _floodFill.UpdateTileHighlighting(originTile);

        TileAstarNode SelectedTile = null;

        return output;

    }
    async UniTask ChooseSpell()
    {
        _floodFill.ResetTilesHighlighting();
        await CombatUI.Instance.SpellSelectionPanel.SelectEntitySpell(this);
    }
}
