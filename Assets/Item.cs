using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MissionObject,IInteractable
{
    [SerializeField] Transform defaultPartent;
    [SerializeField] EnumItem.Item item;
    private int index;
    void Start()
    {
        index=(int)item;
    }

    void Update()
    {
        if(transform.parent==defaultPartent){}

        else if(transform.GetComponentInParent<Morph>().index==-1)
        {
            transform.position=transform.parent.position;
        }
        else
        {
            transform.GetComponent<Collider>().enabled=true;
            transform.SetParent(defaultPartent);
        }
    }
    protected override void OnInteract(GameObject @object)
    {
        transform.GetComponent<Collider>().enabled=false;
        @object.GetComponent<Equipment>().Add(transform);

    }
    public int Index()
    {
        return index;
    }

}
