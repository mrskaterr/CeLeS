using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class GunMode : NetworkBehaviour
{
    [SerializeField] GameObject FirstMode;
    [SerializeField] GameObject SecondMode;
    private bool CanChangeMode=true;
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Mouse1) && Object.HasInputAuthority) 
            RPC_teleport();

    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_teleport()
    {
        if(FirstMode.activeInHierarchy && CanChangeMode)
        { 
            FirstMode.SetActive(false);
            SecondMode.SetActive(true);
        }
        else if(SecondMode.activeInHierarchy && CanChangeMode)
        {
            SecondMode.SetActive(false);
            FirstMode.SetActive(true);
        }
    } 
}



