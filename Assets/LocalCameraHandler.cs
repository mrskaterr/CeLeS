using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraHandler : MonoBehaviour
{
    [SerializeField] private Transform anchorPointFPS;
    [SerializeField] private Transform anchorPointTPS;
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private NetworkCharacterController networkCC;

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

        cam.transform.position = anchorPoint.position;

        cameraRotationX += viewInput.y * Time.deltaTime * networkCC.viewVerticalSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

        cameraRotationY += viewInput.x * Time.deltaTime * networkCC.rotationSpeed;

        cam.transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);
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
        }
        else
        {
            anchorPoint = anchorPointTPS;
        }
    }
}