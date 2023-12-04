using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderStation : MissionObject
{
    static int iterator=0;
    public void Done()
    {
        iterator++;
        Debug.Log(iterator);
        if(transform.childCount<=iterator)
            NextTask();

    }

}
