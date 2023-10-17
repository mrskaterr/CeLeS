using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using TMPro; 
using UnityEngine.UI; 
using UnityEditor.SearchService; 
using UnityEngine.SceneManagement; 
 
public class AutoPlay : MonoBehaviour 
{ 
    [SerializeField] string log="test@test.com"; 
    [SerializeField] string pass="123456"; 
    [SerializeField] TMP_InputField login; 
    [SerializeField] TMP_InputField password; 
    [SerializeField] GameObject Manager; 
    [SerializeField] GameObject CustomGame;  
     
    [SerializeField] GameObject CreateOrJoin;  
    [SerializeField] GameObject PrivateRoomDetails; 
    bool []isDone=new bool[]{false,false,false}; 
    void Start() 
    { 
        SceneManager.GetActiveScene(); 
        login.text=log; 
        password.text=pass; 
        Manager.GetComponent<PlayfabLogin>().LoginButtonMethod(); 
    } 
    void Update() 
    { 
        if(!isDone[0] && CustomGame.activeInHierarchy) 
        { 
            Manager.GetComponent<UIManager>().SetSection_SessionDetails(); 
            isDone[0]=true; 
        } 
        if(!isDone[1] && CreateOrJoin.activeInHierarchy) 
        { 
            Manager.GetComponent<LobbyManagerV2>().JoinOrCreateSession(); 
            Manager.GetComponent<UIManager>().SetLoadingIconActive(true); 
            CreateOrJoin.GetComponent<Button>().interactable=false; 
            isDone[1]=true; 
        } 
        if(!isDone[2] && PrivateRoomDetails.activeInHierarchy) 
        { 
            Manager.GetComponent<LobbyManagerV2>().SetRole(4); 
            Manager.GetComponent<LobbyManagerV2>().IsReady(); 
            isDone[2]=true; 
        } 
 
    } 
} 
