using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    private float x;
    private float z;
    private bool rotateX;
    private float rotationSpeed;

    void Start()
    {
        x = 0.0f;
        z = 0.0f;
        rotationSpeed = 200.0f;
    }

    void FixedUpdate()
    {
            z += Time.deltaTime * rotationSpeed;
            x += Time.deltaTime * 1;
            if (z > 360.0f)
            {
                z = 0.0f;
                rotateX = true;
            }

        transform.localRotation = Quaternion.Euler(x, 0, z);
        if(z<180)transform.position+=new Vector3(20,0,0);
        else transform.position-=new Vector3(20,0,0);
    }
}
