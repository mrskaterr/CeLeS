using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CloseLoading());
    }

    private IEnumerator CloseLoading()
    {
        yield return new WaitForSeconds(1);
        LoadingCanvas.SetActive(false);
    }
}