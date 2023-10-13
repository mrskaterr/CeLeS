using Fusion.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeScene : MonoBehaviour
{
    void Awake()
    {
            EditorSceneManager.MoveSceneBefore(SceneManager.GetSceneByBuildIndex(0),SceneManager.GetSceneByBuildIndex(1));
    }
}