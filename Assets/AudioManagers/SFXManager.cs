using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioClip SFX;
    private AudioSource slowpaced;
    public AudioMixerGroup SFXGroup;
    void Start()
    {
        slowpaced = gameObject.AddComponent<AudioSource>();
        SFX = Resources.Load<AudioClip>("Audio/Music/slowpaced");
        Debug.Log(SFX);
        slowpaced.outputAudioMixerGroup = SFXGroup;
        slowpaced.clip = SFX;
        //slowpaced.Play();

        



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
