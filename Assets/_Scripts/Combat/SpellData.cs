using System.Collections.Generic;
using UnityEngine;

public class SpellData : ScriptableObject
{
    public int ManaCost;

    private List<Vector2Int> _affectedTiles = new(); //Tuiles affecté par une capacité
}
