using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Dave");
    }
    public void Pasue()
    {
        Time.timeScale=0f;
    }
    public void Resume()
    {
        Time.timeScale=1f;
    }
    public void Menu()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
