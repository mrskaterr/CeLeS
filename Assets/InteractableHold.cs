using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHold : MissionObject, IInteractableHold
{
    [SerializeField] EnumItem.Item ItemToNeed;
    [SerializeField] protected float holdTime = 1f;
    public string desc = "Opening ...";
    //public string Description { get; protected set; } = "Opening ...";
    [SerializeField] protected bool saveProgress = false;

    public float percent { get; protected set; } = 0;
    private readonly float interval = .1f;
    [SerializeField] Collider nextPart;
    public void StartInteract(GameObject @object)
    {
        if(@object.GetComponent<Equipment>().FindItem((int)ItemToNeed)!=null)
            StartCoroutine(Holding());
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
        if(nextPart!=null)
            nextPart.enabled=true;
        GetComponent<Collider>().enabled=false;
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
}