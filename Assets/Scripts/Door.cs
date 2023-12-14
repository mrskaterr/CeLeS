using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{   
    [SerializeField] AudioSource OpenSound;
    [SerializeField] AudioSource CloseSound;
    private Animator animator;
    void Awake()
    {
        animator=GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    { 
        DoorAnimation(true,CloseSound,OpenSound);
    } 

    void OnTriggerExit(Collider other)
    {
        DoorAnimation(false,OpenSound,CloseSound);
    }
    private void DoorAnimation(bool isOpenAnimation,AudioSource StopSound,AudioSource PlaySound)
    {
        StopSound.Stop();
        animator.SetBool("open", isOpenAnimation);
        PlaySound.Play();
    }
}