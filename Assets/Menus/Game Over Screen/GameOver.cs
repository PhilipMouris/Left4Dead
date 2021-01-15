using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    private MainGameManager levelsManager;
    void Awake(){
        levelsManager = GameObject.FindObjectOfType<MainGameManager>();
    }
    public void RestartLevel() {
        levelsManager.RestartCurrentLevel();
        //SceneManager.LoadScene("OutdoorsLevel");
    }

    public void QuitToMain() {
        levelsManager.RestartGame();
    }
    
}
