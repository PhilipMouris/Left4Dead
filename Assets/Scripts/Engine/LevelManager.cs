using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    protected bool isLevelFinished = false;
    protected string currentObjective;
    protected NormalInfectantsManager normalInfectantsManager;

    protected SpecialInfectedManager specialInfectedManager;

    protected GameManager gameManager;

    protected HUDManager hUDManager;


    void Awake(){
        normalInfectantsManager = FindObjectOfType<NormalInfectantsManager>();
        specialInfectedManager = FindObjectOfType<SpecialInfectedManager>();
        gameManager = FindObjectOfType<GameManager>();
        hUDManager = FindObjectOfType<HUDManager>();
    }

   
   
    public bool isLevelFinsihed(){
        return isLevelFinished;
    }
    // Update is called once per frame
    

    
}
