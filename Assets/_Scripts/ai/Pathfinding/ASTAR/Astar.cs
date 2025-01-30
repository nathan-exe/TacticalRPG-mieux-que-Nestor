using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unity.Jobs;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// ce script implémente l'algo de pathfinding A* de manière générique.
/// </summary>
public class Astar : MonoBehaviour
{
    List<AstarNode> _allNodes = new();

    public Stack<AstarNode> ComputePath(AstarNode from, AstarNode to)
    {
        if(from == to) return new Stack<AstarNode>();

        //reset tous les noeuds du graphe
        if (_allNodes.Count==0) from.AddNeighboursToList_Recursive(ref _allNodes);
        ResetAllNodes(_allNodes);

        List<AstarNode> _openNodes = new();
        AstarNode TargetIfNoPathFound = from; //utilisé si la cible est incaccessible

        //parcourt le premier noeud
        from.parcourir(ref _openNodes,to);
        //tant que la cible n'est pas trouvée
        while (!_openNodes.Contains(to) && _openNodes.Count>0)
        {
            //trouve le voisin le plus proche
            AstarNode best = _openNodes[0];
            foreach (AstarNode node in _openNodes)
            {
                if(node.ComputeCost(to)< best.ComputeCost(to)) best = node;
                if (!TargetIfNoPathFound.isActive()  || node.ComputeCost(to) < TargetIfNoPathFound.ComputeCost(to)) TargetIfNoPathFound = node;
            }
            //parcourt le voisin le plus proche
            best.parcourir(ref _openNodes,to);
        }

        //si la cible est inaccessible, il retourne le chemin vers le noeud le plus proche qu'il ait réussi à trouver
        if (_openNodes.Count == 0 && !_openNodes.Contains(to)) {
            return TargetIfNoPathFound.findPathToBeginning(new Stack<AstarNode>());
        }

        //renvoie le chemin complet
        return to.findPathToBeginning(new Stack<AstarNode>());
    }


    public void ResetAllNodes(List<AstarNode> nodes)
    {
        foreach(AstarNode node in nodes)
        {
            node.resetNode();
        }
    }

}

