using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }

    [Networked(OnChanged = nameof(OnNicknameChanged))]
    public NetworkString<_16> nickname { get; set; }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;

            RPC_SetNickname($"Nick: {Random.Range(0, 1000)}");//TODO: Playerprefs//Q

            Debug.Log("Spawned local player");
        }
        else
        {
            Debug.Log("Spawned remote player");
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if(player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }

    private static void OnNicknameChanged(Changed<NetworkPlayer> _changed)
    {
        _changed.Behaviour.OnNicknameChanged();
    }

    private void OnNicknameChanged()
    {
        Debug.Log($"Nickname changed to {nickname} for player {name}");
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickname(string _nick, RpcInfo _info = default)
    {
        Debug.Log($"[RPC] SetNickname {_nick}");
        nickname = _nick;
    }
}