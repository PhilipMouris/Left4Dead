using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLevelsController : MonoBehaviour
{
    public AudioMixer masterMixer;
    // Start is called before the first frame update
    public void MusicController(float level) {
        masterMixer.SetFloat("MusicVol", Mathf.Log(level) * 20);
    }

    public void SFXController(float level) {
        masterMixer.SetFloat("SFXVol", Mathf.Log(level) * 20);
    }

    public void SpeechController(float level) {
        masterMixer.SetFloat("SpeechVol", Mathf.Log(level) * 20);

    }
}
