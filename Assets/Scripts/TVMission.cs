using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVMission : MissionObject,IInteractable
{
    [SerializeField] Rigidbody rb;

    void OnTriggerEnter(Collider other)
    {
        rb.isKinematic=false;
        rb.useGravity=true;
        GetComponent<Collider>().isTrigger=false;
    }
}
