using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMission : MissionObject
{
    protected override void OnInteract()
    {
        base.OnInteract();
        Debug.Log("Test: " + mission.title);
    }
}