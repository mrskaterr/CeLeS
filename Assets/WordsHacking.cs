using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class WordsHacking : MissionObject,IInteractable
{
    [SerializeField] GameObject hackingPanel;
    protected override void OnInteract()
    {
        hackingPanel.SetActive(true);
    }
}
