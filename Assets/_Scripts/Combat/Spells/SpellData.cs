using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "new spell",menuName = "Combat/Spell")]
public class SpellData : ScriptableObject
{
    
    
    /// <summary>
    /// le nom du spell
    /// </summary>
    public string Name;

    [XmlIgnore]
    public Sprite Sprite;
    
    /// <summary>
    /// le cout en mana du spell
    /// </summary>
    public float ManaCost;

    public float Damage;

    public bool IsOccludedByWalls;

    /// <summary>
    /// utilis� pour savoir la taille du canvas quand on dessine le sort dans la window. Le lan�eur de sort est plac� en 0,0
    /// </summary>
    public Rect Bounds = new Rect(-5, -5, 11, 11); 


    /// <summary>
    /// Tuiles affect� par une capacit�, relativement au bonhomme qui la lance
    /// </summary>
    public List<Vector2Int> AffectedTiles = new();

    private void OnValidate()
    {
        Assert.IsTrue(Bounds.xMin<=0 && Bounds.xMax>=0 && Bounds.yMin <= 0 && Bounds.yMax >= 0,"Les bounds sont invalides, sale quiche");

    }
}
