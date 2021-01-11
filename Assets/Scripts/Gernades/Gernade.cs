using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gernade : MonoBehaviour
{
    
     public GameObject particleEffect;
     public AudioClip explosionSound;
     protected GernadeManager manager;
     
  protected NormalInfectantsManager infectantManager;
     
     
    
    // Start is called before the first frame update
    

    void Start()
    {
        manager = FindObjectOfType<GernadeManager>();
        infectantManager = FindObjectOfType<NormalInfectantsManager>();
        
    }
   
    public AudioSource GetAudioSource(){
        return manager.source;
    }
    // Update is called once per frame

}
