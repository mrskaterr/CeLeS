using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMission3 : MissionObject
{
    protected override void OnInteract()
    {
        base.OnInteract();
        Debug.Log("Test: Jadalnia B");
    }
}