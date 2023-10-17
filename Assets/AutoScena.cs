using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using PlayFab.MultiplayerModels;
using UnityEditor.SceneManagement;
 
class AutoScena:MonoBehaviour
{
    //if (options.HasFlag(EnterPlayModeOptions.))s_MySimpleValue = 0;

    [InitializeOnEnterPlayMode]
    void OnEnterPlaymodeInEditor(EnterPlayModeOptions options)
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Lobby.unity",
                                          OpenSceneMode.Single);
    }
}