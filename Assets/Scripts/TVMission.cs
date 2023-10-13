using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVMission : MissionObject
{
  [SerializeField] Collider nextPart;
  void  OnCollisionEnter (Collision collision)
  {
    if(collision.gameObject.GetComponent<Morph>())
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Debug.Log("TV Mission");
        if(nextPart!=null)
          nextPart.enabled=true;
        mission.NextStep();
    }
    if(collision.gameObject.GetComponent<BarrelMission>())
    {
      Debug.Log("+ 5 punktów");
      this.enabled=false;    
    }
  }
}
