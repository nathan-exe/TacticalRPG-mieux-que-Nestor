using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum NodeState { open, closed, notVisitedYet }

/// <summary>
/// cette classe abstraite représente n'importe quel noeud pouvant être parcouru par l'algo Astar.
/// </summary>
[Serializable]
public abstract class AstarNode
{
    [SerializeField] public List<AstarNode> Neighbours = new();

    //algo
    [HideInInspector] public NodeState VisitedState = NodeState.notVisitedYet;
    public AstarNode previousNode { get; protected set; } = null;

    /// <summary>
    /// le nombre de noeuds parcourus depuis le début du chemin
    /// </summary>
    [HideInInspector] public int g; 

    public abstract float ComputeCost(AstarNode target);
    public abstract bool isActive();

    public event Action OnBeforeActivationCheck;

    /// <summary>
    /// remet le noeud à 0 : non visité et blanc
    /// </summary>
    public virtual void resetNode()
    {
        g = 0;
        VisitedState = NodeState.notVisitedYet;
        previousNode = null;
    }

    /// <summary>
    /// ouvre le noeud
    /// </summary>
    public void open()
    {
        VisitedState = NodeState.open;
    }


    /// <summary>
    /// ferme le noeud
    /// </summary>
    public void Close()
    {
        VisitedState = NodeState.closed;
    }

    //algo
    /// <summary>
    /// se ferme et ouvre tous ses voisins
    /// </summary>
    /// <param name="openNodes"></param>
    /// <param name="target"></param>
    public void parcourir(ref List<AstarNode> openNodes,AstarNode target)
    {
        Close();
        openNodes.Remove(this);

        foreach (AstarNode n in Neighbours)
        {
            if (n.VisitedState == NodeState.notVisitedYet)
            {
                n.previousNode = this;
                n.OnBeforeActivationCheck?.Invoke();

                if (n.isActive())
                {
                    openNodes.Add(n);
                    n.open();
                    n.g = g + 1;
                }
            }
        }
    }

    /// <summary>
    /// retrouve le chemin vers le premier noeud de manière récursive
    /// </summary>
    /// <param name="l"></param>
    /// <returns></returns>
    public Stack<AstarNode> findPathToBeginning(Stack<AstarNode> l)
    {
        if (previousNode == null) return l;
        l.Push(this);
        return previousNode.findPathToBeginning(l);
    }

    /// <summary>
    /// permet de trouver tous les noeuds appartenant au même graphe que ce noeud ci.
    /// </summary>
    /// <param name="list"></param>
    public void AddNeighboursToList_Recursive (ref List<AstarNode> list)
    {
        list.Add(this);
        foreach ( AstarNode neighbour in Neighbours)
        {
            if(!list.Contains(neighbour)) neighbour.AddNeighboursToList_Recursive(ref list);
        }
    }

}
