using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progresbar : MonoBehaviour
{
    static Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    static public void SetSlider()
    {
        image.fillAmount=(float)++MissionManager.Score/100;
    }
}
