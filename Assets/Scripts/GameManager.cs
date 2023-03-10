using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public MissionManager missionManager;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else { instance = this; }

        missionManager = GetComponent<MissionManager>();
    }

    private void Start()
    {
        Invoke(nameof(EndLoading), 1);
    }

    private void EndLoading()
    {
        LoadingCanvas.SetActive(false);
    }
}