using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindingTriggerMission : MissionObject,IInteractable
{
    [SerializeField] Transform mainTransform;
    [SerializeField] List<Transform> points;
    [SerializeField] private UnityEvent toDo;
    //public GameObject player;
    private void ToDo()
    {
        toDo.Invoke();
        NextTask();
        Debug.Log(gameObject.name);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Morph>())
            ToDo();
    }
    public void SetPosition(int index)
    {
        mainTransform.position = points[index].position;
    }
}
