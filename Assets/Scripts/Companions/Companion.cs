using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{   

    private string type;
    private Animator animator;
    private string[] actions = {"walk","run","fire","idle"};
    bool isMoving;
    bool isRunning;
    bool isShooting;
    // Start is called before the first frame update
    void Awake() {
        animator = GetComponent<Animator>();
    }

    private void SetAction(string action) {
        foreach (string actionType in actions)
        {
            if(action == actionType) animator.SetBool(action, true);
            else animator.SetBool(actionType, false);
        }
    }
    public void Run() {
        SetAction(actions[1]);
    }

    public void Walk() {
        SetAction(actions[0]);
    }

    public void Fire() {
        SetAction(actions[2]);
    }

    public void Idle() {
        SetAction(actions[3]);
    }


    private void HandleAnimation() {
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)
                        || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        isRunning = Input.GetKey(KeyCode.LeftShift);
        
        if(isShooting){
            Fire();
            return;
        }
        if(isRunning){
            Run();
            return;
        }
        if(isMoving) {
            Walk();
            return;
        }

        Idle();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
        
    }


    public void SetIsShooting(bool isShooting) {
        this.isShooting = isShooting;
    }
}
