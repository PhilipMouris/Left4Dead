using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    protected bool isLevelFinished = false;
    protected string currentObjective;
    protected string extraObjective;
    protected NormalInfectantsManager normalInfectantsManager;

    protected SpecialInfectedManager specialInfectedManager;

    protected GameManager gameManager;

    protected HUDManager hUDManager;
    protected Player player;
    protected bool lost = false;


    

   
   
    public bool isLevelFinsihed(){
        return isLevelFinished;
    }
    // Update is called once per frame
    

    
}
