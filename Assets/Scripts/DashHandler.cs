using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class DashHandler : MonoBehaviour
{
    [SerializeField] TMP_Text dashAmount;
    [SerializeField] Image dashBarFill;
    [SerializeField] float dashSpeed=10f;
    [SerializeField] int dashMaxAmount=2;
    [SerializeField] float maxDashTime=1f;
    [SerializeField] float dashResetTime=100f;
    private NetworkCharacterController networkController;
    private float normalSpeed;
    private int dashCurrentAmount=0;
    private float currentDashTime;
    private float currentDashResetTime;
    [HideInInspector]
    public bool startDashing;
    void Awake()
    {
        networkController=GetComponent<NetworkCharacterController>();
        normalSpeed=networkController.maxSpeed;
        currentDashTime = maxDashTime;
        dashAmount.text=(dashMaxAmount-dashCurrentAmount).ToString();
        currentDashResetTime=dashResetTime;
    }
    public void Dash()
    {
        if (startDashing &&  dashCurrentAmount<dashMaxAmount)
        {
            dashCurrentAmount++;
            currentDashTime = 0.0f;
            currentDashResetTime= 0.0f;
            dashAmount.text=(dashMaxAmount-dashCurrentAmount).ToString();
            startDashing=false;
        }
        if (currentDashTime < maxDashTime)
        {
            networkController.maxSpeed=dashSpeed;
            currentDashResetTime=0;
            dashBarFill.fillAmount = currentDashResetTime/dashResetTime;
            currentDashTime += Time.fixedDeltaTime;
        }
        else
        {
            networkController.maxSpeed=normalSpeed;
            currentDashResetTime += Time.fixedDeltaTime;
            
            dashBarFill.fillAmount = currentDashResetTime/dashResetTime;
            if(currentDashResetTime>=dashResetTime)
            {
                dashCurrentAmount=0;
                dashAmount.text=(dashMaxAmount-dashCurrentAmount).ToString();
            }
                
        }
    }
}
