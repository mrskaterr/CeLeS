using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class TaskMenager : NetworkBehaviour
{
    
    [SerializeField] List<GameObject> Tasks;
    static GameObject[] Identicator;
    void Start()
    {
        Identicator=new GameObject[Tasks.Count];
        if(Object.HasInputAuthority)
            for(int i=0;i<Tasks.Count;i++)
                Identicator[i]=Tasks[i];
    }

}
