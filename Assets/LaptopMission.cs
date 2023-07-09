using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class LaptopMission : MissionObject,IInteractable
{
    [SerializeField] List<Transform> Points;
    [Networked] public int i {get;set;}

    protected override void OnInteract()
    {
        mission.NextStep();
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    void Start()
    {
        i=Random.Range(0,Points.Count);
        transform.position=Points[i].position;
        // transform.SetParent(Point[i]);
    }
}
