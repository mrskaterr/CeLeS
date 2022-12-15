using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;

public class PlayerNetworkEventsHandler : MonoBehaviour, INetworkRunnerCallbacks
{
    public void OnConnectedToServer(NetworkRunner runner)
    {
        return;
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        return;
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        return;
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        return;
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        return;
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        return;
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        return;
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        return;
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if(runner.LocalPlayer == player)
        {
            GetComponent<SpawnerV2>().SpawnPlayerParent(runner, player, $"Player {player.PlayerId}");
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        return;
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        return;
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        return;
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        return;
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        LobbyManagerV2.sessions = sessionList;
        return;
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        return;
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        return;
    }
}