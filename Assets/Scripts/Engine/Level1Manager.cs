using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : ScriptableObject
{
    // Start is called before the first frame update

    private HealthPackManager healthPackManager;
    private NormalInfectantsManager normalInfectantsManager;

    private AmmoPackManager ammoPackManager;

    private WeaponsManager weaponsManager;

    private IngredientsManager ingredientsManager;

    void Awake(){
        InitializeManagers();
    }

    private void InitializeManagers(){
        //  healthPackManager = ScriptableObject.CreateInstance("HealthPackManager") as HealthPackManager;
        //  normalInfectantsManager = ScriptableObject.CreateInstance("NormalInfectantsManager") as NormalInfectantsManager;
        //  ammoPackManager = ScriptableObject.CreateInstance("AmmoPackManager") as AmmoPackManager;
         //weaponsManager =  ScriptableObject.CreateInstance("WeaponsManager") as WeaponsManager;
         //ingredientsManager =  ScriptableObject.CreateInstance("IngredientsManager") as IngredientsManager;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(){
        Debug.Log("INITIALL");
    }
}
