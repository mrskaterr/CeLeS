using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    public bool canSneak = false;
    
    [SerializeField] LocalCameraHandler cameraHandler;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 viewInput = Vector2.zero;
    private bool jumpInput = false;
    private bool fireInput = false;//TODO: interact
    private float speedStep = 0;
    private bool sneakyInput = false;

    private Vector3 sneakRot = Vector3.zero;

    private CharacterMovementHandler characterMovementHandler;
<<<<<<< HEAD
    private void Awake()
    {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
=======
    private void Awake()
    {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
>>>>>>> Dave
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//TOIMPROVE: Utils
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!characterMovementHandler.Object.HasInputAuthority) { return; }

        viewInput.x = Input.GetAxis("Mouse X");
        viewInput.y = Input.GetAxis("Mouse Y") * -1;

        moveInput.x = Input.GetAxis("Horizontal");//TODO: new input system
        moveInput.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            fireInput = true;
        }

        if (Input.GetButtonDown("Fire2") && canSneak)
        {
            sneakRot = cameraHandler.transform.forward;
            sneakyInput = true;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            sneakyInput = false;
        }

        moveInput.y += speedStep;

        cameraHandler.SetViewInput(viewInput);
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        if (sneakyInput)
        {
            networkInputData.aimForwardVector = sneakRot;
        }
        else
        {
            networkInputData.aimForwardVector = cameraHandler.transform.forward;
        }

        networkInputData.movementInput = moveInput;

        networkInputData.isJumpPressed = jumpInput;

        networkInputData.isFirePressed = fireInput;

        jumpInput = false;
        fireInput= false;

        return networkInputData;
    }
}