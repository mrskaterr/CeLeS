using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestInput : MonoBehaviour
{
    [SerializeField] private GameObject W, S, A, D;

    private void Update()
    {
        W.SetActive(Input.GetKey(KeyCode.W));
        S.SetActive(Input.GetKey(KeyCode.S));
        A.SetActive(Input.GetKey(KeyCode.A));
        D.SetActive(Input.GetKey(KeyCode.D));
    }
}