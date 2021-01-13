using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gernade : MonoBehaviour
{
    
     public GameObject particleEffect;
     public AudioClip explosionSound;
     protected GernadeManager manager;
     
  protected NormalInfectantsManager infectantManager;
  protected HUDManager hudManager;
  protected int maxCapacity;

  protected GameManager gameManager;

  protected AudioMixerGroup SFXGroup;
     
protected string type;
    
    // Start is called before the first frame update
    

    void Start()
    {
        manager = FindObjectOfType<GernadeManager>();
        infectantManager = FindObjectOfType<NormalInfectantsManager>();
        hudManager = FindObjectOfType<HUDManager>();
        SFXGroup = GameObject.Find("SFXManager").GetComponent<SFXManager>().SFXGroup;
        
    }
    public string GetGernadeType(){
        return type;
    }
    public int GetMaxCapacity(){
        return maxCapacity;
    }
   
    public AudioSource GetAudioSource(){
        return manager.source;
    }
    // Update is called once per frame
    public AudioMixerGroup GetAudioMixerGroup() {
        return SFXGroup;
    }
}
