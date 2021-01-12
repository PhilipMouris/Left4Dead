using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageMeter : MonoBehaviour
{   

    private AnimatedBar rageBar;

    private GameManager gameManager;

    private float resetBarTimer = 3f;

    void Awake() {
        rageBar = null;
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void ChangeRage(int amount) {
        if(gameManager.GetIsRaged()) return;
        rageBar.Change(amount);
        ResetTimer();

    }

    public void resetBar() {
        rageBar.Change(-100);
        ResetTimer();
        gameManager.SetRage(false);
    }

    public bool ActivateRage() {
        if(rageBar.GetPercentage() >= 100) {
            gameManager.SetRage(true);
            resetBarTimer = 7f;
            return true;
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ResetTimer() {
        resetBarTimer = 3f;
    }

    public void SetRageBar(GameObject bar) {
        rageBar = bar.AddComponent<AnimatedBar>();
        rageBar.Initialize(2f,1f,0);

    }
    // Update is called once per frame
    void Update()
    {   
        if(resetBarTimer<0){
            resetBar();
            ResetTimer();
            return;
        }
        resetBarTimer -= Time .deltaTime;

    }
}
