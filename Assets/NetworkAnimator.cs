using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAnimator : MonoBehaviour
{
    [SerializeField] private BodyType bodyType;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bodyAimTarget;

    private NetworkCharacterController controller;

    private const string move_ParamName = "isMoving";
    private const string sprint_ParamName = "isSprinting";
    private const string grounded_ParamName = "isGrounded";

    private void Awake()
    {
        controller = GetComponent<NetworkCharacterController>();
    }

    private void Update()
    {
        animator.SetBool(grounded_ParamName, controller.IsGrounded);
    }

    public void SetMoveAnim(bool _isMoving)
    {
        animator.SetBool(move_ParamName, _isMoving);
    }

    public void SetAimTargetPos(float _height)
    {
        if(bodyType == BodyType.Human)
        {
            float newVal = (1f - Mathf.Clamp01(_height)) * 2f - 1f;
            bodyAimTarget.transform.localPosition = new Vector3(bodyAimTarget.transform.localPosition.x, newVal, bodyAimTarget.transform.localPosition.z);
        }
    }

    private enum BodyType { Human, Blob }
}