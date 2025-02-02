using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object des Entités InGame
/// </summary>
[CreateAssetMenu(fileName = "new spell", menuName = "Combat/Entity")]
public class EntityData : ScriptableObject
{
    public int MaxHP;
    public List<Spell> Spells = new List<Spell>();
}
