using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TMP_Text spaceNameTxt;
    [SerializeField] private TMP_Text rotationTxt;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject onHitImage;
    [SerializeField] private GameObject miniGameParent;

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
        rotationTxt.text = transform.eulerAngles.y.ToString("0");
    }

    public void SetSpaceName(string _name)
    {
        spaceNameTxt.text = _name;
    }

    public void ToggleCrosshair(bool _p) { crosshair.SetActive(_p); }
    public void ToggleOnHitImage(bool _p) { onHitImage.SetActive(_p); }
    public void ToggleMiniGame(bool _p) { miniGameParent.SetActive(_p); }
}