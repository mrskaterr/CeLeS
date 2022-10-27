using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private NetworkRunnerHandler runnerHandler;
    [Header("GUI")]
    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private GameObject connect_btn;
    [SerializeField] private GameObject session_btnsParent;
    [Space]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject joinPanel;
    [SerializeField] private GameObject createPanel;

    private enum WhichPanel { Info, Join, Create }

    private const string nickname_PlayerPrefName = "nickname";

    private void Start()
    {
        ToggleButtons(true);

        if (PlayerPrefs.HasKey(nickname_PlayerPrefName))
        {
            playerName.placeholder.GetComponent<TMP_Text>().text = PlayerPrefs.GetString(nickname_PlayerPrefName);
        }
    }

    public void Connect()
    {
        string nickname = playerName.text;
        if (!string.IsNullOrEmpty(nickname))//TOIMPROVE: more conditions
        {
            SetNick();
        }
        if (PlayerPrefs.HasKey(nickname_PlayerPrefName))
        {
            runnerHandler.InstantiateNetworkRunner(PlayerPrefs.GetString(nickname_PlayerPrefName));
            ToggleButtons(false);
        }
    }

    public void CreateRoom()
    {
        LobbyInfo lobbyInfo = runnerHandler.networkRunner.
    }

    #region GUI

    public void SetNick()
    {
        PlayerPrefs.SetString(nickname_PlayerPrefName, playerName.text);
    }

    /// <summary>
    /// Switching between sets of buttons.
    /// </summary>
    /// <param name="_p">True - Connect Button; False - Session Buttons</param>
    public void ToggleButtons(bool _p)
    {
        connect_btn.SetActive(_p);
        session_btnsParent.SetActive(!_p);

        playerName.readOnly = !_p;
    }

    public void OpenPanel_Info() { SwitchPanel(WhichPanel.Info); }
    public void OpenPanel_Join() { SwitchPanel(WhichPanel.Join); }
    public void OpenPanel_Create() { SwitchPanel(WhichPanel.Create); }

    private void SwitchPanel(WhichPanel _panel)
    {
        GameObject currentPanel = _panel switch
        {
            WhichPanel.Info => infoPanel,
            WhichPanel.Join => joinPanel,
            WhichPanel.Create => createPanel,
            _ => throw new System.Exception($"{typeof(WhichPanel)}: out of range."),
        };
        infoPanel.SetActive(false);
        joinPanel.SetActive(false);
        createPanel.SetActive(false);
        currentPanel.SetActive(true);
    }

    #endregion
}