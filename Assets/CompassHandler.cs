using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassHandler : MonoBehaviour
{
    [SerializeField] private Transform compassObject;
    [SerializeField] private float turnSpeed = .1f;

    private Quaternion goal;

    private void Start()
    {
        goal = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        compassObject.rotation = Quaternion.Slerp(compassObject.rotation, goal, turnSpeed);
    }
}