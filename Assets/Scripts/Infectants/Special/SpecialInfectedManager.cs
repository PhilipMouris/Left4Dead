using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInfectedManager : MonoBehaviour
{
    private List<GameObject> deadMembers = new List<GameObject>();
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        int special1 = GameObject.FindObjectsOfType<SpecialInfected>().Length;
        int special2 = GameObject.FindObjectsOfType<SpecialInfectedCharger>().Length;
        int special3 = GameObject.FindObjectsOfType<SpecialInfectedSpitter>().Length;
        int special4 = GameObject.FindObjectsOfType<SpecialInfectedSpitterClone>().Length;
        count = special1+special2+special3+special4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDeadMembers(GameObject deadInfected)
    {
        deadMembers.Add(deadInfected);
    }

    public int GetDeadInfected()
    {
        return deadMembers.Count;
    }
    public int GetRemainingSpecialInfected(){
        
       
        return count-GetDeadInfected();

    }
}
