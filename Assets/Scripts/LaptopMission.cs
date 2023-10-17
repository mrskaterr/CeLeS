using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class LaptopMission : MissionObject,IInteractable
{
    LayerMask layerMask ;
    
    [SerializeField] Transform laptop;
    [SerializeField] List<Transform> points;
    //public GameObject player;
    [Networked] public int i {get;set;}
    [Rpc(RpcSources.All, RpcTargets.All)]
    void Start()
    {
        layerMask =LayerMask.NameToLayer("Interactable");
        // mission.Init();
        //base.Enable();
        i=Random.Range(0,points.Count);
        laptop.position=points[i].position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(gameObject.layer==layerMask && other.gameObject.GetComponent<Morph>())
        {
            Debug.Log("LaptopMission");
            mission.NextStep();
            GetComponent<Collider>().enabled=false;

        }
    }

}
