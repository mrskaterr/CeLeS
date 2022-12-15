using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    public static List<GameObject> playerParentObjects = new List<GameObject>();

    public static int playerCount() { return playerParentObjects.Count; }
    public static void AddPlayer2List(GameObject _obj) 
    {
        if(!playerParentObjects.Contains(_obj)) { playerParentObjects.Add(_obj); }
    }
    public static void RemovePlayerFromList(GameObject _obj) { playerParentObjects.Remove(_obj); }
}