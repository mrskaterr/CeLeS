using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class InteractableHold : MissionObject, IInteractableHold
{
    [SerializeField] EnumItem.Item ItemToNeed;
    [SerializeField] protected float holdTime = 1f;
    public string desc = "Opening ...";
    //public string Description { get; protected set; } = "Opening ...";
    [SerializeField] protected bool saveProgress = false;

    public float percent { get; protected set; } = 0;
    private readonly float interval = .1f;
    private Transform itemToDestroy;
    [SerializeField] private UnityEvent toDo;

    private void ToDo()
    {
        toDo.Invoke();
    }
    public void StartInteract(GameObject @object)
    {
        
        if(@object.GetComponent<Equipment>().isHeHad((int)ItemToNeed)!=null)
        {
            itemToDestroy=@object.GetComponent<Equipment>().isHeHad((int)ItemToNeed);
            StartCoroutine(Holding());
        }    
        else
            Debug.Log("null");
    }

    public void StopInteract()
    {
        StopAllCoroutines();
        if (!saveProgress)
        {
            percent = 0;
        }
    }

    public virtual void OnFill()
    {
        Debug.Log("OnFill");
        ToDo();
        mission.NextStep();
    }

    private IEnumerator Holding()
    {
        while (percent < holdTime)
        {
            yield return new WaitForSeconds(interval);
            percent += interval;
        }
        OnFill();
    }
    public void DestroyUsedItem()
    {
        Destroy(itemToDestroy);
    }
}