using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] int max;
    [SerializeField] int current;
    [SerializeField] Image mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void getCurrentFill()
    {
        float fill=(float) current/(float)max;
        mask.fillAmount=fill;
    }
}
