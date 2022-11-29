using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
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
    private void SwitchPanel(WhichPanel _panel)
    {
        bool p = true;
        switch(_panel)
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
}