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

    public GameObject PauseScreen;

    public GameObject HUD;
    
    private GameObject pauseScreen;

    private SoundManager soundManager;
    
    private Craft craftingManager;

    private bool isPaused;

    private Level1Manager level1Manager;

    private Level2Manager level2Manager;

    private Level3Manager level3Manager;

    private WeaponsManager weaponsManager;

    private GernadeManager gernadeManager;

    private Player player;

    private int level;

    private HUDManager hudManager;

    private Camera FPS;

    private Camera craftingCamera;

    private Camera TPS;
    
    private Camera pauseCamera;

    private static String companion;

    private bool isChasing;

    private float throwingPower = 3;

    public static bool crafting_bool;
    
    public static bool isPauseScreen;

    private bool isRaged ;


    // Start is called before the first frame update
    void Awake()
    {
        crafting_bool = false;
        FPS = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        craftingCamera = GameObject.Find("CraftingCamera").GetComponent<Camera>();
        pauseCamera = GameObject.Find("PauseCamera").GetComponent<Camera>();
        TPS = GameObject.Find("ThirdPersonCamera").GetComponent<Camera>();
        craftingManager = GameObject.Find("CraftingManager").GetComponent<Craft>();
        FPS.enabled = true;
        TPS.enabled = true;
        craftingCamera.enabled = false;
        pauseCamera.enabled = false;
        //   Debug.Log(FPS.enabled + " FPS");
        //   De
    }

    void InitializeWeapon(string type, bool isSelected)
    {
        Weapon weapon = weaponsManager.InitializeWeapon(type);
        if (isSelected)
            player.SetWeapon(weapon);
        hudManager.AddWeapon(weapon, isSelected);
    }
    void InitializeGernades()
    {
        hudManager.AddAllGernades();
    }


    private void InitializeLevelManagers()
    {
        level1Manager = ScriptableObject.CreateInstance("Level1Manager") as Level1Manager;
        level2Manager = ScriptableObject.CreateInstance("Level2Manager") as Level2Manager;
        level3Manager = ScriptableObject.CreateInstance("Level3Manager") as Level3Manager;
    }


    private void InitializeScene()
    {
        switch (level)
        {
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

    private void HandleSwitchGrenades()
    {
        if (Input.GetButtonDown(PlayerConstants.SWITCH_GRENADE))
        {
            // hudManager.SetHealth(50);
            Debug.Log("Switching");
            Gernade gernade = hudManager.SwitchGrenade();
            player.SetGrenade(gernade);

        }
    }
    public void ResetGrenadeInfo()
    {
        throwingPower = 3f;

    }
    private void HandleThrowGrenade()
    {
        if (!hudManager.CheckAllEmptyGrenades())
        {
            if (Input.GetMouseButton(1))
            {  if (throwingPower < PlayerConstants.THROWING_POWER_MAX)
                {   
                    throwingPower += 0.2f;
                    hudManager.ChangePowerBar(Convert.ToInt32((0.2/7f) * 100));
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                // Debug.Log(gernades.Count + " COUNT?????");
                hudManager.ChangePowerBar(-100);
                player.ThrowGrenade(throwingPower);
                hudManager.RemoveCurrentGernade();
                this.ResetGrenadeInfo();
            }
        }
        else
        {
            // Debug.Log("NO GRENADES AVAILABLE");
        }
    }

    

    public static void SetCompanion(String companionName) {
        companion = companionName;
        Debug.Log(companion);
    }

    //private void HandleSwitchWeapons() {
      //  if(Input.GetButtonDown(PlayerConstants.DRAW_WEAPON_INPUT)){

    private void HandleSwitchWeapons()
    {   
        if (Input.GetButtonDown(PlayerConstants.DRAW_WEAPON_INPUT))
        {
            // hudManager.SetHealth(50);
            if (!player.GetIsweaponDrawn())
            {
                player.HandleDrawWeapon();
            }
            else
            {
                Weapon weapon = hudManager.SwitchWeapon();
                player.SetWeapon(weapon);
            }

            PlaySwitch();
        }
    }


    private void HandleCraftingScreen()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            crafting_bool = !crafting_bool;
            FPS.enabled = false;
            TPS.enabled = false;
            craftingCamera.enabled = true;
            CraftingScreen.SetActive(crafting_bool);
            
            GameObject.Find("FPSController").GetComponent<FirstPersonController>().GetMouseLook().SetCursorLock(!crafting_bool);
            this.craftingManager.FindObjects();
            HandlePause();
        }
    }

    private void HandlePauseScreen() {
        if(!isPauseScreen) {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseScreen.SetActive(true);
            CraftingScreen.SetActive(false);
            FPS.enabled = false;
            TPS.enabled = false;
            craftingCamera.enabled = false;
            pauseCamera.enabled = true;
            isPauseScreen = true;
            //GameObject.Find("FPSController").GetComponent<FirstPersonController>().GetMouseLook().SetCursorLock(false);
            HandlePause();
        }
    }
        
    }

    private void HandlePickUpWeapon() {
        if(Input.GetKeyDown(KeyCode.E)){
            Weapon weapon = player.GetWeaponInRange();
            if(!weapon) return;
            Weapon oldWeapon = weaponsManager.GetWeapon(weapon.GetType());
            if(!oldWeapon) {
                InitializeWeapon(weapon.GetType(),false);
                weaponsManager.PickUp(weapon,false);
            }
            else {
                oldWeapon.Reset();
               weaponsManager.PickUp(weapon, true);
            }
         
        }
    }

    public void EnemyDead(string type) {
        if(type=="normal"){
            hudManager.ChangeRage(10);
        }
        else hudManager.ChangeRage(50);
    }

    private void HandleActivateRage() {
        if(isRaged) return;
        if(Input.GetKeyDown(KeyCode.F)){
            hudManager.ActivateRage();
            PlayRage();
        } 
    }

    public void SetRage(bool rage) {
        this.isRaged = rage;
    }

    public bool GetIsRaged() {
        return isRaged;
    }



    void Update()
    {
        HandleCraftingScreen();
        HandleSwitchWeapons();
        HandleSwitchGrenades();
        HandleThrowGrenade();
        HandlePickUpWeapon();
        HandlePauseScreen();
        HandleMusic();
        HandleActivateRage();

        if(Input.GetKeyDown(KeyCode.H)){
            hudManager.ChangeRage(+30);
        }
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
            AudioListener.pause = !AudioListener.pause;
            return;
        }
        Time.timeScale = 1;
        //pauseScreen.SetActive(false);
        AudioListener.pause = !AudioListener.pause;
        HUD.SetActive(true);
    }

    public void ResumeGame() {
        PauseScreen.SetActive(false);
        FPS.enabled = true;
        TPS.enabled = true;
        craftingCamera.enabled = false;
        pauseCamera.enabled = false;
        isPauseScreen = false;
        HandlePause();
    }

    public void RestartGame() {
         SceneManager.LoadScene("OutdoorsLevel");
    }

    public void QuitToMain() {
        SceneManager.LoadScene("Menus");
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
      
        hudManager.SetPlayer(player);

        // InitializeWeapon(WeaponsConstants.WEAPON_TYPES["PISTOL"], true);
        // InitializeWeapon(WeaponsConstants.WEAPON_TYPES["ASSAULT_RIFLE"], false);
        // InitializeWeapon(WeaponsConstants.WEAPON_TYPES["SMG"], false);
        // InitializeWeapon(WeaponsConstants.WEAPON_TYPES["HUNTING_RIFLE"], false);
        // InitializeWeapon(WeaponsConstants.WEAPON_TYPES["SHOTGUN"], false);

        InitializeGernades();
        // InitializeGernade(WeaponsConstants.WEAPON_TYPES["MOLOTOV_COCKTAIL"],false);
        // InitializeGernade(WeaponsConstants.WEAPON_TYPES["PIPE_BOMB"],false);
        // InitializeGernade(WeaponsConstants.WEAPON_TYPES["STUN_BOMB"],false);


        InitializeWeapon(WeaponsConstants.WEAPON_TYPES["PISTOL"], true);
        
        // InitializeWeapon(WeaponsConstants.WEAPON_TYPES["ASSAULT_RIFLE"],false);
        // InitializeWeapon(WeaponsConstants.WEAPON_TYPES["SMG"],false);
        // InitializeWeapon(WeaponsConstants.WEAPON_TYPES["HUNTING_RIFLE"],false);
        // InitializeWeapon(WeaponsConstants.WEAPON_TYPES["SHOTGUN"],false);
        //GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayExplore();

    }

    private void onQuit()
    {
        soundManager.PlayButtonClick();
        SceneManager.LoadScene(MenuConstants.MENU_SCENE);
    }

    private void PlayExplore() {
        GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayExplore();
    }

    private void PlayFight() {
        GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayFight();
    }

    private void PlayRage() {
        GameObject.Find("SFXManager").GetComponent<SFXManager>().PlayRage();
    }

    private void PlayChasing() {
        GameObject.Find("SFXManager").GetComponent<SFXManager>().PlayChasing();
    }
    
    private void PlaySwitch() {
        GameObject.Find("SFXManager").GetComponent<SFXManager>().PlaySwitch();
    }

    private void StopFight() {
        GameObject.Find("MusicManager").GetComponent<MusicManager>().StopFight();
    }

    private void StopExplore() {
        GameObject.Find("MusicManager").GetComponent<MusicManager>().StopExplore();
    }

    private void StopChasing() {
        GameObject.Find("SFXManager").GetComponent<SFXManager>().StopChasing();
    }
    
    private void HandleMusic() {
        Debug.Log(isChasing + "chasing");
        if(!isChasing) {
            if(!GameObject.Find("MusicManager").GetComponent<MusicManager>().isExplorePlaying()) {
                PlayExplore();
                StopFight();
            }
            
        }

        else {
            if(!GameObject.Find("SFXManager").GetComponent<SFXManager>().isChasingPlaying()) {
                PlayChasing();
            }
            
            if(!GameObject.Find("MusicManager").GetComponent<MusicManager>().isFightPlaying()) {
                PlayFight();
                StopExplore();
            }
        }
    }

    public void SetChasing(bool chasing) {
        isChasing = chasing;

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
        hudManager.ChangeHealth(health);
    }

    public int GetHealth()
    {
        return hudManager.GetHealth();
    }

}
