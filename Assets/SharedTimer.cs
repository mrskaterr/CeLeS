using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class SharedTimer : NetworkBehaviour
{
    [Networked]
    public int seconds { get; set; } = 0;

    [SerializeField] private TMP_Text timerTxt;

    private void Update()
    {
        timerTxt.text = seconds.ToString();
    }
}