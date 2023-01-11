using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash : MonoBehaviour
{
    public Animator anim;
    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        anim.Play("scale");
        ps.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
