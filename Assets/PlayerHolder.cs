using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    public static List<GameObject> playerParentObjects = new List<GameObject>();
    //public static List<GameObject> playerObjects = new List<GameObject>();

    public static int playerCount() { return playerParentObjects.Count; }
    public static void AddPlayer2List(GameObject _obj) 
    {
        if(!playerParentObjects.Contains(_obj)) { playerParentObjects.Add(_obj); }
    }
    public static void RemovePlayerFromList(GameObject _obj) { playerParentObjects.Remove(_obj); }

    //public static void AddPlayerObject2List(GameObject _obj)
    //{
    //    if (!playerObjects.Contains(_obj)) { playerObjects.Add(_obj); }
    //}
    //public static void RemovePlayerObjectFromList(GameObject _obj) { playerObjects.Remove(_obj); }
}