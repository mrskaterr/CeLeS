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
    [Networked(OnChanged = nameof(OnRoleChange))]
    [HideInInspector] public int roleIndex { get; set; } = 0;
    [Networked(OnChanged = nameof(OnIsReadyChange))]
    public bool isReady { get; set; } = false;
    [SerializeField] private GameObject hunterAvatar;
    [SerializeField] private GameObject blobAvatar;
    //public static GameObject Avatar;
    public PlayerRef owner;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            nick = Manager.Instance.playfabLogin.playerName;
        }
        PlayerHolder.AddPlayer2List(gameObject);
        RPC_OnPlayerInRoom();
        DontDestroyOnLoad(gameObject);
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

    public static void OnIsReadyChange(Changed<RPCManager> _changed)
    {
        _changed.Behaviour.OnIsReadyChange();
    }

    public void OnIsReadyChange()
    {
        Manager.Instance.UIManager.RefreshList();
        if (Manager.Instance.lobbyManager.ArePlayersReady())
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        var avatar = Manager.Instance.lobbyManager.SpawnPlayerAvatar();
        avatar.transform.name = nick;
        yield return new WaitForSecondsRealtime(2);
        Manager.Instance.lobbyManager.ChangeNetworkScene(2);
    }

    public static void OnRoleChange(Changed<RPCManager> _changed)
    {
        _changed.Behaviour.OnRoleChange();
    }
    public void OnRoleChange() 
    {
        role = (Role)roleIndex;
        Manager.Instance.UIManager.RefreshList();
    }
    public GameObject PlayerAvatar()
    {
        if(roleIndex == 4 || roleIndex == 5 || roleIndex == 6)//TOIMPROVE:switch and list of available
        {
            return blobAvatar;
        }
        return hunterAvatar;
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