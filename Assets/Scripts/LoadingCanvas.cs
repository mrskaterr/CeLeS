using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCanvas : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;

    private static GameObject loading;

    private void Awake()
    {
        loading = loadingScreen;

        DontDestroyOnLoad(gameObject);
    }

    public static void SetActive(bool _p)
    {
        loading.SetActive(_p);
    }
}