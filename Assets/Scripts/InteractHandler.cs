using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] Transform rayOriginPoint;
    [SerializeField] private float range = 3;
    [SerializeField] private GameObject indicator;

    private Morph morph;

    private IInteractable interactable;

    private void Awake()
    {
        morph = GetComponent<Morph>();
    }

    private void Update()
    {
        Look4Interaction();

        if(interactable != null)
        {
            indicator.SetActive(true);
            if(Input.GetMouseButtonDown(1))
            {
                interactable.Interact(gameObject);
            }
        }
        else
        {
            indicator.SetActive(false);
            if (Input.GetMouseButtonDown(1))
            {
                morph.index = -1;
            }
        }
    }

    private void Look4Interaction()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOriginPoint.position, rayOriginPoint.forward, out hit, range, interactableMask))
        {
            interactable = hit.transform.GetComponent<IInteractable>();
        }
        else { interactable = null; }
    }
}

interface IInteractable//TOIMPROVE: change 4 virtual void
{
    void Interact(GameObject @object);
}