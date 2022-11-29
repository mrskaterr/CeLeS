using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLogin : MonoBehaviour//TODO: Guest Mode, Remember Me, Mail OR Name
{
    private UIManager UIm;

    private void Awake()
    {
        UIm = GetComponent<UIManager>();
    }

    public void RegisterButtonMethod()
    {
        string pass = UIm.GetPassword(UIManager.WhichPanel.Register);
        if (pass.Length < 6)
        {
            UIm.SetMessage("Password too short.");
            return;
        }
        else if (!UIm.CheckDoublePassword())
        {
            UIm.SetMessage("Passwords do not match.");
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = UIm.GetMail(UIManager.WhichPanel.Register),
            Password = UIm.GetPassword(UIManager.WhichPanel.Register),
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    public void LoginButtonMethod()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = UIm.GetMail(UIManager.WhichPanel.Login),
            Password = UIm.GetPassword(UIManager.WhichPanel.Login)
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void ResetPasswordButtonMethod()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = UIm.GetMail(UIManager.WhichPanel.Reset),
            TitleId = "4ACE0"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult _result)
    {
        UIm.SetMessage("Registered and logged in!");
    }

    private void OnError(PlayFabError _error)
    {
        UIm.SetMessage(_error.ErrorMessage);
        //Debug.Log(_error.GenerateErrorReport());
    }

    private void OnLoginSuccess(LoginResult _result)
    {
        UIm.SetMessage("Logged in!");
        Debug.Log("Successful login");
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult _result)
    {
        UIm.SetMessage("Password reset mail sent!");
    }
}