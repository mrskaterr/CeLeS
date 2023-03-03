using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityInspector;

public class LobbyManagerV2 : MonoBehaviour
{
    private NetworkRunnerHandler runnerHandler;
    private PlayfabLogin playfabLogin;
    private UIManager UIm;
    private bool isThereMatchingLobby = false;

    public static List<SessionInfo> sessions = new List<SessionInfo>();

    private GameMap gameMap = GameMap.Restaurant;
    private GameTime gameTime = GameTime.Fifteen;
    private int huntersAmount = 2;
    private int hidersAmount = 3;

    private int prefRole = 0;

    private const string cp_huntersSlots = "isTeamFull_Hunters";
    private const string cp_hidersSlots = "isTeamFull_Hiders";

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

    public void SetPreferredRole(int _index)
    {
        prefRole = _index;
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
        customProps["hunters"] = huntersAmount;
        customProps["hiders"] = hidersAmount;

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
            Invoke("SetGamePropertiesNames", .1f);
            Debug.Log("Room Created");
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
        }
    }//TODO: Quick Game ( predefined properties )

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
            var avatar = runner.Spawn(RPCManager.Local.PlayerAvatar(), Vector3.up + Vector3.one * Random.Range(0f, 2f), Quaternion.identity, RPCManager.Local.owner);
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
    #region Game Properties

    public void SetGameProperties()
    {
        gameMap = GetGameMap();
        gameTime = (GameTime)UIm.GetSelectedDuration();
        huntersAmount = GetGameHuntersAmount();
        hidersAmount = GetGameHidersAmount();
    }

    private GameMap GetGameMap() => UIm.GetSelectedMap() switch
    {
        0 => GameMap.Restaurant,
        1 => GameMap.Farm,
        2 => GameMap.Factory,
        _ => GameMap.Restaurant,
    };

    private void SetGamePropertiesNames()
    {

        SessionInfo session = runnerHandler.networkRunner.SessionInfo;
        int tmp = session.Properties["map"];
        int tmp2 = session.Properties["time"];
        string sessionName = session.Name;
        string mapName = tmp switch
        {
            0 => "Restaurant",
            1 => "Farm",
            2 => "Factory",
            _ => "Map Name",
        };
        string durationName = tmp2 switch
        {
            0 => "5 min",
            1 => "10 min",
            2 => "15 min",
            3 => "25 min",
            _ => "Duration",
        };

        UIm.SetRoomDisplayInfo(sessionName, mapName, durationName);
    }

    private GameTime GetGameTime() => UIm.GetSelectedDuration() switch
    {
        0 => GameTime.Five,
        1 => GameTime.Ten,
        2 => GameTime.Fifteen,
        3 => GameTime.TwentyFive,
        _ => GameTime.Fifteen,
    };

    private int GetGameHuntersAmount() => UIm.GetSelectedHuntersAmount() switch
    {
        0 => 1,
        1 => 2,
        2 => 3,
        _ => 2,
    };

    private int GetGameHidersAmount() => UIm.GetSelectedHidersAmount() switch
    {
        0 => 1,
        1 => 2,
        2 => 3,
        _ => 3,
    };

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
    #endregion
}