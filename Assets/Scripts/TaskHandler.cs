using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskHandler : MonoBehaviour
{
    [SerializeField] TMP_Text progressTxt;
    [SerializeField] TMP_Text toDo;
    private int rnd;

    private void Start()
    {
        rnd = Random.Range(0,5);
        Invoke(nameof(Init), 3);
    }

    void Update()
    {
        if(toDo)toDo.text=GameManager.instance.missionManager.rooms[0].missions[0].currentStep.description;    
        
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