using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LobbyManagerV2 : MonoBehaviour
{
    private NetworkRunnerHandler runnerHandler;
    private PlayfabLogin playfabLogin;
    private UIManager UIm;

    private void Awake()
    {
        runnerHandler = GetComponent<NetworkRunnerHandler>();
        playfabLogin = GetComponent<PlayfabLogin>();
        UIm = GetComponent<UIManager>();
    }

    private void Start()
    {
        playfabLogin.OnCorrectNameProvided += StartConnecting;//TODO: Add anim
    }

    public void StartConnecting()
    {
        Invoke(nameof(Connect), 1);//Q
    }

    private void Connect()
    {
        runnerHandler.InstantiateNetworkRunner(playfabLogin.playerName);
        var joinLobby = JoinLobby(runnerHandler.networkRunner, $"PH");
    }

    private async Task JoinLobby(NetworkRunner _runner, string _lobbyName)
    {
        var result = await _runner.JoinSessionLobby(SessionLobby.Custom, _lobbyName);

        if (result.Ok)
        {
            UIm.SetSection_Matchmaking();
        }
        else
        {
            //Debug.LogError($"Failed to Start: {result.ShutdownReason}");
            UIm.SetMessage($"Failed to Start: {result.ShutdownReason}");
        }
    }
}