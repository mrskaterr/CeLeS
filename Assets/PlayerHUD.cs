using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    [SerializeField] private GameObject pauseMenu;

    public void DisplayInfo(string _text)
    {
        StopAllCoroutines();
        StartCoroutine(PingInfo(_text));
    }

    private IEnumerator PingInfo(string _txt)
    {
        txt.text = _txt;
        yield return new WaitForSeconds(2f);
        txt.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (pauseMenu.activeSelf)
            {
                Manager.Instance.lobbyManager.LeaveSession();
            }
        }
    }
}