using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureHandler : NetworkBehaviour
{
    [SerializeField] private LocalCameraHandler cameraHandler;

    private CarryGlobal carryGlobal;
    private HealthSystem healthSystem;

    [Networked(OnChanged = nameof(OnChangeRelease))]
    public bool isFree { get; set; } = true;

    private void Start()
    {
        Invoke(nameof(Init), 2);
    }

    private void Init()
    {
        carryGlobal = GameManager.instance.GetComponent<CarryGlobal>();
        healthSystem = GetComponent<HealthSystem>();
    }

    [Rpc]
    public void RPC_Capture()
    {
        var carry = carryGlobal.GetCarry();
        transform.parent = carry.holdCenter;
        carry.captureHandler = this;
        carry.available = false;
        transform.localPosition = Vector3.zero;
        cameraHandler.ChangePerspective(1);
        isFree = false;
    }

    [Rpc]
    public void RPC_PutDown()
    {
        transform.parent = null;
        transform.position += Vector3.down;
        cameraHandler.ChangePerspective(-1);
    }

    public static void OnChangeRelease(Changed<CaptureHandler> _changed)//TODO: all logic here
    {
        if (_changed.Behaviour.isFree)
        {
            _changed.Behaviour.RPC_PutDown();
        }
    }

    public void Release()
    {
        transform.parent = null;
        cameraHandler.ChangePerspective(-1);
        healthSystem.Restore();
    }
}