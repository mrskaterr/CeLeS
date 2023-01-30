using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetColors : MonoBehaviour
{
    [SerializeField] GameObject PanelLeds;
    void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("reset");
        PanelLeds.GetComponent<TaskRemeberColor>().ResetColor();
    }
}
