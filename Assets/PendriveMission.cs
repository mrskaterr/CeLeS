using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendriveMission : MonoBehaviour,IInteractable
{
   static int i=0;
    void Update()
    {
        if(i>=3)
        {
            Debug.Log("Done");
            i=0;
        }
    }
    public void Interact(GameObject Object)
    {
        i++;
        GetComponent<Collider>().enabled=false;
    }
}
