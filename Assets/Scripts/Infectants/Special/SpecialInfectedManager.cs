using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInfectedManager : MonoBehaviour
{
    public GameObject tankModel;
    public GameObject locations;
    public GameObject player;
    private Transform[] locationsList;
    private GameObject[] infectedMembers;
    private List<GameObject> deadMembers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        infectedMembers = new GameObject[12];
        locationsList = locations.GetComponentsInChildren<Transform>();
        Spawn();
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

    public void Spawn()
    {
        for (int i = 0; i < infectedMembers.Length/4; i++)
        {
            GameObject instantiated = Instantiate(tankModel, locationsList[i].position, Quaternion.identity);
            SpecialInfected infected = instantiated.AddComponent<SpecialInfected>();
            infected.initialize(1000, 30, 1, locationsList, player);
        }
    }
}
