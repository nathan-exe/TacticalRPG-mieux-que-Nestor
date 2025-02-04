using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatTileData",menuName = "Combat/CombatTileData")]
public class CombatTileData : ScriptableObject
{
    public Material Material_Danger;
    public Material Material_Clickable;
    public Material Material_Default;
}
