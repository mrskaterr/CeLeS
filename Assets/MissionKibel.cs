using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionKibel : MissionObject,IInteractable
{
    int i;
    void Start()
    {
        i=0;
    }
    protected override void OnInteract(GameObject @object)
    {
        Transform prop=@object.GetComponent<Equipment>().isHeHad((int)EnumItem.Item.Prop);
        if(prop!=null)
        {
            i++;
            prop.GetComponent<Item>().SetedefaultPartent();
            prop.gameObject.SetActive(false);
        }
        if(i>=6)
        {
            Debug.Log("Gówno wiatry");
            mission.NextStep();
        }
    }
}
