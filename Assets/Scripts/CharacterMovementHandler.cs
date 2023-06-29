using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkTransform
{
    private NetworkRigidbody networkRigidBody;
    private NetworkAnimator networkAnimator;

    [SerializeField] private Camera localCamera;
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private float cameraSens = 1;
    private Movement movement;
    protected override void CopyFromBufferToEngine()
    {
        networkRigidBody.Rigidbody.isKinematic = true;
        networkRigidBody.Rigidbody.detectCollisions = false;

        base.CopyFromBufferToEngine();

        networkRigidBody.Rigidbody.isKinematic = false;
        networkRigidBody.Rigidbody.detectCollisions = true;

    }
    private void Awake()
    {
        movement = GetComponent<Movement>();
        networkRigidBody = GetComponent<NetworkRigidbody>();
        networkAnimator = GetComponent<NetworkAnimator>();
    }

    private void Start()
    {
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
 
            networkRigidBody.WriteVelocity(networkInputData.velocity);

            networkAnimator.SetMoveAnim(networkInputData.velocity.magnitude > 0);

            if (networkInputData.isJumpPressed)
            {
                movement.isJumping=true;
            }
        }
    }
}