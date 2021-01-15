using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class MainGameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private string currentLevel;
    private GameObject currentLevelObject;
    private bool gameFinished = false;
    private bool gameStarted = false;


    void Awake()
    {
       
        
    }


    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        currentLevel = scene.name;
        currentLevelObject = GameObject.Find("CurrentLevelManager");
        gameStarted=true;
        AddLevelManager();
    }
    LevelManager GetCorresspondingLevelObject()
    {
        return currentLevelObject.GetComponent<LevelManager>();
    }
    void AddLevelManager()
    {
        switch (currentLevel)
        {
            case EngineConstants.LEVEL1_NAME: currentLevelObject.AddComponent<Level1Manager>(); break;
            case EngineConstants.LEVEL2_NAME: currentLevelObject.AddComponent<Level2Manager>(); break;
            case EngineConstants.LEVEL3_NAME: currentLevelObject.AddComponent<Level3Manager>(); break;
        }
        Debug.Log("Added Level Manager");
    }
    void SwitchToNextLevel()
    {
        switch (currentLevel)
        {
            case EngineConstants.LEVEL1_NAME: LoadScene(EngineConstants.LEVEL2_NAME); break;
            case EngineConstants.LEVEL2_NAME: LoadScene(EngineConstants.LEVEL3_NAME); break;
            case EngineConstants.LEVEL3_NAME: LoadScene("Credits"); break;
        }
    }
    void CheckSwitchLevel()
    {
        if (gameStarted)
        {
            
            if (currentLevelObject.GetComponent<LevelManager>().isLevelFinsihed())
            {
                if (currentLevel.Equals(EngineConstants.LEVEL3_NAME))
                {
                    gameFinished = true;
                }
                else
                {

                    Invoke("SwitchToNextLevel", 3);
                }
            }
        }
    }
    public void RestartCurrentLevel(){
        switch (currentLevel)
        {
            case EngineConstants.LEVEL1_NAME: LoadScene(EngineConstants.LEVEL1_NAME); break;
            case EngineConstants.LEVEL2_NAME: LoadScene(EngineConstants.LEVEL2_NAME); break;
            case EngineConstants.LEVEL3_NAME: LoadScene(EngineConstants.LEVEL3_NAME); break;
        }
        
    }
    public void RestartGame(){
        GameObject.Find("FPSController").GetComponent<FirstPersonController>().GetMouseLook().SetCursorLock(false);
        LoadScene(EngineConstants.STARTMENU_NAME);
    }
    // Update is called once per frame
    void Update()
    {
        if(currentLevelObject)
            CheckSwitchLevel();
    }
    public void LoadScene(string sceneName)
    {
        Time.timeScale=1.0f;
        AudioListener.pause = false;
        
        SceneManager.LoadScene(sceneName);
    }
    public void StartGame()
    {
        if (!currentLevel.Equals(EngineConstants.LEVEL1_NAME))
        {
            gameStarted=true;
            LoadScene(EngineConstants.LEVEL1_NAME);
        }
    }
}
