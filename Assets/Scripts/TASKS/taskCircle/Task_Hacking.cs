using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Hacking : TaskLogic
{
    [SerializeField] GameObject hackPanel;
    public GameObject player;

    public override void AllUHave2Do(GameObject _player)
    {
        if (job.active)
        {

        }
    }

    private void Update()
    {
        if (hackPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Q)) 
        {
            player.GetComponent<NetworkCharacterController>().enabled=true;
            player.GetComponent<CharacterController>().enabled=true;
            player.GetComponent<CharacterMovementHandler>().enabled=true;;
            hackPanel.SetActive(false);
        }
        else if(!job.active)
        {
            StartCoroutine(Complete());
        }
    }
    IEnumerator Complete()
    {
        player.GetComponent<JobHandler>().VarTask=true;
                    player.GetComponent<NetworkCharacterController>().enabled=true;
            player.GetComponent<CharacterController>().enabled=true;
            player.GetComponent<CharacterMovementHandler>().enabled=true;;
        yield return new WaitForSeconds(.5f);
        hackPanel.SetActive(false);
    }

}