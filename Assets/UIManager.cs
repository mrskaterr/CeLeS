using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityInspector;

public class UIManager : MonoBehaviour
{
    [Foldout("Account", true)]
    [SerializeField] private GameObject LogPanel;
    [SerializeField] private TMP_InputField LogMailInput;
    [SerializeField] private TMP_InputField LogPasswordInput;
    [Foldout("Register", true)]
    [SerializeField] private GameObject RegPanel;
    [SerializeField] private TMP_InputField RegMailInput;
    [SerializeField] private TMP_InputField RegPasswordInput;
    [SerializeField] private TMP_InputField RegPasswordInput2;
    [Foldout("Set Name", true)]
    [SerializeField] private GameObject NameWindow;
    [SerializeField] private TMP_InputField NameInput;
    [Foldout("Reset Password", true)]
    [SerializeField] private GameObject ResPanel;
    [SerializeField] private TMP_InputField ResMailInput;
    [Foldout("Error")]
    [SerializeField] private TMP_Text messageTxt;
    [Foldout("Sections", true)]
    [SerializeField] private GameObject accountSection;
    [SerializeField] private GameObject connectSection;
    [SerializeField] private GameObject matchmakingSection;
    [Space]
    [SerializeField] private GameObject sessionDetailsSection;
    [SerializeField] private GameObject roomDetailsSection;
    [Foldout("Matchmaking")]
    [SerializeField] private TMP_Text playerNameTxt;
    [Foldout("Session Details", true)]
    [SerializeField] private TMP_Text playerNameTxtB;
    [SerializeField] private TMP_InputField sessionNameInput;
    [SerializeField] private TMP_Text CJBtnText;//TODO: blocking button
    [SerializeField] private GameObject createPanel;
    [SerializeField] private TMP_Dropdown mapSelection;
    [SerializeField] private TMP_Dropdown timeSelection;
    [Foldout("Room Details", true)]
    [SerializeField] private TMP_Text roomNameTxt;
    [SerializeField] private TMP_Text mapNameTxt;
    [SerializeField] private TMP_Text timeTxt;
    [SerializeField] private Transform playerListParent;
    [SerializeField] private GameObject playerListItemPrefab;
    [SerializeField] private Sprite[] roleIcons;
    //[SerializeField] private List<PlayerDataHandler> playerDataHandlers = new List<PlayerDataHandler>(); custom data class

    private const string joinTxt = "Join";
    private const string createTxt = "Create";

    #region Account

    private void SwitchPanel(WhichPanel _panel)
    {
        bool p = true;
        switch (_panel)
        {
            case WhichPanel.Login:
                LogPanel.SetActive(p);
                RegPanel.SetActive(!p);
                NameWindow.SetActive(!p);
                ResPanel.SetActive(!p);
                break;
            case WhichPanel.Register:
                LogPanel.SetActive(!p);
                RegPanel.SetActive(p);
                NameWindow.SetActive(!p);
                ResPanel.SetActive(!p);
                break;
            case WhichPanel.Name:
                LogPanel.SetActive(!p);
                RegPanel.SetActive(!p);
                NameWindow.SetActive(p);
                ResPanel.SetActive(!p);
                break;
            case WhichPanel.Reset:
                LogPanel.SetActive(!p);
                RegPanel.SetActive(!p);
                NameWindow.SetActive(!p);
                ResPanel.SetActive(p);
                break;
        }
    }
    public string GetMail(WhichPanel _panel) => _panel switch
    {
        WhichPanel.Login => LogMailInput.text,
        WhichPanel.Register => RegMailInput.text,
        WhichPanel.Reset => ResMailInput.text,
        _ => ""
    };
    public string GetPassword(WhichPanel _panel) => _panel switch
    {
        WhichPanel.Login => LogPasswordInput.text,
        WhichPanel.Register => RegPasswordInput.text,
        _ => ""
    };
    public string GetNewName()
    {
        return NameInput.text;
    }
    public bool CheckDoublePassword()
    {
        return RegPasswordInput.text == RegPasswordInput2.text;
    }
    public void SetMessage(string _message)
    {
        messageTxt.SetContent(_message);
    }
    #region SwitchPanel Methods

