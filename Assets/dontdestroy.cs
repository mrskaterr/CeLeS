using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontdestroy : MonoBehaviour
{
    AudioSource button;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
            Destroy(gameObject);
    }
}
