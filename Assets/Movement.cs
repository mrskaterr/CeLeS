using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    CharacterInputHandler characterInputHandler;
    public float rotatioX;
    public float rotatioY;
    [SerializeField] Transform FootPoint;
    [SerializeField] float Speed;
    float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float JumpForce ;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float RadiusOverlapSphere;
    [SerializeField] float CrouchSpeed;
    private Rigidbody _Rigidbody;
    public bool _Grounded = false;
    private float _ScaleY=1;
    private float _rouchHeight;
    private float _Length;
    private float CrouchHeight;
    [SerializeField] float MaxDashTime;
    [SerializeField] float DashSpeed ;
    [SerializeField] float DashStoppingSpeed ;
    [SerializeField] float DashResetTime ;
    [SerializeField] float Lock;
    private float currentDashTime;
    private float currentDashResetTime;
    //input
    [HideInInspector]
    public bool isDashing;
    public bool isRunning;
    public bool isJumping;
    [HideInInspector]
    public Vector3 Velocity;
    private Vector3 _MoveInput;
    void Awake()
    {
        characterInputHandler= GetComponent<CharacterInputHandler>();
        currentDashTime = MaxDashTime;

        CrouchHeight = 0.5f * _ScaleY;
        _Length =  _ScaleY - CrouchHeight;
        walkSpeed=Speed;
        _Rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Velocity.x = characterInputHandler.moveInput.x;
        Velocity.z = characterInputHandler.moveInput.y;

        if (_Grounded==false && Physics.OverlapSphere(FootPoint.position,RadiusOverlapSphere,GroundLayer).Length>0)
        {
            _Grounded = true;
            isJumping = false;
        }
        if (isJumping && _Grounded)
        {
            Velocity.y = JumpForce;
            _Grounded = false;
        }
        else
        {
            Velocity.y=_Rigidbody.velocity.y;
        }

        _MoveInput = transform.forward * Velocity.z + transform.right * Velocity.x;

        _Rigidbody.MovePosition(transform.position + _MoveInput.normalized * Time.fixedDeltaTime * Speed);

        _Rigidbody.velocity = Velocity;

        //Dash
        //if (isDashing && currentDashResetTime > DashResetTime)
        //{
        //    currentDashTime = 0.0f;
        //    currentDashResetTime = 0.0f;
        //}
        //if (currentDashTime < MaxDashTime)
        //{
        //    Speed = DashSpeed;
        //    currentDashTime += DashStoppingSpeed;
        //}
        //else
        //{
        //    Speed = walkSpeed;
        //    currentDashResetTime += DashStoppingSpeed;
        //}

        //sprint, kucanie

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    Speed = runSpeed;
        //}
        //else if (Input.GetKey(KeyCode.LeftControl) || (Physics.Raycast(transform.position, Vector3.up, _Length)/*canstand*/))
        //{
        //    transform.localScale = new Vector3(1, CrouchHeight, 1);
        //    Speed = CrouchSpeed;
        //}
        //else
        //{
        //    transform.localScale = new Vector3(1, _ScaleY, 1);
        //    Speed = walkSpeed;
        //}

        //if (!_Grounded)
        //    Speed *= Lock;
    }





    public void Jump()
    {
        _Grounded = false;
    }
}
