using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompanionScreen : MonoBehaviour
{

    public GameObject companionError;
    private bool isCompanionSelected;
    // Start is called before the first frame update
    

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
        companionError.SetActive(true); // Enable the text so it shows
        yield return new WaitForSecondsRealtime(1);
        companionError.SetActive(false); // Disable the text so it is hidden
    }


    public void CompanionToManager(string companionName) {
       // GameObject.Find("GameManager").GetComponent<GameManager>().SetCompanion(companionName);
        GameManager.SetCompanion(companionName);
        isCompanionSelected = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
