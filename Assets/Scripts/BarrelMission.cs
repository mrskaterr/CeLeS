using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMission : MissionObject,IInteractable
{
    [SerializeField] float Power;
    [SerializeField] Collider Area;
    Rigidbody rb;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    protected override void OnInteract(GameObject @object)
    {
        rb.AddForce(@object.transform.forward*Power);
    }
    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.name==Area.gameObject.name)//TODO
        {
            rb.constraints=RigidbodyConstraints.FreezeAll;
            mission.NextStep();
            this.enabled=false;
        }
        
    }
}
