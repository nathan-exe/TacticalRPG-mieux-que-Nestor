using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object des Entités InGame
/// </summary>
public class EntityData : ScriptableObject
{
    public int MaxHP;
    private List<Spell> _spells = new List<Spell>();
}
