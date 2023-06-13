using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class GunMode : NetworkBehaviour
{
    [SerializeField] GameObject blueMode;
    [SerializeField] GameObject pinkMode;
    private bool canChangeMode=true;
    private bool fireMode;
    //public AnimationCurve X;
    void Start()
    {
        fireMode=true;
    }
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Mouse1) && Object.HasInputAuthority) 
            {RPC_GunMode();}

    }
    public bool FireMode()
    {
        return fireMode;
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_GunMode()
    {
        if(blueMode.activeInHierarchy && canChangeMode)
        {
            blueMode.SetActive(false);
            pinkMode.SetActive(true);
            fireMode=false;
        }
        else if(pinkMode.activeInHierarchy && canChangeMode)
        {
            pinkMode.SetActive(false);
            blueMode.SetActive(true);
            fireMode=true;
        }
    } 
}



