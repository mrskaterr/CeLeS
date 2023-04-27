using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public virtual void Interacting(GameObject whoInteracts = null)
    {
        Debug.Log(gameObject.name);
    }
}