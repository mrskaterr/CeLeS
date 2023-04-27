using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportyzer : MonoBehaviour
{
    [SerializeField] Transform TransformToTeleport;

    void OnTriggerEnter(Collider c)
    {
        c.transform.position=TransformToTeleport.position;
    }
}
