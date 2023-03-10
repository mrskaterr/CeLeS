using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTest : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject canv;

    [SerializeField] Task_Hacking hp;
    public void Interact(GameObject @object)
    {
        canv.SetActive(true);
        @object.GetComponent<NetworkCharacterController>().enabled=false;
        @object.GetComponent<CharacterController>().enabled=false;
        @object.GetComponent<CharacterMovementHandler>().enabled=false;

        hp.player=@object;

    }
}