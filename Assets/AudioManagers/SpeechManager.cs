using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpeechManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioClip infectedAudio;
    private AudioClip companionAudio;
    private AudioSource infectedSource;
    private AudioSource companionSource;
    public AudioMixerGroup speechGroup;
    void Start()
    {
        infectedAudio = Resources.Load<AudioClip>("Audio/Speech/infected_detected");
        companionAudio = Resources.Load<AudioClip>("Audio/Speech/open_fire");
        
        infectedSource = gameObject.AddComponent<AudioSource>();
        companionSource = gameObject.AddComponent<AudioSource>();
        
        infectedSource.outputAudioMixerGroup = speechGroup;
        companionSource.outputAudioMixerGroup = speechGroup;
        

        infectedSource.clip = infectedAudio;
        companionSource.clip = companionAudio;

    }

    public void PlayInfectedDetected() {
        infectedSource.PlayOneShot(infectedSource.clip);
    }

    public void PlayCompanionFire() {
        companionSource.PlayOneShot(companionSource.clip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
