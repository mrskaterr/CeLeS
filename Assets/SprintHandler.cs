using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintHandler : MonoBehaviour
{
         public float maxStamina = 5f;
    [HideInInspector]
    public bool IsSprinting;
    private float cunrrentStamina=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //         if(IsSprinting && cunrrentStamina>0f)
        // {
        //     cunrrentStamina-=Time.deltaTime;
        //     if(cunrrentStamina<=0f)
        //         inputHandler.canSprinting=false;
        // }
        // else if(!IsSprinting && cunrrentStamina<=maxStamina)
        // {
        //     cunrrentStamina+=Time.deltaTime;
        //     if(cunrrentStamina>=maxStamina)
        //         inputHandler.canSprinting=true;
        // }
    }
}
