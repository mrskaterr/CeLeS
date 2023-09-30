using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MissionObject,IInteractable
{
    [SerializeField] Transform defaultPartent;
    int index = 1;
    void Update()
    {
        if(transform.parent==defaultPartent){}

        else if(transform.parent.parent.parent.GetComponent<Morph>().index==-1)
        {
            transform.position=transform.parent.position;
        }
        else
        {
            transform.SetParent(defaultPartent);
        }
    }
    protected override void OnInteract(GameObject @object)
    {
        transform.SetParent(@object.GetComponent<InteractHandler>().itemHolder);
    }

}
