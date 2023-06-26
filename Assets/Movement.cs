using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //[SerializeField] Transform Cam;
    [SerializeField] Transform FootPoint;
    [SerializeField] float MouseSensitivity;
     [SerializeField] float LookUpMax = 90;
     [SerializeField] float LookUpMin =-90;
    [SerializeField] float Speed;
    //[SerializeField]
    float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float JumpForce ;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float RadiusOverlapSphere;
    [SerializeField] float CrouchSpeed;
   
    private Rigidbody _Rigidbody;
    private Vector3 _MoveInput;
    public Quaternion Rotation=Quaternion.Euler(Vector3.zero);
    public Vector2 mouseInput;
    public bool _Grounded = true;
    private bool _DoubleJump=false;
    private float _CameraX;
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
    public bool JumpInput;
    [HideInInspector]
    public Vector3 MoveInput;
    void Start()
    {
        currentDashTime = MaxDashTime;

        CrouchHeight = 0.5f * _ScaleY;
        _Length =  _ScaleY - CrouchHeight;
        walkSpeed=Speed;
        _Rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {   
        // Cam.localRotation = Quaternion.Euler(_CameraX, Cam.localRotation.y, Cam.localRotation.z);

        //Rotation =  Quaternion.Euler(0,Input.GetAxis("Mouse X") * MouseSensitivity,0);
        //Rotation =  Quaternion.Euler(Input.GetAxis("Mouse X") * MouseSensitivity,0,0);
        // mouseInput.y = Input.GetAxis("Mouse Y");
        // mouseInput.x = Mathf.Clamp(mouseInput.x - Input.GetAxis("Mouse X") * MouseSensitivity, LookUpMin, LookUpMax);
        _MoveInput = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
        MoveInput = _MoveInput;

        //Dash
        if (Input.GetKey(KeyCode.Z) &&  currentDashResetTime>DashResetTime)
        {
            currentDashTime = 0.0f;
            currentDashResetTime= 0.0f;
        }
        if (currentDashTime < MaxDashTime)
        {
            Speed=DashSpeed;
            currentDashTime += DashStoppingSpeed;
        }
        else
        {
            Speed=walkSpeed;
            currentDashResetTime += DashStoppingSpeed;
        }

        //Sprint, kucanie

	    if(Input.GetKey(KeyCode.LeftShift)) 
        {
		    Speed = runSpeed;
	    }
        else if(Input.GetKey(KeyCode.LeftControl) || (Physics.Raycast( transform.position , Vector3.up, _Length)/*CanStand*/) ) 
        {
	        transform.localScale = new Vector3(1,CrouchHeight,1);
	        Speed = CrouchSpeed;
        }
        else
        {
            transform.localScale = new Vector3(1,_ScaleY,1);
	        Speed = walkSpeed;
        }

  
        ///JUMP
        if(_Grounded==false && Physics.OverlapSphere(FootPoint.position,RadiusOverlapSphere,GroundLayer).Length>0)
        {
            _Grounded = true;
            _DoubleJump = true;
        }
        if(Input.GetKeyDown(KeyCode.Space) && _Grounded)
        {
            JumpInput=true;
            Jump();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !_Grounded && _DoubleJump)
        {
            JumpInput=true;
            Jump();
            _DoubleJump=false;
        }
    }
    
    void FixedUpdate()
    {
        if(!_Grounded)
            Speed*=Lock;
        
        //Rotation=Quaternion.Euler(mouseInput.y, mouseInput.x, 0f);
        _Rigidbody.MovePosition(transform.position + _MoveInput.normalized * Time.fixedDeltaTime * Speed);
        _Rigidbody.MoveRotation(_Rigidbody.rotation * Rotation);
    } 

    public void Jump()
    {
        _Grounded=false;
        _Rigidbody.velocity = new Vector3(_Rigidbody.velocity.x, 0f, _Rigidbody.velocity.z);
        _Rigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }
}
