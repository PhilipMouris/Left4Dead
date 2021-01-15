using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : LevelManager
{
    // Start is called before the first frame update

    void Awake(){
        normalInfectantsManager = FindObjectOfType<NormalInfectantsManager>();
        specialInfectedManager = FindObjectOfType<SpecialInfectedManager>();
        gameManager = FindObjectOfType<GameManager>();
        hUDManager = FindObjectOfType<HUDManager>();
         SetHordeLocations();
         SetHordeArea();
    }
     void SetHordeLocations(){
        GameObject locations = GameObject.Find("HordeLocations");
        normalInfectantsManager.SetHordeLocations(locations);
        normalInfectantsManager.SpawnHorde();
    }
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
     void SetHordeArea()
    {
        GameObject area = Resources.Load(EngineConstants.AREAS_PATH+"Level1Horde") as GameObject;
        BoxCollider box = normalInfectantsManager.gameObject.AddComponent<BoxCollider>();
        if (area)
        {
            box.center = area.GetComponent<BoxCollider>().center;
            box.size = area.GetComponent<BoxCollider>().size;
            box.isTrigger = area.GetComponent<BoxCollider>().isTrigger;
            // box.transform.position = area.GetComponent<BoxCollider>().transform.position;
        }
        else
        {
            Debug.Log("NO AREA");
        }

    }
    void CheckFinishLevel(){
        if(remainingMembers<=0){
            isLevelFinished=true;
        }
    }
    
    void Start()
    {
        hUDManager.SetCurrentLevel(1);
    }
     void CheckLost(){
        if(hUDManager.isPlayerDead() && !lost){
            lost=true;
            gameManager.PlayerDie();
            GameObject.Find("SFXManager").GetComponent<SFXManager>().PlayJoelDead();
            // Time.timeScale = 0f;
            Invoke("HandleLost",4);
            //
        }

        // if(lost && !isDying){
        //      if(!GameObject.FindObjectOfType<Player>().GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("die"))
        //        HandleLost();
             
        // }
    }
    void HandleLost(){
        gameManager.HandleGameOverScreen();
        
    }


    // Update is called once per frame
    void Update()
    {
        if(!lost){
        UpdateTotalRemainingMembers();
        UpdateObjective();
        CheckFinishLevel();
        }
        CheckLost();
    }

    
}
