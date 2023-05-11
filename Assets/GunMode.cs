using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class GunMode : NetworkBehaviour
{
    [SerializeField] GameObject FirstMode;
    [SerializeField] GameObject SecondMode;

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Mouse1) && Object.HasInputAuthority) 
            RPC_teleport();

    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_teleport()
    {
        if(FirstMode.activeInHierarchy)
        { 
            FirstMode.SetActive(false);
            SecondMode.SetActive(true);
        }
        else if(SecondMode.activeInHierarchy)
        {
            SecondMode.SetActive(false);
            FirstMode.SetActive(true);
        }
    } 
}



