using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    // Start is called before the first frame update

    private string displayName;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(string name) {
        Utils.AddBoxCollider(gameObject);
        SetDisplayName(name);
    }

    public void SetDisplayName(string displayName){
        this.displayName = displayName;
    }

    public string getDisplayName(string name){
        return displayName;
    }
}
