using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private GameObject connect_btn;
    [SerializeField] private GameObject session_btnsParent;

    public void SetNick()
    {
        PlayerPrefs.SetString("nickname", playerName.text);
    }

    /// <summary>
    /// Switching between sets of buttons.
    /// </summary>
    /// <param name="_p">True - Connect Button; False - Session Buttons</param>
    public void ToggleButtons(bool _p)
    {
        connect_btn.SetActive(_p);
        session_btnsParent.SetActive(!_p);
    }
}