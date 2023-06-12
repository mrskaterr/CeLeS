using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class GunMode : NetworkBehaviour
{
    [SerializeField] GameObject firstMode;
    [SerializeField] GameObject secondMode;
    private bool canChangeMode=true;
    public bool fireMode;
    void Start()
    {
        fireMode=true;
    }
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Mouse1) && Object.HasInputAuthority) 
            RPC_ChangeMode();

    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_ChangeMode()
    {
        if(firstMode.activeInHierarchy && canChangeMode)
        { 
            firstMode.SetActive(false);
            secondMode.SetActive(true);
            fireMode=false;
        }
        else if(secondMode.activeInHierarchy && canChangeMode)
        {
            secondMode.SetActive(false);
            firstMode.SetActive(true);
            fireMode=true;;
        }
    } 
}



