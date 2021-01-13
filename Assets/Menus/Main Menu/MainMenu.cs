using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool isCompanionSelected;

    public GameObject button;

    void Start() {
        GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayMenu();
    }
    public void StartGame() {
        if (isCompanionSelected) {
            Debug.Log("Start Game");
            SceneManager.LoadScene("OutdoorsLevel");
        }

        else {
            StartCoroutine("ShowError");
        }
    }

    private IEnumerator ShowError() {
        //yield WaitForSeconds(5);
        button.SetActive(true); // Enable the text so it shows
        yield return new WaitForSecondsRealtime(1);
        button.SetActive(false); // Disable the text so it is hidden
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void CompanionToManager(string companionName) {
       // GameObject.Find("GameManager").GetComponent<GameManager>().SetCompanion(companionName);
        GameManager.SetCompanion(companionName);
        isCompanionSelected = true;
    }

}
