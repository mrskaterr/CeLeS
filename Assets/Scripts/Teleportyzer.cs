using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportyzer : MonoBehaviour , IInteractable
{
    
    [SerializeField] Transform TransformToTeleport;

    public void Interact(GameObject @object)
    {
        @object.transform.position=TransformToTeleport.position;
        @object.GetComponent<JobHandler>().VarTask=true;
    }
}
