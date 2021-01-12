using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageMeter : MonoBehaviour
{   

    private AnimatedBar rageBar;

    private float resetBarTimer = 3f;

    void Awake() {
        rageBar = null;
    }

    public void ChangeRage(int amount) {
        rageBar.Change(amount);

    }

    public void resetBar() {
        ChangeRage(-100);
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
