using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Transform itemHolder;
    private List<Transform> Items=new();

    public Transform FindItem(int index)
    {
        for(int i=0;i<itemHolder.childCount;i++)
            if(itemHolder.GetChild(i).GetComponent<Item>().Index()==index)
                return itemHolder.GetChild(i);
                
        Debug.Log("nullfind");
        return null;
    }

    public void Add(Transform t)
    {
        t.SetParent(itemHolder);
        Items.Add(t);
    }
    public void Delete(Transform t)
    {
        Items.Remove(t);
    }

}
    // public Transform FindItemInEquipment(EnumItem.Item enumItem)
    // {
    //     for(int i=0;i<Items.Count;i++)
    //         if(Items[i].GetComponent<Item>().Index()==(int)enumItem)
    //             return Items[i];

    //     return null;
    // }
