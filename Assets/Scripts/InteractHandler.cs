using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] Transform rayOriginPoint;
    [SerializeField] private float range = 3;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera tpsCam;
    [SerializeField] private LocalCameraHandler cameraHandler;
    private Morph morph;

    private IInteractable interactable;

    private Vector3 screenCenter = new Vector3(.5f, .5f, 0);

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
                //morph.index = -1;
            }
        }
    }

    private void Look4Interaction()
    {
        Ray ray;
        Camera cam = fpsCam;
        if (!cameraHandler.fps)
        {
            cam = tpsCam;
            ray = new Ray(rayOriginPoint.position, cam.transform.forward);
        }
        else
        {
            ray = cam.ViewportPointToRay(screenCenter);
        }
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, interactableMask))
        {
            interactable = hit.transform.GetComponent<IInteractable>();
        }
        else { interactable = null; }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(new Ray(rayOriginPoint.position, tpsCam.transform.forward));
    //}
}

interface IInteractable//TOIMPROVE: change 4 virtual void
{
    void Interact(GameObject @object);
}