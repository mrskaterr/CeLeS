using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionMission : MissionObject,IInteractable
{
    private float interactTime=0;
    private float holdTime=15;
    void Update()
    {
       if(interactTime>=holdTime)
        {
            Debug.Log("Done");
        }

    }


}
