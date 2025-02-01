using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInteraction : MonoBehaviour
{
    Iinteractable SelectedInteractable;

    [Header("References")]
    [SerializeField] NavMeshAgent _navMeshAgent;


    /// <summary>
    /// selectionne un Interacible. Le joueur interragira automatiquement
    /// avec lui quand il s'en sera assez rapproché.
    /// </summary>
    /// <param name="interactable"></param>
    public void SelectInteractable(Iinteractable interactable)
    {
        SelectedInteractable = interactable;
        SelectedInteractable.OnSelected();
    }

    /// <summary>
    /// déselectionne l'interacible préalablement sélectionné.
    /// </summary>
    public void ClearSelection()
    {
        if(SelectedInteractable==null)return;

        SelectedInteractable.OnUnselected();
        SelectedInteractable = null;
    }

    private void Update() 
    {
        //pas d'event OnDestinationReached sur le navmesh agent...
        if (SelectedInteractable!=null &&
            _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance
            && (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude <= 0.001f)
            ) 
        {
            SelectedInteractable.InteractWith();
            ClearSelection();
        }
    }

}
