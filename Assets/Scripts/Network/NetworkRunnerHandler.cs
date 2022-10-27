using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;

    [HideInInspector] public NetworkRunner networkRunner;

    public void InstantiateNetworkRunner(string _playerName)
    {
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = $"Network runner: {_playerName}";
    }

    public void StartGame(string _sessionName, string _password)
    {
        //var clientTask = InitializeNetworkRunner(networkRunner, _sessionName, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
        //Debug.Log($"Server NetworkRunner started.");
        networkRunner.JoinSessionLobby(SessionLobby.Custom, _sessionName);
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner _runner, string _sessionName, GameMode _gameMode, NetAddress _address, SceneRef _scene, Action<NetworkRunner> _initialized)
    {
        var sceneManager = _runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if (sceneManager != null)
        {
            sceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        _runner.ProvideInput = true;

        return _runner.StartGame(new StartGameArgs
        {
            GameMode = _gameMode,
            Address = _address,
            Scene = _scene,
            SessionName = _sessionName,
            Initialized = _initialized,
            SceneManager = sceneManager,
        });
    }
}