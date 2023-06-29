using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class LocalCameraHandler : MonoBehaviour
{
    public bool fps = true;
    [SerializeField] private Transform anchorPointFPS;
    [SerializeField] private Transform anchorPointTPS;
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private Transform TPSOriginTransform;
    [SerializeField] private NetworkRigidbody networkRb;
    [SerializeField] private NetworkAnimator networkAnimator;

    private Camera cam;
    private float cameraRotationX = 0;
    private float cameraRotationY = 0;
    private Vector2 viewInput;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        if (cam.enabled)
        {
            cam.transform.parent = null;
        }
    }

    private void LateUpdate()
    {
        if (anchorPoint == null || !cam.enabled) { return; }

        if (fps)
        {
            cam.transform.position = anchorPoint.position;

            cameraRotationX += viewInput.y * Time.deltaTime * networkRb.ReadRigidbodyRotation().x;
            cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

            cameraRotationY += viewInput.x * Time.deltaTime * networkRb.ReadAngularVelocity().magnitude;

            cam.transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);
            if (networkAnimator != null)
            {
                float tmp = cameraRotationX;
                tmp += 90;
                tmp /= 180f;
                networkAnimator.SetAimTargetPos(tmp);
            }
        }
        else
        {
            cameraRotationX += viewInput.y * Time.deltaTime * networkRb.ReadRigidbodyRotation().x;
            cameraRotationX = Mathf.Clamp(cameraRotationX, -60, 30);

            cameraRotationY += viewInput.x * Time.deltaTime * networkRb.ReadAngularVelocity().magnitude;

            TPSOriginTransform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);
            cam.transform.position = anchorPoint.position;
            cam.transform.LookAt(TPSOriginTransform.position);
        }
    }

    public void SetViewInput(Vector2 _viewInput)
    {
        viewInput = _viewInput;
    }

    public void ChangePerspective(int _index)
    {
        if(_index == -1)
        {
            anchorPoint = anchorPointFPS;
            fps = true;
            cameraRotationX = 0;
        }
        else
        {
            anchorPoint = anchorPointTPS;
            fps = false;
        }
    }
}