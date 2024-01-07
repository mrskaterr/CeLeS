using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] GameObject interactDone;
    [SerializeField] GameObject interactLoading;
    [SerializeField] AudioSource stepAudio;
    // Start is called before the first frame update
    public void PlayStepAudio()
    {
        if(!stepAudio.isPlaying)
            stepAudio.Play();
    }
    public void NinjaMode(float time)
    {
        StartCoroutine(WaitAndEnale(time));
    }
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
        StartCoroutine(WaitAndDisable(0.2f));
    }
    private IEnumerator WaitAndDisable(float time)
    {
        interactDone.SetActive(true);
            yield return new WaitForSeconds(time);
        interactDone.SetActive(false);

    }
    private IEnumerator WaitAndEnale(float time)
    {
        stepAudio.enabled=false;
            yield return new WaitForSeconds(time);
        stepAudio.enabled=true;

    }
    public void InteractLoading(bool isInteract)
    {
        interactLoading.SetActive(isInteract);
    }
}
