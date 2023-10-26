using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : FusionStatsBillboard
{
    private void Start()
    {
        Camera = CamerasHolder.GetActiveCamera();
    }
}