using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    protected NormalInfectantsManager normalInfectantsManager;

    protected SpecialInfectedManager specialInfectedManager;

    protected GameManager gameManager;

    protected HUDManager hUDManager;
    protected Player player;
    protected GernadeManager gernadeManager;
    


    
    void Awake(){
        normalInfectantsManager = FindObjectOfType<NormalInfectantsManager>();
        specialInfectedManager = FindObjectOfType<SpecialInfectedManager>();
        gameManager = FindObjectOfType<GameManager>();
        hUDManager = FindObjectOfType<HUDManager>();
        player = FindObjectOfType<Player>();
        gernadeManager = FindObjectOfType<GernadeManager>();
    }
   
   
    void Update(){
        if(Input.GetKeyDown(KeyCode.Alpha0)){ //Kill All Infected in Level
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha7)){
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)){
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha9)){
            
        }
    }
    // Update is called once per frame
    

    
}
