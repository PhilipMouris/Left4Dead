using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioClip rageClip;
    private AudioSource rageSource;

    private AudioClip switchClip;
    private AudioSource switchSource;

    private AudioClip chasingClip;
    private AudioSource chasingSource;
    public AudioMixerGroup SFXGroup;

    private AudioClip deadClip;
    private AudioSource deadSource;

    private AudioClip specialDeadClip;
    private AudioSource specialDeadSource;

    private AudioClip hitClip;
    private AudioSource hitSource;
    void Start()
    {
        rageSource = gameObject.AddComponent<AudioSource>();
        rageClip = Resources.Load<AudioClip>("Audio/SFX/rage");
        rageSource.outputAudioMixerGroup = SFXGroup;
        rageSource.clip = rageClip;
        
        switchSource = gameObject.AddComponent<AudioSource>();
        switchClip = Resources.Load<AudioClip>("Audio/SFX/switching_weapon");
        switchSource.outputAudioMixerGroup = SFXGroup;
        switchSource.clip = switchClip;

        chasingSource = gameObject.AddComponent<AudioSource>();
        chasingClip = Resources.Load<AudioClip>("Audio/SFX/infected_rushing");
        chasingSource.outputAudioMixerGroup = SFXGroup;
        chasingSource.clip = chasingClip;

        deadSource = gameObject.AddComponent<AudioSource>();
        deadClip = Resources.Load<AudioClip>("Audio/SFX/joel_dead");
        deadSource.outputAudioMixerGroup = SFXGroup;
        deadSource.clip = deadClip;
        
        specialDeadSource = gameObject.AddComponent<AudioSource>();
        specialDeadClip = Resources.Load<AudioClip>("Audio/SFX/special_dead");
        specialDeadSource.outputAudioMixerGroup = SFXGroup;
        specialDeadSource.clip = specialDeadClip;
        
        hitSource = gameObject.AddComponent<AudioSource>();
        hitClip = Resources.Load<AudioClip>("Audio/SFX/joel_hit");
        hitSource.outputAudioMixerGroup = SFXGroup;
        hitSource.clip = hitClip;


    }

    public void PlayRage() {
        rageSource.PlayOneShot(rageSource.clip);
    }

    public void PlaySwitch() {
        switchSource.PlayOneShot(switchSource.clip);
    }


    public void PlayChasing() {
        chasingSource.PlayOneShot(chasingSource.clip);
    }

    public void PlayJoelDead() {
        deadSource.PlayOneShot(deadSource.clip);
    }

    public void PlaySpecialDead() {
        specialDeadSource.PlayOneShot(specialDeadSource.clip);
    }

    public void PlayHit() {
        hitSource.PlayOneShot(hitSource.clip);
    }

    public void StopChasing() {
        chasingSource.Stop();
    }
    public bool isChasingPlaying() {
        return chasingSource.isPlaying;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}