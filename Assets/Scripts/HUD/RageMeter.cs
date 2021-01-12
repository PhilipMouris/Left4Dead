using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageMeter : MonoBehaviour
{   

    private AnimatedBar rageBar;

    private float resetBarTimer = 3f;

    void Awake() {
        rageBar = null;
    }

    public void ChangeRage(int amount) {
        rageBar.Change(amount);
        ResetTimer();

    }

    public void resetBar() {
        ChangeRage(-100);
        ResetTimer();
    }

    public void ActivateRageBar() {
        resetBarTimer = 7f;
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
