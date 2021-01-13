using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{   

    private string type;
    private Animator animator;
    private string[] actions = {"walk","run","fire"};
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
        SetAction(actions[1]);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
