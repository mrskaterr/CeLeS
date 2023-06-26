using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taskChanger : MonoBehaviour
{
    [SerializeField] List<GameObject> Task;

    int i;

    void Start()
    {
        i=0;
        Task[i].SetActive(true);
    }
    void Update()
    {
        if(Input.GetButtonDown("X"))
        {
            Task[i].SetActive(false);
            if(Task.Count>i)
            {
                Task[i].SetActive(true);
            }
        }
    }
}
