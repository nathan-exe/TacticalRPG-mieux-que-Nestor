using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerOverworldController : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public NavMeshAgent agent;
    [SerializeField] PlayerInteraction _interaction;

    public bool HasComeToDestination;

    private void Start()
    {
        GameState.GetPlayer().transform.position = GameState.TeamPosition;
    }
    void Update()
    {
        //clic souris
        if (Input.GetMouseButtonDown(0))
        {
            //raycast
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //deplacements
                agent.SetDestination(hit.point);

                //selection des interactibles
                if (hit.collider.gameObject.TryGetComponent<Iinteractable>(out Iinteractable interactable))
                {
                    _interaction.SelectInteractable(interactable);
                }
                else
                    _interaction.ClearSelection();
            }

            if (gameObject.transform.position == hit.point)
            {
                HasComeToDestination = true;
            }
        }
    }
}
