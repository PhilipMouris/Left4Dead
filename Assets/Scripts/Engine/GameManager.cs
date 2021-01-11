using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class GameManager : MonoBehaviour
{

    public GameObject CraftingScreen;

    public GameObject HUD;
    private GameObject pauseScreen;

    private SoundManager soundManager;
    
    private bool isPaused;

    private Level1Manager level1Manager;

    private Level2Manager level2Manager;

    private Level3Manager level3Manager;
    
    private WeaponsManager weaponsManager;

    private Player player;

    private int level;

    private HUDManager hudManager;

    public Camera FPS;

    public Camera craftingCamera;


    public static bool crafting_bool;
    // Start is called before the first frame update
    // void Awake()
    // {   
      
    // }

    void InitializeWeapon(string type, bool isSelected) {
        Weapon weapon = weaponsManager.InitializeWeapon(type);
        if(isSelected)
            player.SetWeapon(weapon);
        hudManager.AddWeapon(weapon, isSelected);
    }


    private void InitializeLevelManagers() {
         level1Manager = ScriptableObject.CreateInstance("Level1Manager") as Level1Manager;
         level2Manager = ScriptableObject.CreateInstance("Level2Manager") as Level2Manager;
         level3Manager = ScriptableObject.CreateInstance("Level3Manager") as Level3Manager;
    }


    private void InitializeScene(){
        switch(level){
            case 1:
                level1Manager.Initialize();
                break;
            case 2:
            //level2Manager.Initialize();
            default:
                break;
            //level3Manager.Initialize();

        }
    }


    private void HandleSwitchWeapons() {
        if(Input.GetButtonDown(PlayerConstants.DRAW_WEAPON_INPUT)){
            // hudManager.SetHealth(50);
            if(!player.GetIsweaponDrawn()) {
                player.HandleDrawWeapon();
            } else {
                  Weapon weapon = hudManager.SwitchWeapon();
                  player.SetWeapon(weapon);
            }
        }
    }


    private void HandleCraftingScreen() {
        if (Input.GetKeyDown(KeyCode.I))
        {
            FPS.enabled = !FPS.enabled;
            craftingCamera.enabled = !craftingCamera.enabled;
            crafting_bool = !crafting_bool;
            CraftingScreen.SetActive(crafting_bool);
            GameObject.Find("FPSController 1").GetComponent<FirstPersonController>().isCrafting = crafting_bool;
            HandlePause();
        }
    }

    void Update()
    {
        HandleCraftingScreen();

        HandleSwitchWeapons();
        // if (Input.GetKeyDown("1"))
        // {
        //     InitializeWeapon(WeaponsConstants.SHOT_GUN_DATA,false);
        // }
        // if (Input.GetKeyDown("2"))
        // {
        //     InitializeWeapon(WeaponsConstants.SMG_DATA,false);
        // }

        // if (Input.GetKeyDown("3"))
        // {
        //     InitializeWeapon(WeaponsConstants.HUNTING_RIFLE_DATA,false);
        // }

        // if (Input.GetKeyDown("4"))
        // {
        //     InitializeWeapon(WeaponsConstants.ASSAULT_RIFLE_DATA,false);
        // }

    }

    private void HandlePause()
    {
        this.isPaused = !this.isPaused;
        //soundManager.HandlePause(this.isPaused);
        if (isPaused)
        {
            Time.timeScale = 0;
            //pauseScreen.SetActive(true);
            HUD.SetActive(false);
            return;
        }
        Time.timeScale = 1;
        //pauseScreen.SetActive(false);
        HUD.SetActive(true);
    }


    void Start()
    {
       
        player = GameObject.Find(EngineConstants.PLAYER).GetComponent<Player>();
        hudManager = GameObject.Find(EngineConstants.HUD).GetComponent<HUDManager>();
        weaponsManager = GameObject.Find(EngineConstants.WEAPONS_MANAGER).GetComponent<WeaponsManager>();
        //level = 1;
        //isPaused = false;
        //pauseScreen = GameObject.Find(EngineConstants.PAUSE);
        //SetButtonListeners();
        //pauseScreen.SetActive(false);
        //this.soundManager = GameObject.Find(MenuConstants.AUDIO_MANAGER).GetComponent<SoundManager>();
        //InitializeLevelManagers();
        //InitializeScene();
        //InitializePistol();
        // InitializeWeapon(WeaponsConstants.PISTOL_DATA, true, WeaponsConstants.PISTOL_TRANSFORMATIONS, WeaponsConstants.PISTOL_CAMERA_DATA);
        // InitializeWeapon(WeaponsConstants.ASSAULT_RIFLE_DATA, false, WeaponsConstants.RIFLE_TRANSFORMATIONS, WeaponsConstants.RIFLE_CAMERA_DATA);
        // InitializeWeapon(WeaponsConstants.SHOT_GUN_DATA, false, WeaponsConstants.SHOT_GUN_TRANSFORMATIONS, WeaponsConstants.RIFLE_CAMERA_DATA);
        // InitializeWeapon(WeaponsConstants.HUNTING_RIFLE_DATA, false, WeaponsConstants.HUNTING_RIFLE_TRANSFORMATIONS, WeaponsConstants.RIFLE_CAMERA_DATA);
        // //InitializeWeapon(WeaponsConstants.HUNTING_RIFLE_DATA, false, WeaponsConstants.HUNTING_RIFLE_TRANSFORMATIONS, WeaponsConstants.RIFLE_CAMERA_DATA);
        // InitializeWeapon(WeaponsConstants.SMG_DATA, false, WeaponsConstants.SMG_TRANSFORMATIONS, WeaponsConstants.SMG_CAMERA_DATA);
        

        InitializeWeapon(WeaponsConstants.WEAPON_TYPES["PISTOL"], true);
        InitializeWeapon(WeaponsConstants.WEAPON_TYPES["ASSAULT_RIFLE"],false);
        InitializeWeapon(WeaponsConstants.WEAPON_TYPES["SMG"],false);
        InitializeWeapon(WeaponsConstants.WEAPON_TYPES["HUNTING_RIFLE"],false);
        InitializeWeapon(WeaponsConstants.WEAPON_TYPES["SHOTGUN"],false);
        crafting_bool = false;
        FPS.enabled = true;
        craftingCamera.enabled = false;

    
    
    
    
    }

    private void onQuit()
    {
        soundManager.PlayButtonClick();
        SceneManager.LoadScene(MenuConstants.MENU_SCENE);
    }

    private void onResume()
    {   
        this.soundManager.PlayButtonClick();
        HandlePause();
    }

    private void SetButtonListeners()
    {    
        // GameObject[] restartButtons = GameObject.FindGameObjectsWithTag(Constants.RESTART_BUTTON);
        GameObject[] quitButtons = GameObject.FindGameObjectsWithTag(Constants.QUIT_BUTTON);

        for (int i = 0; i < quitButtons.Length; i++)
        {

            quitButtons[i].GetComponent<Button>().onClick.AddListener(onQuit);
        }
       
        GameObject.Find(EngineConstants.RESUME).GetComponent<Button>().onClick.AddListener(onResume);

    }


    // Update is called once per frame

    public void SetHealth(int health)
    {
        hudManager.SetHealth(health);
    }

    public int GetHealth()
    {
        return hudManager.GetHealth();
    }

}
