using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : LevelManager
{
    // Start is called before the first frame update

    GameObject destination;
    private bool reachedDestination = false;
    private bool isRescued = false;
    private int timeLeft = EngineConstants.TIME_TO_RESCUE;
    
    void Awake()
    {
        normalInfectantsManager = FindObjectOfType<NormalInfectantsManager>();
        specialInfectedManager = FindObjectOfType<SpecialInfectedManager>();
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        hUDManager = FindObjectOfType<HUDManager>();
        destination = GameObject.Find("Destination");
        SetHordeLocations();
        SetHordeArea();
    }
    void UpdateObjective()
    {
        hUDManager.SetCurrentObjective(currentObjective);
        hUDManager.SetExtraObjective(extraObjective);
    }
    public void SetCurrentObjective(string objective)
    {
        currentObjective = objective;
    }
    public void UpdateExtraObjective(){
        extraObjective = "Time Left: "+timeLeft.ToString();
    }

    void Start()
    {
        hUDManager.SetCurrentLevel(3);
        SetCurrentObjective("Follow The Arrow To Save the Kidnapped.");
        UpdateExtraObjective();
        InvokeRepeating("UpdateTimeLeft",0,1);
        // SetHordeLocations();
    }
    void UpdateTimeLeft(){
        timeLeft--;
    }
    void SetHordeLocations()
    {
        GameObject locations = GameObject.Find("HordeLocations2");
        normalInfectantsManager.SetHordeLocations(locations);
        normalInfectantsManager.SpawnHorde();
    }
    void SetHordeArea()
    {
        GameObject area = Resources.Load(EngineConstants.AREAS_PATH+"Level2Horde") as GameObject;
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
        if (reachedDestination)
        {
            isLevelFinished = true;
            currentObjective = "LEVEL PASSED";
        }
    }
    void CheckDangerZone()
    {
        if (normalInfectantsManager.isInsideDanger())
        {
            currentObjective = "DANGER ZONE !!!!";
        }
        else
        {
            currentObjective = "Follow the smoke in sky without entering Danger Zone";
        }
    }
    void CheckDistanceToDest()
    {

        float distance = Vector3.Distance(destination.transform.position, player.transform.position);
        if (distance <= 3.0f)
        {
            reachedDestination = true;
        }
        else
        if (distance < 40.0f)
        {
            currentObjective = "ONLY " + Mathf.Floor(distance).ToString() + " Meters Left";
            close = true;
        }
        else
        {
            close = false;
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (!isLevelFinished)
        {
            CheckDistanceToDest();
        
            CheckDangerZone();
        }
        UpdateObjective();
        CheckFinishLevel();

    }


}
