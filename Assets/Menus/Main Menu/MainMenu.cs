using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool isCompanionSelected;

    //public GameObject button;

    void Start() {
        GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayMenu();
    }
    


    public void QuitGame() {
        Application.Quit();
    }

   

}
