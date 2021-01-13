using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private string currentLevel;
    private GameObject currentLevelObject;
    private bool gameFinished =false;
    

    void Awake(){
      Scene scene = SceneManager.GetActiveScene();
      currentLevel = scene.name;
    }

   
    void Start()
    {
        currentLevelObject = GameObject.Find("CurrentLevelManager");
        if(currentLevelObject){
            AddLevelManager();
        }
    }
    LevelManager GetCorresspondingLevelObject(){
         return currentLevelObject.GetComponent<LevelManager>();
    }
    void AddLevelManager(){
        switch(currentLevel){
            case EngineConstants.LEVEL1_NAME: currentLevelObject.AddComponent<Level1Manager>();break;
            case EngineConstants.LEVEL2_NAME: currentLevelObject.AddComponent<Level2Manager>();break;
            case EngineConstants.LEVEL3_NAME: currentLevelObject.AddComponent<Level3Manager>();break;
        }
    }
    void SwitchToNextLevel(){
       switch(currentLevel){
            case EngineConstants.LEVEL1_NAME: LoadScene(EngineConstants.LEVEL2_NAME);break;
            case EngineConstants.LEVEL2_NAME: LoadScene(EngineConstants.LEVEL3_NAME);break;
        }
    }
    void CheckSwitchLevel(){
        if(currentLevelObject.GetComponent<LevelManager>().isLevelFinsihed()){
            if(currentLevel.Equals(EngineConstants.LEVEL3_NAME)){
                gameFinished=true;
            }
            else{
                
                Invoke("SwitchToNextLevel",5);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckSwitchLevel();
    }
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void StartGame(){
        if(!currentLevel.Equals(EngineConstants.LEVEL1_NAME)){
            LoadScene(EngineConstants.LEVEL1_NAME);
        }
    }
}
