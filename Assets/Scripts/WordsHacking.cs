using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class WordsHacking : MissionObject,IInteractable
{
    public GameObject player;
    [SerializeField] GameObject hackingPanel;
    [SerializeField] HackingMission hackingMission;
    [SerializeField] Transform PendriveWirus;
    [SerializeField] Transform EmptyPendrive;
    public bool isDone=false;
    protected override void OnInteract(GameObject @object)
    {
        player = @object.gameObject;
        EmptyPendrive=@object.GetComponent<Equipment>().FindItem((int)EnumItem.Item.EmptyPendrive);
        if(EmptyPendrive!=null)
        {
            @object.GetComponent<CharacterInputHandler>().enabled=false;
            hackingPanel.SetActive(true);
            hackingMission.player=@object;
        }

    }
    public void done()
    {
        player.GetComponent<Equipment>().Delete(EmptyPendrive);
        Destroy(EmptyPendrive.gameObject);
        PendriveWirus.gameObject.SetActive(true);
        player.GetComponent<Equipment>().Add(PendriveWirus);
        mission.NextStep();
    }
}
