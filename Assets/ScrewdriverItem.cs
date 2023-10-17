using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverItem : Item
{
    bool isFind;
    void Start()
    {
        isFind=false;
    }
    protected override void OnInteract(GameObject @object)
    {
        transform.GetComponent<Collider>().enabled=false;
        @object.GetComponent<Equipment>().Add(transform);
        if(!isFind)
        {
            mission.NextStep();
            isFind=true;
        }
    }
}
