using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class WordsHacking : MissionObject,IInteractable
{
    [SerializeField] GameObject hackingPanel;
    [SerializeField] HackingMission hackingMission;
    public bool isDone=false;
    protected override void OnInteract(GameObject @object)
    {
        hackingPanel.SetActive(true);
        hackingMission.player=@object;
    }
}
