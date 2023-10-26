using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class LaptopMission : MissionObject,IInteractable
{
    LayerMask layerMask ;
    [SerializeField] MissionManager manager;
    [SerializeField] Transform laptop;
    [SerializeField] List<Transform> points;
    //public GameObject player;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        layerMask = LayerMask.NameToLayer("Interactable");
        laptop.position=points[manager.LaptopPositionIndex].position;
        // mission.Init();
        //base.Enable();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Morph>())
        {
            Debug.Log("LaptopMission");
            mission.NextStep();
            GetComponent<Collider>().enabled=false;

        }
    }

}
