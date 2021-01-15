using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnTheGameOn.ArrowWaypointer;
public class Level3Manager : LevelManager
{
    // Start is called before the first frame update

    GameObject destination;
    private bool reachedDestination = false;
    private bool isRescued = false;
    private bool killedAll = false;
    private int timeLeft = EngineConstants.TIME_TO_RESCUE;
    private int remainingMembers;
    
    void UpdateTotalRemainingMembers()
    {
        int remainingNormal = normalInfectantsManager.GetRemainingNormalInfected();
        int remainingSpecial = specialInfectedManager.GetRemainingSpecialInfected();
        remainingMembers = remainingNormal + remainingSpecial;
        if (remainingMembers <=60)
        {
            killedAll = true;
        }
    }

    void Awake()
    {
        normalInfectantsManager = FindObjectOfType<NormalInfectantsManager>();
        specialInfectedManager = FindObjectOfType<SpecialInfectedManager>();
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.SetRescueLevel();
        hUDManager = FindObjectOfType<HUDManager>();
        destination = GameObject.Find("Destination");
        SetHordeLocations();
        SetHordeArea();
    }
    void Rescue()
    {
        isRescued = true;
        gameManager.SetRescued();
        timeLeft = 0;
        extraObjective = "";

    }

    void UpdateHUD()
    {

        hUDManager.SetCurrentObjective(currentObjective);
        hUDManager.SetExtraObjective(extraObjective);
    }
    public void SetCurrentObjective(string objective)
    {
        currentObjective = objective;
    }
    public void UpdateObjective(){
        if(!killedAll)
            currentObjective = "Kill "+remainingMembers.ToString() + " remaining infected members";
    }
    public void SetExtraObjective()
    {
        extraObjective = "Time Left: " + timeLeft.ToString();
    }
    void CheckDistanceToDest()
    {

        float distance = Vector3.Distance(CompanionConstants.KIDNAPPED_TRANSFORMATION.Item1, player.transform.position);
        if (distance <= 5.0f)
        {
            if (!reachedDestination)
            {
                Rescue();
                reachedDestination = true;
                // GameObject.FindObjectOfType<WaypointController>().gameObject.SetActive(false);
            }
        }



    }

    void Start()
    {
        hUDManager.SetCurrentLevel(3);
        SetCurrentObjective("Follow The Arrow To Save the Kidnapped.");
        SetExtraObjective();

        InvokeRepeating("UpdateTimeLeft", 1, 1);
        // SetHordeLocations();
    }
    void InitializeCompanion()
    {
        gameManager.InitializeCompanion(gameManager.GetCompanionName());
    }
    void UpdateTimeLeft()
    {
        timeLeft--;
    }
    void SetHordeLocations()
    {
        GameObject locations = GameObject.Find("HordeLocations3");
        normalInfectantsManager.SetHordeLocations(locations);
        normalInfectantsManager.SpawnHorde();
    }
    void SetHordeArea()
    {
        GameObject area = Resources.Load(EngineConstants.AREAS_PATH + "Level3Horde") as GameObject;
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
    void CheckFinishLevel()
    {
        if (reachedDestination & killedAll)
        {
            isLevelFinished = true;
            currentObjective = "LEVEL PASSED";
        }
    }

    void CheckLost(){
        if(timeLeft==0 && !reachedDestination && !lost){
            lost=true;
            currentObjective = "Companion Killed";
            UpdateHUD();
            // Time.timeScale = 0f;
            // Invoke("HandleLost",2);
            HandleLost();
        }
    }
    void HandleLost(){
        gameManager.HandleGameOverScreen();
        
    }
    // Update is called once per frame
    void Update()
    {

       
        if (!reachedDestination)
        {
            SetExtraObjective();
        }else{
            UpdateTotalRemainingMembers();
            UpdateObjective();
        }
        CheckDistanceToDest();
        CheckFinishLevel();
        CheckLost();
        UpdateHUD();
        
    }


}
