using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] GameObject interactDone;
    [SerializeField] GameObject interactLoading;
    // Start is called before the first frame update
    public void PlayClip(AudioClip audioClip)
    {
        if(mainAudioSource.clip==audioClip && mainAudioSource.isPlaying)
            return;
        mainAudioSource.clip=audioClip;
        mainAudioSource.Play();
    }
    public void StopClip(AudioClip audioClip)
    {
        if(audioClip==mainAudioSource.clip)
            mainAudioSource.Stop();
    }
    public void InteractDone()
    {
        StartCoroutine(WaitAndDisable());
    }
    private IEnumerator WaitAndDisable()
    {
        interactDone.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        interactDone.SetActive(false);

    }
    public void InteractLoading(bool isInteract)
    {
        interactLoading.SetActive(isInteract);
    }
}
