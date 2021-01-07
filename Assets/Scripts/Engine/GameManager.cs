using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    private GameObject pauseScreen;

    private SoundManager soundManager;
    private bool isPaused;

    private Level1Manager level1Manager;

    private Level2Manager level2Manager;

    private Level3Manager level3Manager;

    private Player player;

    private int level;


    // Start is called before the first frame update
    void Awake()
    {   
        player = GameObject.Find(EngineConstants.PLAYER).GetComponent<Player>();
        //level = 1;
        //isPaused = false;
        //pauseScreen = GameObject.Find(EngineConstants.PAUSE);
        //SetButtonListeners();
        //pauseScreen.SetActive(false);
        //this.soundManager = GameObject.Find(MenuConstants.AUDIO_MANAGER).GetComponent<SoundManager>();
        //InitializeLevelManagers();
        //InitializeScene();
        InitializePistol();
    }


    // Initialize(int dmg, int clipCapacity,int rateOfFire, int maxAmmo, GameObject weapon);
    void InitializePistol() {
        GameObject pistol = Resources.Load(WeaponsConstants.PISTOL_PATH) as GameObject;
        pistol.AddComponent<Weapon>();
        Weapon weapon = pistol.GetComponent<Weapon>();
        weapon.Initialize(WeaponsConstants.PISTOL_DAMAGE,
                          WeaponsConstants.PISTOL_CLIP_CAPACITY,
                          WeaponsConstants.PISTOL_RATE_OF_FIRE,
                          WeaponsConstants.PISTOL_MAX_AMMO,
                          pistol
                          );
        player.AddWeapon(weapon, true);
        //Debug.Log(weapon.getDmg() + "PISTOLLLL");
        
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // HandlePause();
        }
    }

    private void HandlePause()
    {
        this.isPaused = !this.isPaused;
        soundManager.HandlePause(this.isPaused);
        if (isPaused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            return;
        }
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }


    void Start()
    {
       

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

}
