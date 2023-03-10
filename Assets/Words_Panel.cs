using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Words_Panel : MonoBehaviour
{
    public GameObject player;
    [SerializeField] char [] word;
    [SerializeField] GameObject can;
    [SerializeField] Job job;
    [SerializeField] TMP_Text [] line0;
    [SerializeField] TMP_Text [] line1;
    [SerializeField] TMP_Text [] line2;
    [SerializeField] TMP_Text [] line3;
    [SerializeField] TMP_Text [] line4;

    private TMP_Text [] line;

    private TMP_Text[][] line22;


    int j=0;

    string buff="";
    bool stop;
    bool click=false;

    void Start()
    {
        line22=new TMP_Text[5][];

        line22[0]=line0;
        line22[1]=line1;
        line22[2]=line2;
        line22[3]=line3;
        line22[4]=line4;

        for(int i=0;i<5;i++)
            line22[i][Rand()].SetText(word[i].ToString());


        //buff=line[0].ToString();
   
        //line[4].SetText(buff);
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
            click=true;
        else if (click){
            j++;
            if(j>=5)
                j=0;
            click=false;
        }
        else if (!stop)
            StartCoroutine(Turn());
        for(int i=0;i<5;i++)
        {
            if(line22[i][2].text != word[i].ToString())
            {
                break;
            }
            if(i==4)Debug.Log("donee");

        }

        // if (c <= 3 && !stop)
        // {
        //     left.Rotate(Vector3.back * 1.5f);

        //     lRot = (int)left.localEulerAngles.z;

        //     if (CheckLeft())
        //     { 
        //         left.GetComponent<Image>().color = green; 
        //     }
        //     else
        //     { 
        //         left.GetComponent<Image>().color = blue; 
        //     }
            
        //     if(!CheckLeft() && Input.GetKey(KeyCode.Space))
        //     {
        //         if(c>0)
        //             c--;
        //         StartCoroutine(Depass());
        //     }
        //     else if (CheckLeft() && Input.GetKey(KeyCode.Space))
        //     {
        //         c++;
        //         StartCoroutine(Pass());
        //         if (c == 3)
        //         {
        //             job.active = false;
        //             stop = true;
        //             return;
        //         }
        //         Rand();
        //     } 
        // }
    }

    int Rand()
    {
        return Random.Range(0, 5);
    }

    // bool CheckLeft()
    // {
    //     int L = lRot;
    //     int target = lRand;
    //     int diff = Mathf.Abs(L - target);
    //     Debug.Log(diff);
    //     return diff <= diffrend;
    // }

    IEnumerator Turn()
    {
        stop = true;
        yield return new WaitForSeconds(0.5f);
        buff=line22[j][0].text;
        for(int i=0;i<4;i++)
        {
            line22[j][i].SetText(line22[j][i+1].text);
        }
        line22[j][4].SetText(buff);
        stop = false;
    }
    // IEnumerator Depass()
    // {
    //     stop = true;
    //     images[c].color = blue;
    //     yield return new WaitForSeconds(.5f);
    //     stop = false;
    //     left.localEulerAngles=new Vector3(0,0,0);
    // }
}