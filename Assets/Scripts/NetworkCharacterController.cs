using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
    public float rotationSpeed = 15.0f;
    public float viewVerticalSpeed = 50;

    [Networked]
    [HideInInspector]
    public bool IsGrounded { get; set; }

    [Networked]
    [HideInInspector]
    public Vector3 Velocity { get; set; }

    protected override Vector3 DefaultTeleportInterpolationVelocity => Velocity;

    protected override Vector3 DefaultTeleportInterpolationAngularVelocity => new Vector3(0f, 0f, rotationSpeed);

    public Rigidbody rigidbody { get; private set; }
    public Movement movement { get; private set; }

    protected override void Awake()
    {
        movement=GetComponent<Movement>();
        base.Awake();
        CacheController();
    }

    public override void Spawned()
    {
        base.Spawned();
        CacheController();
    }

    private void CacheController()
    {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();

            Assert.Check(rigidbody != null, $"An object with {nameof(NetworkCharacterControllerPrototype)} must also have a {nameof(Rigidbody)} component.");
        }
    }

    protected override void CopyFromBufferToEngine()
    {
        rigidbody.isKinematic = true;
        rigidbody.detectCollisions = false;

        base.CopyFromBufferToEngine();

        rigidbody.isKinematic = false;
        rigidbody.detectCollisions = true;

    }
    public virtual void Jump(bool ignoreGrounded = false, float? overrideImpulse = null)
    {
        if (IsGrounded || ignoreGrounded)
        {
            var newVel = Velocity;
            newVel.y += overrideImpulse ?? jumpImpulse;
            Velocity = newVel;
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

        rigidbody.MovePosition(moveVelocity * deltaTime);

        Velocity = (transform.position - previousPos) * Runner.Simulation.Config.TickRate;
        IsGrounded = movement._Grounded;
    }

    public void Rotate(float _rotationInput)
    {
        transform.Rotate(0, _rotationInput * Runner.DeltaTime * rotationSpeed, 0);
    }
}