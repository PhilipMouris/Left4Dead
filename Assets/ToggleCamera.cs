using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    public GameObject FPS;
    public Camera CraftScreen;
 
    void Start() {
        FPS.SetActive(false);
        CraftScreen.enabled = true;
 }
 
    void Update() {
 
        if (Input.GetKeyDown(KeyCode.T)) {
            //FPS.enabled = !FPS.enabled;
            Debug.Log(CraftScreen.enabled);
            CraftScreen.enabled = !CraftScreen.enabled;
     }
 }
}
