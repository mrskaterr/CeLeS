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
}