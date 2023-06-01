using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class GunMode : NetworkBehaviour
{
    [SerializeField] GameObject firstMode;
    [SerializeField] GameObject secondMode;
    private bool canChangeMode = true;
    public bool fireMode;
    //public AnimationCurve X;
    void Start()
    {
        fireMode = true;
    }

    public void SwapMode()
    {
        RPC_teleport();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_teleport()
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



