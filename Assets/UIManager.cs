using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour//TODO: custom editor
{
    //Account
    [Header("Login")]
    [SerializeField] private GameObject LogPanel;
    [SerializeField] private TMP_InputField LogMailInput;
    [SerializeField] private TMP_InputField LogPasswordInput;
    [Header("Register")]
    [SerializeField] private GameObject RegPanel;
    [SerializeField] private TMP_InputField RegMailInput;
    [SerializeField] private TMP_InputField RegPasswordInput;
    [SerializeField] private TMP_InputField RegPasswordInput2;
    [Header("Set Name")]
    [SerializeField] private GameObject NameWindow;
    [SerializeField] private TMP_InputField NameInput;
    [Header("Reset Password")]
    [SerializeField] private GameObject ResPanel;
    [SerializeField] private TMP_InputField ResMailInput;
    [Header("Error")]
    [SerializeField] private TMP_Text messageTxt;
    //Switch Sections
    [Space][Space]
    [SerializeField] private GameObject accountSection;
    [SerializeField] private GameObject connectSection;
    [SerializeField] private GameObject matchmakingSection;
    //Matchmaking
    [Space][Space]
    [SerializeField] private TMP_Text playerNameTxt;

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
        messageTxt.text = _message;
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
                break;
            case WhichSection.Connect:
                accountSection.SetActive(!p);
                connectSection.SetActive(p);
                matchmakingSection.SetActive(!p);
                break;
            case WhichSection.Matchmaking:
                accountSection.SetActive(!p);
                connectSection.SetActive(!p);
                matchmakingSection.SetActive(p);
                break;
        }
    }
    #region SwitchSection Methods

    public void SetSection_Account() { SwitchSection(WhichSection.Account); }
    public void SetSection_Connect() { SwitchSection(WhichSection.Connect); }
    public void SetSection_Matchmaking() { SwitchSection(WhichSection.Matchmaking); }

    #endregion
    public enum WhichSection { Account, Connect, Matchmaking }

    #endregion
    #region Connect



    #endregion
    #region Matchmaking



    #endregion

    public void SetDisplayName(string _name) 
    { 
        playerNameTxt.text = _name;
    }
}