using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class ScoreManager : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnScoreUpdate))]
    public int Score { get; set; }

    [SerializeField] private Image progressBar;
    [SerializeField] private int scoreTarget = 100;

    private static void OnScoreUpdate(Changed<ScoreManager> _changed)
    {
        _changed.Behaviour.progressBar.fillAmount = _changed.Behaviour.Score / (float)_changed.Behaviour.scoreTarget;
        _changed.Behaviour.Rpc_MyStaticRpc(_changed.Behaviour.Score);
        //If Score greater than scoreTarget end game ( blobs wins )
    }

    [Rpc]
    public void Rpc_MyStaticRpc(int a) { print(a); }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Score += 5;
        }
    }
}