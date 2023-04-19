using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject Cam;
    [SerializeField] Transform FootPoint;
    [SerializeField] float MouseSensitivity;
    [SerializeField] float LookUpMax ;
    [SerializeField] float LookUpMin ;
    [SerializeField] float Speed ;
    [SerializeField] float JumpForce ;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float RadiusOverlapSphere;
    private Rigidbody _Rigidbody;
    private Vector3 _MoveInput;
    private Quaternion _MouseInput;
    private float CameraX;
    private bool Grounded = true;
    private bool DoubleJump=false;


    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if(Grounded==false && Physics.OverlapSphere(FootPoint.position,RadiusOverlapSphere,GroundLayer).Length>0)
        {
            Grounded = true;
            Debug.Log(Grounded);
            DoubleJump=true;
        }

        _MouseInput =  Quaternion.Euler(0,Input.GetAxis("Mouse X") * MouseSensitivity,0);
    
        CameraX +=-Input.GetAxis("Mouse Y") * MouseSensitivity;
        CameraX = Mathf.Clamp(CameraX,LookUpMin,LookUpMax);

        Cam.transform.localRotation= Quaternion.Euler(CameraX,
                                                    Cam.transform.localRotation.y,
                                                    Cam.transform.localRotation.z);



        _MoveInput = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");

        _Rigidbody.MovePosition(transform.position + _MoveInput.normalized * Time.fixedDeltaTime * Speed);

        _Rigidbody.MoveRotation(_Rigidbody.rotation * _MouseInput);


        if(Input.GetKeyDown(KeyCode.Space) && Grounded)
            Jump();
        else if(Input.GetKeyDown(KeyCode.Space) && !Grounded && DoubleJump)
        {
            Jump();
            DoubleJump=false;
        }

    }
    private void Jump()
    {
        Grounded=false;
        Debug.Log(Grounded);
        _Rigidbody.velocity = new Vector3(_Rigidbody.velocity.x, 0f, _Rigidbody.velocity.z);

        _Rigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }




    // [Header("Movement")]
    // public float moveSpeed;

    // public float groundDrag;

    // public float jumpForce;
    // public float jumpCooldown;
    // public float airMultiplier;
    // bool readyToJump;

    // [HideInInspector] public float walkSpeed;
    // [HideInInspector] public float sprintSpeed;

    // [Header("Keybinds")]
    // public KeyCode jumpKey = KeyCode.Space;

    // [Header("Ground Check")]
    // public float playerHeight;
    // public LayerMask whatIsGround;
    // bool grounded;

    // public Transform orientation;

    // float horizontalInput;
    // float verticalInput;

    // Vector3 moveDirection;

    // Rigidbody rb;

    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    //     rb.freezeRotation = true;

    //     readyToJump = true;
    // }

    // private void Update()
    // {
    //     // ground check
    //     grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

    //     MyInput();
    //     SpeedControl();

    //     // handle drag
    //     if (grounded)
    //         rb.drag = groundDrag;
    //     else
    //         rb.drag = 0;
    // }

    // private void FixedUpdate()
    // {
    //     MovePlayer();
    // }

    // private void MyInput()
    // {
    //     horizontalInput = Input.GetAxisRaw("Horizontal");
    //     verticalInput = Input.GetAxisRaw("Vertical");

    //     // when to jump
    //     if(Input.GetKey(jumpKey) && readyToJump && grounded)
    //     {
    //         readyToJump = false;

    //         Jump();

    //         Invoke(nameof(ResetJump), jumpCooldown);
    //     }
    // }

    // private void MovePlayer()
    // {
    //     // calculate movement direction
    //     moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

    //     // on ground
    //     if(grounded)
    //         rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

    //     // in air
    //     else if(!grounded)
    //         rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    // }

    // private void SpeedControl()
    // {
    //     Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

    //     // limit velocity if needed
    //     if(flatVel.magnitude > moveSpeed)
    //     {
    //         Vector3 limitedVel = flatVel.normalized * moveSpeed;
    //         rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
    //     }
    // }

    // private void Jump()
    // {
    //     // reset y velocity
    //     rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

    //     rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    // }
    // private void ResetJump()
    // {
    //     readyToJump = true;
    // }
}
