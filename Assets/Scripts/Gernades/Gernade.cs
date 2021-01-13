using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gernade : MonoBehaviour
{
    
     public GameObject particleEffect;
     public AudioClip explosionSound;
     protected GernadeManager manager;
     
  protected NormalInfectantsManager infectantManager;
  protected HUDManager hudManager;
  protected int maxCapacity;

  protected GameManager gameManager;
     
protected string type;
    
    // Start is called before the first frame update
    

    void Start()
    {
        manager = FindObjectOfType<GernadeManager>();
        infectantManager = FindObjectOfType<NormalInfectantsManager>();
        hudManager = FindObjectOfType<HUDManager>();
        
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

}
