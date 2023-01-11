using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMove : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] Transform background1;
    [SerializeField] Transform background2;
    Vector3 speedV3;

    void Start()
    {
        speedV3=new Vector3(speed,speed,0);
    }

    void Update()
    {
        background1.position-=speedV3;
        background2.position-=speedV3;
        if(background1.position.x< endPos.position.x && background1.position.y <endPos.position.y)
            background1.position=startPos.position;
        if(background2.position.x< endPos.position.x && background2.position.y <endPos.position.y)
            background2.position=startPos.position;

    }
}
