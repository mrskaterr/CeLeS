using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class LaptopMission : MissionObject,IInteractable
{
    [SerializeField] Transform laptop;
    [SerializeField] List<Transform> points;
    [SerializeField] Collider nextMission;
    public bool isDone;
    //public GameObject player;
    [Networked] public int i {get;set;}

    protected override void OnInteract()
    {
        Debug.Log("LaptopMission");
        isDone=true;
        nextMission.enabled=true;
        gameObject.GetComponent<Collider>().enabled=false;
        mission.NextStep();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    void Start()
    {
        i=Random.Range(0,points.Count);
        laptop.position=points[i].position;
        // transform.SetParent(Point[i]);
    }
}
