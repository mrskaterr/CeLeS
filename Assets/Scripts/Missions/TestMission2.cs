using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMission2 : MissionObject
{
    protected override void OnInteract()
    {
        base.OnInteract();
        Debug.Log("Test: Jadalnia A");
    }
}