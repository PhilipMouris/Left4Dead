using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    private Button startButton;
    private Button quitButton;
    private Button muteButton;
    private Button optionsButton;
    private Button guideButton;
    private Button creditsButton;
    private Button moreInfoButton;
    private GameObject[] screens;
    private GameObject mainMenu;
    private GameObject optionsScreen;
    private GameObject creditsScreen;
    private GameObject moreInfoScreen;
    private GameObject guide;
    private int currentScreen;
    private int prevScreen;
    private GameObject[] backButtons;
    //private static bool isMuted = false;
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Awake()
    {
        
        SetButtons();
        SetScreens();
        this.currentScreen = 0;
        this.prevScreen = 0;
        this.backButtons = GameObject.FindGameObjectsWithTag(Constants.BACK_BUTTON_MAIN_MENU);
        SetButtonListeners();
        SetBackButtonListeners();
        SetActiveScreen(this.currentScreen);
        this.soundManager = GameObject.Find(MenuConstants.AUDIO_MANAGER).GetComponent<SoundManager>();

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetButtons(){
        this.startButton = GameObject.Find(MenuConstants.START).GetComponent<Button>();
        this.quitButton = GameObject.Find(MenuConstants.QUIT).GetComponent<Button>();
        this.muteButton = GameObject.Find(MenuConstants.MUTE).GetComponent<Button>();
        this.optionsButton = GameObject.Find(MenuConstants.OPTIONS).GetComponent<Button>();
        this.guideButton = GameObject.Find(MenuConstants.GUIDE).GetComponent<Button>();
        this.creditsButton = GameObject.Find(MenuConstants.CREDITS).GetComponent<Button>();
        this.moreInfoButton = GameObject.Find(MenuConstants.MORE_INFO).GetComponent<Button>();
    }

    private void SetScreens() {
        GameObject mainMenu = GameObject.Find(MenuConstants.MAIN_MENU);
        GameObject optionsScreen = GameObject.Find(MenuConstants.OPTIONS_SCREEN);
        GameObject creditsScreen = GameObject.Find(MenuConstants.CREDITS_SCREEN);
        GameObject moreInfoScreen = GameObject.Find(MenuConstants.MORE_INFO_SCREEN);
        GameObject guide = GameObject.Find(MenuConstants.GUIDE_SCREEN);
        this.screens = new GameObject[] { mainMenu, optionsScreen, creditsScreen, guide, moreInfoScreen};
    }

    private void SetButtonListeners()
    {
        this.startButton.onClick.AddListener(onStart);
        this.quitButton.onClick.AddListener(onQuit);
        // this.muteButton.onClick.AddListener(onMute);
        this.optionsButton.onClick.AddListener(onOptionsClick);
        this.guideButton.onClick.AddListener(onGuideClick);
        this.creditsButton.onClick.AddListener(onCreditsClick);
        this.moreInfoButton.onClick.AddListener(onMoreInfoClick);
    }

    private void SetBackButtonListeners()
    {
        for(int i = 0; i < this.backButtons.Length; i++)
        {   
            this.backButtons[i].GetComponent<Button>().onClick.AddListener(onBack);
     
        }
    }



    private void onGuideClick()
    {
       soundManager.PlayButtonClick();
       SetActiveScreen(3);
    }

    private void onCreditsClick()
    {
        soundManager.PlayButtonClick(); 
        SetActiveScreen(2);
    }

    private void onStart()
    {   soundManager.PlayButtonClick();
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    private void onQuit()
    {
        soundManager.PlayButtonClick();
        Application.Quit();
    }

    private void onOptionsClick()
    {   
        soundManager.PlayButtonClick();
        SetActiveScreen(1);
    }

    private void onMoreInfoClick()
    {   
        soundManager.PlayButtonClick();
        SetActiveScreen(4);
    }

    private void onBack()
    {   
        soundManager.PlayButtonClick();
        if(currentScreen == 1) {
            SetActiveScreen(0);
            return;
        }
        SetActiveScreen(prevScreen);
    }

    private void SetActiveScreen(int screenIndex)
    {
        this.prevScreen = currentScreen;
        this.currentScreen = screenIndex;
        for(int i = 0; i < screens.Length; i++)
        {
            if (i == currentScreen)
            {
                screens[i].SetActive(true);
            }
            else
            {
                screens[i].SetActive(false);
            }
        }
    }

   


}

    