    public void Open_LoginPanel() { SwitchPanel(WhichPanel.Login); }
    public void Open_RegisterPanel() { SwitchPanel(WhichPanel.Register); }
    public void Open_ResetPanel() { SwitchPanel(WhichPanel.Reset); }
    public void Open_NameWindow() { SwitchPanel(WhichPanel.Name); }

    #endregion
    public enum WhichPanel { Login, Register, Reset, Name }

    #endregion
    #region Sections
    private void SwitchSection(WhichSection _section)
    {
        bool p = true;
        switch (_section)
        {
            case WhichSection.Account:
                accountSection.SetActive(p);
                connectSection.SetActive(!p);
                matchmakingSection.SetActive(!p);
                sessionDetailsSection.SetActive(!p);
                roomDetailsSection.SetActive(!p);
                break;
            case WhichSection.Connect:
                accountSection.SetActive(!p);
                connectSection.SetActive(p);
                matchmakingSection.SetActive(!p);
                sessionDetailsSection.SetActive(!p);
                roomDetailsSection.SetActive(!p);
                break;
            case WhichSection.Matchmaking:
                accountSection.SetActive(!p);
                connectSection.SetActive(!p);
                matchmakingSection.SetActive(p);
                sessionDetailsSection.SetActive(!p);
                roomDetailsSection.SetActive(!p);
                break;
            case WhichSection.SessionDetails:
                accountSection.SetActive(!p);
                connectSection.SetActive(!p);
                matchmakingSection.SetActive(!p);
                sessionDetailsSection.SetActive(p);
                roomDetailsSection.SetActive(!p);
                break;
            case WhichSection.RoomDetails:
                accountSection.SetActive(!p);
                connectSection.SetActive(!p);
                matchmakingSection.SetActive(!p);
                sessionDetailsSection.SetActive(!p);
                roomDetailsSection.SetActive(p);
                break;
        }
    }
    #region SwitchSection Methods

    public void SetSection_Account() { SwitchSection(WhichSection.Account); }
    public void SetSection_Connect() { SwitchSection(WhichSection.Connect); }
    public void SetSection_Matchmaking() { SwitchSection(WhichSection.Matchmaking); }
    public void SetSection_SessionDetails() { SwitchSection(WhichSection.SessionDetails); }
    public void SetSection_RoomDetails() { SwitchSection(WhichSection.RoomDetails); }

    #endregion
    public enum WhichSection { Account, Connect, Matchmaking, SessionDetails, RoomDetails }

    #endregion
    #region Connect



    #endregion
    #region Matchmaking



    #endregion
    #region Session Details

    public void SetCreateButtonTxt(bool _p)
    {
        CJBtnText.SetContent(_p ? joinTxt : createTxt);
    }

    public string GetInputText_SessionName()
    {
        return sessionNameInput.text;
    }

    #endregion
    #region Room Details

    public void SetRoomDisplayInfo(string _name, string _map, string _time)
    {
        roomNameTxt.SetContent(_name);
        mapNameTxt.SetContent(_map);
        timeTxt.SetContent(_time);
    }

    public void RefreshList()
    {
        foreach (Transform child in playerListParent)
        {
            Destroy(child.gameObject);
        }
        int playersAmount = PlayerHolder.playerCount();
        for (int i = 0; i < playersAmount; i++)
        {
            var item = Instantiate(playerListItemPrefab, playerListParent);
            var playerData = PlayerHolder.playerParentObjects[i].GetComponent<RPCManager>();
            item.GetComponent<PlayerListItem>().SetContent(playerData.nick, roleIcons[0]);
        }
    }

    #endregion
    public void SetDisplayName(string _name) 
    {
        playerNameTxt.SetContent(_name);
        playerNameTxtB.SetContent(_name);
    }
}