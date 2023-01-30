using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject PanelLeds;
    [SerializeField] int index;
    void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Play();
        PanelLeds.GetComponent<TaskRemeberColor>().AddSelectedColor(index);
    }
}
