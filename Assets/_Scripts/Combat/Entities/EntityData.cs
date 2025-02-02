using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object des Entit�s InGame
/// </summary>
[CreateAssetMenu(fileName = "new spell", menuName = "Combat/Entity")]
public class EntityData : ScriptableObject
{
    public int MaxHP;
    public List<Spell> Spells = new List<Spell>();
}
