using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new spell",menuName = "Spells")]
public class SpellData : ScriptableObject
{
    
    public readonly Rect Bounds =  new Rect(-5,-5,11,11); //utilisé pour savoir la taille du canvas quand on dessine le sort dans la window

    /// <summary>
    /// le nom du spell
    /// </summary>
    public string Name;

    /// <summary>
    /// le cout en mana du spell
    /// </summary>
    public int ManaCost;

    /// <summary>
    /// Tuiles affecté par une capacité, relativement au bonhomme qui la lance
    /// </summary>
    public List<Vector2Int> AffectedTiles = new(); 


}
