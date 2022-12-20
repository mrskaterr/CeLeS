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
    private bool isThereMatchingLobby = false;

    public static List<SessionInfo> sessions = new List<SessionInfo>();

    private GameMap gameMap;
    private GameTime gameTime;

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
        Invoke(nameof(Connect), 1);
    }

    private void Connect()//TODO: region select
    {
        runnerHandler.InstantiateNetworkRunner(playfabLogin.playerName);
        var joinLobby = JoinLobby(runnerHandler.networkRunner, $"PH");
    }

    public void JoinOrCreateSession()
    {
        switch (isThereMatchingLobby)
        {
            case false:
                var create = StartHost(runnerHandler.networkRunner);
                break;
            case true:
                var join = JoinSession(runnerHandler.networkRunner);
                break;
        }
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

    private async Task StartHost(NetworkRunner _runner/*, string _lobbyName = "MyCustomLobby"*/)
    {
        var customProps = new Dictionary<string, SessionProperty>();

        customProps["map"] = (int)gameMap;
        customProps["time"] = (int)gameTime;

        var result = await _runner.StartGame(new StartGameArgs()
        {
            SessionName = UIm.GetInputText_SessionName(),
            GameMode = GameMode.Shared,//FFS
            SessionProperties = customProps,
            //CustomLobbyName = _lobbyName,
            SceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>(),
        });

        if (result.Ok)
        {
            UIm.SetSection_RoomDetails();
            Debug.Log("Room Created");
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
        }
    }

    private async Task JoinSession(NetworkRunner _runner)
    {
        var result = await _runner.StartGame(new StartGameArgs()
        {
            SessionName = UIm.GetInputText_SessionName(),
            GameMode = GameMode.Shared,//FFS
            SceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
        if (result.Ok)
        {
            UIm.SetSection_RoomDetails();
            Debug.Log("Joined Room");
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
        }
    }

    private bool Search4Session(string _name)
    {
        for (int i = 0; i < sessions.Count; i++)
        {
            if (sessions[i].Name == _name) { return true; }
        }
        return false;
    }

    public void ChangeNetworkScene(int _index)
    {
        runnerHandler.networkRunner.SetActiveScene(_index);
    }

    public NetworkObject SpawnPlayerAvatar()
    {
        NetworkRunner runner = runnerHandler.networkRunner;
        if(runner.LocalPlayer == RPCManager.Local.owner)
        {
            var avatar = runner.Spawn(RPCManager.Local.playerAvatar, Vector3.up * 50 + Vector3.one * Random.Range(5f, 10f), Quaternion.identity, RPCManager.Local.owner);
            //DontDestroyOnLoad(avatar);
            runner.SetPlayerObject(runner.LocalPlayer, avatar);
            return avatar;
        }
        return null;
    }

    public void SetCJButton()
    {
        isThereMatchingLobby = Search4Session(UIm.GetInputText_SessionName());
        UIm.SetCreateButtonTxt(isThereMatchingLobby);
    }

    #region SetLobbyPlayerData

    public void SetRole(int _index)
    {
        RPCManager.Local.roleIndex = _index;
    }

    public void IsReady()
    {
        RPCManager.Local.isReady = !RPCManager.Local.isReady;
    }

    public bool ArePlayersReady()
    {
        int playersAmount = PlayerHolder.playerCount();
        for (int i = 0; i < playersAmount; i++)
        {
            var playerData = PlayerHolder.playerParentObjects[i].GetComponent<RPCManager>();
            if (!playerData.isReady) { return false; }
        }
        return true;
    }

    #endregion

    public enum GameMap : int
    {
        Restaurant,
        Farm,
        Factory
    }

    public enum GameTime : int
    {
        Five,
        Ten,
        Fifteen,
        TwentyFive
    }
}