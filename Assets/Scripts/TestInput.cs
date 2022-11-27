using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class TestInput : NetworkBehaviour
{
    [SerializeField] private GameObject W, S, A, D;
    [SerializeField] private TMP_Text ping;

    private void Update()
    {
        W.SetActive(Input.GetKey(KeyCode.W));
        S.SetActive(Input.GetKey(KeyCode.S));
        A.SetActive(Input.GetKey(KeyCode.A));
        D.SetActive(Input.GetKey(KeyCode.D));
        ping.text = $"Ping: {Runner.GetPlayerRtt(Runner.LocalPlayer).ToString("n0")}";
    }
}