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
        Transform prop=@object.GetComponent<Equipment>().FindItem((int)EnumItem.Item.Prop);
        if(prop!=null)
        {
            i++;
            prop.GetComponentInParent<Equipment>().Delete(prop);
            
            prop.gameObject.SetActive(false);
            prop.GetComponent<Item>().SetedefaultPartent();
        }
        if(i>=6)
        {
            Debug.Log("Gówno wiatry");
            mission.NextStep();
        }
    }
}
