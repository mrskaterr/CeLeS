using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVMission : MissionObject
{
  void  OnInteract ()
  {

      GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
      Debug.Log("TV Mission");
      mission.NextStep();
      Debug.Log("+ 5 punktów");
 
  }
}
