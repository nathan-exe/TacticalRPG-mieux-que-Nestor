using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "new spell",menuName = "Spells")]
public class SpellData : ScriptableObject
{
    
    
    /// <summary>
    /// le nom du spell
    /// </summary>
    public string Name;

    /// <summary>
    /// le cout en mana du spell
    /// </summary>
    public int ManaCost;

    /// <summary>
    /// utilisé pour savoir la taille du canvas quand on dessine le sort dans la window. Le lançeur de sort est placé en 0,0
    /// </summary>
    public Rect Bounds = new Rect(-5, -5, 11, 11); 


    /// <summary>
    /// Tuiles affecté par une capacité, relativement au bonhomme qui la lance
    /// </summary>
    public List<Vector2Int> AffectedTiles = new();

    private void OnValidate()
    {
        Assert.IsTrue(Bounds.xMin<=0 && Bounds.xMax>=0 && Bounds.yMin <= 0 && Bounds.yMax >= 0,"Les bounds sont invalides, sale quiche");

    }
}
