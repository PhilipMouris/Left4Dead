using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



    // Start is called before the first frame update
public class SoundManager : MonoBehaviour

{

    private AudioSource GameBackroundAudio;
    private AudioSource MenuBackgroundAudio;
    private AudioSource ButtonClickAudio;
    //private AudioListener audioListener;
    private bool isMuted = false;
  

    // Start is called before the first frame update
    void Awake()
    {   
        SetAudioSources();
        SetAudioClips();
        PlayBackGround();

    }

    private void SetAudioClips(){
      
        this.ButtonClickAudio.clip = Resources.Load<AudioClip>("Sounds/Menus/ButtonClick");
        this.GameBackroundAudio.clip= Resources.Load<AudioClip>("Sounds/Menus/PiercingLight");
        this.GameBackroundAudio.volume = 0.2f;
        this.GameBackroundAudio.loop = true;
        this.MenuBackgroundAudio.clip= Resources.Load<AudioClip>("Sounds/Menus/Menus");
        this.MenuBackgroundAudio.loop = true;
    }

    private void SetAudioSources() {
        this.GameBackroundAudio = gameObject.AddComponent<AudioSource>();
        this.MenuBackgroundAudio = gameObject.AddComponent<AudioSource>();
        this.ButtonClickAudio = gameObject.AddComponent<AudioSource>();
    }

    public void HandlePause(bool isPaused){
        if(isPaused){
            this.GameBackroundAudio.Pause();
            this.MenuBackgroundAudio.Play();
            return;
        }
        this.GameBackroundAudio.Play();
        this.MenuBackgroundAudio.Pause();

    }
    
    private void PlayBackGround(){
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == MenuConstants.GAME_SCENE){
            this.GameBackroundAudio.Play();
            return;
        }
        this.MenuBackgroundAudio.Play();
    }

    public void PlayButtonClick(){
        this.ButtonClickAudio.Play();
    }


     public bool HandleMute() {
        isMuted = !isMuted;
        if(isMuted){
            AudioListener.volume = 0.0f;
        }
        else{
           AudioListener.volume = 1.0f;
        }
        return isMuted;
    }

    public void EndGame() {
        this.GameBackroundAudio.Pause();
        this.MenuBackgroundAudio.Play();
    }

    
}

