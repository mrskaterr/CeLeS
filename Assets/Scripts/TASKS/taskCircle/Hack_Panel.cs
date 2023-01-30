using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hack_Panel : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Job job;
    [SerializeField] Transform left, right;
    [SerializeField] Image[] images;
    int lRot, rRot;
    int lRand, rRand;
    [SerializeField] Color green;
    [SerializeField] Color blue;
    int c = 0;
    bool stop;

    private void Start()
    {
        Rand();
    }

    private void FixedUpdate()
    {
        if (c <= 3 && !stop)
        {
            if (Input.GetKey(KeyCode.A)) { left.Rotate(Vector3.back * 1.5f); }
            else if (Input.GetKey(KeyCode.D)) { left.Rotate(Vector3.forward * 1.5f); }

            if (Input.GetKey(KeyCode.LeftArrow)) { right.Rotate(Vector3.back * 1.5f); }
            else if (Input.GetKey(KeyCode.RightArrow)) { right.Rotate(Vector3.forward * 1.5f); }

            lRot = (int)left.localEulerAngles.z;
            rRot = (int)right.localEulerAngles.z;

            if (CheckLeft()) { left.GetComponent<Image>().color = green; }
            else { left.GetComponent<Image>().color = blue; }

            if (CheckRight()) { right.GetComponent<Image>().color = green; }
            else { right.GetComponent<Image>().color = blue; }

            if (CheckLeft() && CheckRight())
            {
                c++;
                StartCoroutine(Pass());
                if (c == 3)
                {
                    job.active = false;
                    stop = true;
                    return;
                }
                Rand();
            } 
        }
    }

    void Rand()
    {
        lRand = Random.Range(0, 360);
        rRand = Random.Range(0, 360);
    }

    bool CheckLeft()
    {
        int L = lRot;
        int target = lRand;
        int diff = Mathf.Abs(L - target);
        return diff <= 5;
    }

    bool CheckRight()
    {
        int R = rRot;
        int target = rRand;
        int diff = Mathf.Abs(R - target);
        return diff <= 5;
    }

    IEnumerator Pass()
    {
        stop = true;
        left.GetComponent<Image>().color = green;
        right.GetComponent<Image>().color = green;
        if (c <= 3) { images[c - 1].color = green; }
        yield return new WaitForSeconds(.5f);
        stop = false;
    }
}