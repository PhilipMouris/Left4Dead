using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private List<AudioSource> music = new List<AudioSource>();
    private AudioClip menuAudio;
    private AudioClip exploreAudio;
    private AudioClip fightAudio;
    private AudioSource menuSource;
    private AudioSource exploreSource;
    private AudioSource fightSource;

    private List<AudioSource> musicSounds = new List<AudioSource>();
    public AudioMixerGroup MusicGroup;
    void Start()
    {
        
        menuAudio = Resources.Load<AudioClip>("Audio/Music/slowpaced");
        exploreAudio = Resources.Load<AudioClip>("Audio/Music/explore");
        fightAudio = Resources.Load<AudioClip>("Audio/Music/fight");
        
        menuSource = gameObject.AddComponent<AudioSource>();
        exploreSource = gameObject.AddComponent<AudioSource>();
        fightSource = gameObject.AddComponent<AudioSource>();
        
        menuSource.outputAudioMixerGroup = MusicGroup;
        exploreSource.outputAudioMixerGroup = MusicGroup;
        fightSource.outputAudioMixerGroup = MusicGroup;

        menuSource.clip = menuAudio;
        exploreSource.clip = exploreAudio;
        fightSource.clip = fightAudio;
        
        /*
        Debug.Log(menuTheme);
        slowpaced.outputAudioMixerGroup = MusicGroup;
        slowpaced.clip = menuTheme;
        slowpaced.Play();
        */
        
}

public void PlayMenu() {
    menuSource.Play();
}

public void PlayExplore() {
    exploreSource.Play();
}

public void PlayFight() {
    fightSource.Play();
}

public void StopMenu() {
    menuSource.Stop();
}

public void StopExplore() {
    exploreSource.Stop();
}

public void StopFight() {
    fightSource.Stop();
}

public bool isExplorePlaying() {
    return exploreSource.isPlaying;
}

public bool isFightPlaying() {
    return fightSource.isPlaying;
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
