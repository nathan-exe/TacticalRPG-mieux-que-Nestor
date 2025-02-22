using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Ce type de noeud represente une case de la tilemap, et permet de faire du
/// pathfinding dedans. le calcul du cout est basé sur la distance entre la case
/// et la cible, ainsi que sur g.
/// </summary>
[Serializable]
public class TileAstarNode : AstarNode
{
    public CombatTile MonoBehaviour;
    public GameObject gameObject => MonoBehaviour.gameObject;
    public Transform transform => MonoBehaviour.transform;
    public Vector2Int pose => gameObject.transform.position.RoundToV2IntXZ();

    public const float gWheight = 2f;

    public override bool isActive()
    {
        return MonoBehaviour.isActiveAndEnabled;
    }

    /// <summary>
    /// ici, f = h+g (comme dans le cour)
    /// </summary>
    /// <returns></returns>
    public override float ComputeCost(AstarNode target)
    {
        float h = Vector2.Distance(((TileAstarNode)target).transform.position, MonoBehaviour.transform.position);
        return h + g * gWheight;
    }

}
