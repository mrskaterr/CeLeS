using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RPCManager : NetworkBehaviour
{
    public static RPCManager Local;
    [Networked, Capacity(14)]
    public string nick { get; set; }
    [SerializeField] private Role role = Role.None;
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            nick = Manager.Instance.playfabLogin.playerName;
        }
        PlayerHolder.AddPlayer2List(gameObject);
        RPC_OnPlayerInRoom();
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        PlayerHolder.RemovePlayerFromList(gameObject);
    }

    private void Start()
    {
        gameObject.name = $"Parent ( {nick} )";
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_OnPlayerInRoom()
    {
        Manager.Instance.UIManager.RefreshList();
    }
    public enum Role
    {
        None,
        HunterA,
        HunterB,
        HunterC,
        BlobA,
        BlobB,
        BlobC
    }
}