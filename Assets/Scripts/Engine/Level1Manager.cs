using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : LevelManager
{
    // Start is called before the first frame update

    
    private int remainingMembers = 0;
    void UpdateTotalRemainingMembers(){
        int remainingNormal = normalInfectantsManager.GetRemainingNormalInfected();
        int remainingSpecial = specialInfectedManager.GetRemainingSpecialInfected();
        remainingMembers = remainingNormal + remainingSpecial;
    }
    void UpdateObjective(){
        if(!isLevelFinished){
        currentObjective = "Kill "+remainingMembers.ToString() + " remaining infected members";
        }else{
            currentObjective = "LEVEL PASSED";
        }
        hUDManager.SetCurrentObjective(currentObjective);
    }
    void CheckFinishLevel(){
        if(remainingMembers<=45){
            isLevelFinished=true;
        }
    }
    
    void Start()
    {
        hUDManager.SetCurrentLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTotalRemainingMembers();
        UpdateObjective();
        CheckFinishLevel();
    }

    
}
