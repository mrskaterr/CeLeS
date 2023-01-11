using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontdestroy : MonoBehaviour
{
    // Start is called before the first frame update
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
