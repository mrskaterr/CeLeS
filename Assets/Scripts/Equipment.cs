using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Equipment : MonoBehaviour
{
    public Transform itemHolder;
[SerializeField] TMP_Text nameItem;
    void Update()
    {
        //nameItem.text=itemHolder.GetChild(0).name;
        if(itemHolder.childCount==0)
            nameItem.text="";
    }
    public Transform isHeHad(int index)
    {
        int count=itemHolder.childCount;
        if (count==1 && itemHolder.GetChild(0).GetComponent<Item>().Index()==index)
            return itemHolder.GetChild(0);
        else
        {       
            Debug.Log("equipment  count:"+count);
            return null;
        }
    }

    public void Add(Transform t)
    {
        if(itemHolder.childCount==0)
            t.SetParent(itemHolder);
            nameItem.text=t.name;    
    }

}
