// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEditor;
// using UnityEditor.SearchService;
// using UnityEngine;
// using UnityEngine.SceneManagement;


// public class Autosc : MonoBehaviour
// {
//     int lastScene=-1;
//     const int zero=0;
//     const int one=1;
//     int currentScene;
//     void Awake()
//     {
//         currentScene=SceneManager.GetActiveScene().buildIndex;
        
//         if(lastScene==-one &&  currentScene==one)
//         {
//             lastScene=zero;
//             DontDestroyOnLoad(this.gameObject);
//             SceneManager.LoadScene(zero);
//         }
//         else
//         {
//             lastScene=one;
//             Destroy(this.gameObject);    
//         }
//     }

// }
