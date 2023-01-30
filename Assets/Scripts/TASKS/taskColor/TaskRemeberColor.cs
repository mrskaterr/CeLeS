using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRemeberColor : MonoBehaviour
{
    int rnd;
    [SerializeField] int HowManyColors;
    int level=0;
    List<int> RandomColors = new List<int>();
    List<int> SelectedColors= new List<int>();
    [SerializeField] GameObject[] Leds;
    void Start()
    {

    }
    public void ResetColor()
    {
        SelectedColors.Clear();
        RandomColors.Clear();
        for(int i=0;i<HowManyColors;i++)
        {
            rnd=Random.Range(0,4);
            RandomColors.Add(rnd);
            
        }
            if(HowManyColors==4)Debug.Log(RandomColors[0]+" "+RandomColors[1]+" "+RandomColors[2]+" "+RandomColors[3]);
            else if(HowManyColors==5)Debug.Log(RandomColors[0]+" "+RandomColors[1]+" "+RandomColors[2]+" "+RandomColors[3]+" "+RandomColors[4]);
            else if(HowManyColors==6)Debug.Log(RandomColors[0]+" "+RandomColors[1]+" "+RandomColors[2]+" "+RandomColors[3]+" "+RandomColors[4]+" "+RandomColors[5]);

        StartCoroutine(ExampleCoroutine());
    }
    public void AddSelectedColor(int selected)
    {
        if(RandomColors.Count==HowManyColors)
        {
            SelectedColors.Add(selected);
            Debug.Log("selected "+selected);
            if(SelectedColors.Count==HowManyColors)
            {
                if(CheckColors())
                {
                    level++;
                    HowManyColors++;
                    Debug.Log("level "+level);
                }
                SelectedColors.Clear();
                RandomColors.Clear();
            }
            if(level==3)
            {
                //done
                Debug.Log("done");
            }
        }
        
    }
    bool CheckColors()
    {
        for(int i=0;i<SelectedColors.Count;i++)
        {
            if(SelectedColors[i]!=RandomColors[i])
            {
                Debug.Log("false");
                return false;
            }
        }
        Debug.Log("true");
        return true;
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(2f);
        for(int i=0;i<RandomColors.Count;i++)
        {
            Leds[RandomColors[i]].SetActive(true);
            yield return new WaitForSeconds(2f);
            Leds[RandomColors[i]].SetActive(false);
            yield return new WaitForSeconds(2f);
        }
    }
    
}
