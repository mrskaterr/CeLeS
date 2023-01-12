using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    [SerializeField] GameObject[] audioSteps; 
    private AudioSource lastStep;
    private NetworkCharacterController networkCharacterController;

    [SerializeField] private Camera localCamera;
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private float cameraSens = 1;

    private void Awake()
    {
        networkCharacterController = GetComponent<NetworkCharacterController>();
    }

    private void Start()
    {
        lastStep=audioSteps[Random.Range (1,(audioSteps.Length - 1))].GetComponent<AudioSource>();
        lastStep.Play();
        if (!Object.HasInputAuthority) 
        { 
            localCamera.enabled = false;
            audioListener.enabled = false;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData networkInputData))
        {
            transform.forward = networkInputData.aimForwardVector;//TODO: lerp or something, to animate and look better

            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;

            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterController.Move(moveDirection);

            if(moveDirection!=Vector3.zero && !lastStep.isPlaying)
            {
                lastStep=audioSteps[Random.Range (0,audioSteps.Length)].GetComponent<AudioSource>();
                lastStep.Play();
            }

            if (networkInputData.isJumpPressed)
            {
                networkCharacterController.Jump();
            }
        }
    }
}