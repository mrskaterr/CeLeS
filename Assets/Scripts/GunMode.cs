using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class GunMode : NetworkBehaviour
{
    [SerializeField] GameObject fireGun;
    [SerializeField] GameObject unmorphGun;
    private void FireMode(bool _v)
    {
        fireGun.SetActive(_v);
        unmorphGun.SetActive(!_v);
    }
    public bool isFireMode()
    {
        return fireGun.activeInHierarchy;
    }

    public void SwapMode()
    {
        RPC_SwapMode();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SwapMode()
    {
        if(!fireGun.activeInHierarchy  && !unmorphGun.activeInHierarchy)
            return;
            
        if(isFireMode())
            FireMode(true);
        else if(!isFireMode())
            FireMode(false);
    } 
}



