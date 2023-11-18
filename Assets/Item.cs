using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MissionObject,IInteractable
{
    [SerializeField] Transform defaultPartent;
    [SerializeField] EnumItem.Item item;
    Collider coll;
    private const string interactableLayerName = "Interactable";
    private const string notvisible = "P4";
    private int ID;
    void Start()
    {
        coll=transform.GetComponent<Collider>();
        ID=(int)item;
    }

    void Update()
    {
        if(transform.parent==defaultPartent){}

        else if(transform.GetComponentInParent<Morph>() 
        && transform.GetComponentInParent<Morph>().index==-1)
        {
            transform.position=transform.parent.position;
            this.gameObject.layer=LayerMask.NameToLayer(notvisible);
        }
        else
        {
            SetedefaultPartent();
        }
    }
    protected override void OnInteract(GameObject @object)
    {
        if(@object.GetComponent<Equipment>().itemHolder.childCount==0)
        {
            coll.enabled=false;
            
            @object.GetComponent<Equipment>().Add(transform);
        }
        
    }
    public int GetID()
    {
        return ID;
    }
    public void SetedefaultPartent()
    {
        coll.enabled=true;
        transform.SetParent(defaultPartent);
        this.gameObject.layer=LayerMask.NameToLayer(interactableLayerName);
    }

}
