using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    private NetworkCharacterController networkCharacterController;
    private Vector2 viewInput;
    private float cameraRotationX = 0;

    [SerializeField] private Camera localCamera;

    private void Awake()
    {
        networkCharacterController = GetComponent<NetworkCharacterController>();
    }

    private void Start()
    {
        if (!Object.HasInputAuthority) { localCamera.gameObject.SetActive(false); }
    }

    private void Update()
    {
        cameraRotationX -= viewInput.y * Time.deltaTime * networkCharacterController.viewVerticalSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

        localCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData networkInputData))
        {
            networkCharacterController.Rotate(networkInputData.rotationInput);

            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterController.Move(moveDirection);
        }
    }

    public void SetViewInput(Vector2 _viewInput)
    {
        viewInput = _viewInput;
    }
}