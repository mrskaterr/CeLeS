using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskHandler : MonoBehaviour
{
    [SerializeField] TMP_Text progressTxt;

    private void Start()
    {
        Invoke(nameof(Init), 3);
    }

    private void Init()
    {
        List<MissionData> missions = GameManager.instance.missionManager.missions;
        for (int i = 0; i < missions.Count; i++)
        {
            missions[i].onDone += CheckProgress;
        }
    }

    private void CheckProgress()
    {
        List<MissionData> missions = GameManager.instance.missionManager.missions;
        int c = 0;
        for (int i = 0; i < missions.Count; i++)
        {
            if (missions[i].isDone) { c++; }
        }

        progressTxt.text = $"{c} / {missions.Count}";
    }
}