using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHold : MonoBehaviour, IInteractableHold
{
    [SerializeField] protected float holdTime = 1f;
    public string desc = "Opening ...";
    //public string Description { get; protected set; } = "Opening ...";
    [SerializeField] protected bool saveProgress = false;

    public float percent { get; protected set; } = 0;
    private readonly float interval = .1f;

    public void StartInteract(GameObject @object)
    {
        StartCoroutine(Holding());
    }

    public void StopInteract(GameObject @object)
    {
        StopAllCoroutines();
        if (!saveProgress)
        {
            percent = 0;
        }
    }

    public virtual void OnFill(GameObject @object)
    {
        Debug.Log(gameObject.name);
        gameObject.SetActive(false);
    }

    private IEnumerator Holding()
    {
        while (percent < holdTime)
        {
            yield return new WaitForSeconds(interval);
            percent += interval;
        }
        OnFill(gameObject);
    }
}