using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public void RestartLevel() {
        Debug.Log("Restart Level");
        //SceneManager.LoadScene("OutdoorsLevel");
    }

    public void QuitToMain() {
        Debug.Log("Main Menu");
        //SceneManager.LoadScene("MainMenu");
    }
    
}
