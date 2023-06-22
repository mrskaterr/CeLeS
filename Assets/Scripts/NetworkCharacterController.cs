using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[OrderBefore(typeof(NetworkTransform))]
[DisallowMultipleComponent]
public class NetworkCharacterController : NetworkTransform
{
    [Header("Character Controller Settings")]
    public float gravity = -20.0f;
    public float jumpImpulse = 8.0f;
    public float acceleration = 10.0f;
    public float braking = 10.0f;
    public float maxSpeed = 2.0f;
    public float runSpeed = 10f;
    public float rotationSpeed = 15.0f;
    public float viewVerticalSpeed = 50;
    public float MaxDashTime=5f;
    public float DashSpeed=10f;
    public float DashStoppingSpeed=0.1f;
    public float DashResetTime=5f;
    private float currentDashTime;
    private float currentDashResetTime;
    float walkSpeed;
    private bool DoubleJump = true;
    [Networked]
    [HideInInspector]
    public bool IsGrounded { get; set; }
    public bool IsDash;
    public bool IsRuning;

    [Networked]
    [HideInInspector]
    public Vector3 Velocity { get; set; }

    protected override Vector3 DefaultTeleportInterpolationVelocity => Velocity;

    protected override Vector3 DefaultTeleportInterpolationAngularVelocity => new Vector3(0f, 0f, rotationSpeed);

    public CharacterController Controller { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        CacheController();
        currentDashTime = MaxDashTime;
        walkSpeed=maxSpeed;
    }
    public override void FixedUpdateNetwork()
    {
        //Dash
        if (IsDash && currentDashResetTime>DashResetTime)
        {
            currentDashTime = 0.0f;
            currentDashResetTime= 0.0f;
            
            IsDash=false;
        }
        if (currentDashTime < MaxDashTime)
        {
            maxSpeed=DashSpeed;
            currentDashTime += DashStoppingSpeed;
        }
        else
        {
            //maxSpeed=walkSpeed;
            currentDashResetTime += DashStoppingSpeed;
        }

        //Sprint
	    if(IsRuning)
        {
            Debug.Log(2);
		    maxSpeed = runSpeed;
	    }
        // else
        // {
        //     maxSpeed = walkSpeed;
        // }
    }
    public override void Spawned()
    {
        base.Spawned();
        CacheController();
    }

    private void CacheController()
    {
        if (Controller == null)
        {
            Controller = GetComponent<CharacterController>();

            Assert.Check(Controller != null, $"An object with {nameof(NetworkCharacterControllerPrototype)} must also have a {nameof(CharacterController)} component.");
        }
    }

    protected override void CopyFromBufferToEngine()
    {
        Controller.enabled = false;

        base.CopyFromBufferToEngine();

        Controller.enabled = true;
    }
    public virtual void Jump(bool ignoreGrounded = false, float? overrideImpulse = null)
    {
        
        if (IsGrounded || ignoreGrounded)
        {
            var newVel = Velocity;
            newVel.y += overrideImpulse ?? jumpImpulse;
            Velocity = newVel;
        }
        else if(DoubleJump)
        {
            var newVel = Velocity;
            newVel.y += overrideImpulse ?? jumpImpulse;
            Velocity = newVel;

            DoubleJump=false;
        }
    }
    public virtual void Move(Vector3 direction)
    {
        var deltaTime = Runner.DeltaTime;
        var previousPos = transform.position;
        var moveVelocity = Velocity;

        direction = direction.normalized;

        if (IsGrounded && moveVelocity.y < 0)
        {
            moveVelocity.y = 0f;
        }

        moveVelocity.y += gravity * Runner.DeltaTime;

        var horizontalVel = default(Vector3);
        horizontalVel.x = moveVelocity.x;
        horizontalVel.z = moveVelocity.z;

        if (direction == default)
        {
            horizontalVel = Vector3.Lerp(horizontalVel, default, braking * deltaTime);
        }
        else
        {
            horizontalVel = Vector3.ClampMagnitude(horizontalVel + direction * acceleration * deltaTime, maxSpeed);
        }

        moveVelocity.x = horizontalVel.x;
        moveVelocity.z = horizontalVel.z;

        Controller.Move(moveVelocity * deltaTime);

        Velocity = (transform.position - previousPos) * Runner.Simulation.Config.TickRate;
        IsGrounded = Controller.isGrounded;
        if(IsGrounded)
            DoubleJump=true;
    }

    public void Rotate(float _rotationInput)
    {
        transform.Rotate(0, _rotationInput * Runner.DeltaTime * rotationSpeed, 0);
    }
}