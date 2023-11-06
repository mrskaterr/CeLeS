using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class LaptopMission : MissionObject,IInteractable
{
    [SerializeField] Transform laptop;
    [SerializeField] List<Transform> points;
    //public GameObject player;

    public bool SetPosition(int index)
    {

            if(index<1)  
                return true;
            else    
            {
                Debug.Log(index);
                laptop.position = points[index].position;
                return false;
            }
    }

    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
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
