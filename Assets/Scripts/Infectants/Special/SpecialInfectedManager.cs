using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInfectedManager : MonoBehaviour
{
    private List<GameObject> deadMembers = new List<GameObject>();
    private int count;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {   
        gameManager = GameObject.FindObjectOfType<GameManager>();
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

     
    public void Die() {
        gameManager.EnemyDead("special");
    }

    public void UpdateDeadMembers(GameObject deadInfected)
    {
        deadMembers.Add(deadInfected);
    }



    public int AddToCompanion(SpecialInfectedGeneral special, int id, string type){
        return gameManager.AddSpecialToCompanion(special, id,type);
    }

    public void RemoveNormalInfectant(int id) {
        gameManager.RemoveNormalFromCompanion(id);
    }

    public void RemoveEnemy(string type, int id) {
        gameManager.RemoveSpecialFromCompanion(type,id);
    }



    public int GetDeadInfected()
    {
        return deadMembers.Count;
    }
    public int GetRemainingSpecialInfected(){
        
       
        return count-GetDeadInfected();

    }
}
