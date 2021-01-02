﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalInfectantsManager : MonoBehaviour
{
    public GameObject model;
    public GameObject locations;
    public GameObject player;
    private Transform[] locations_list;
    private GameObject[] infected_members;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ALO0");
        infected_members = new GameObject[20];
        locations_list  = locations.GetComponentsInChildren<Transform>();
        
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // EXAMPLE CODE TO SPAWN
    // Create Instance -> return game object
    // gameObject.AddComponent<NormalInfectant>();
    // NormalInfectant infectant = gameObject.GetComponent<NormalInfectant>();
    // infectant.Initialize(....)
    public void Spawn(){
        for(int i =0;i<infected_members.Length;i++){
            GameObject instantiated = Instantiate(model,locations_list[i].position,Quaternion.identity);
            NormalInfectant infectant = instantiated.GetComponent<NormalInfectant>();
            infectant.initialize(10,10,locations_list,player);


        }
    }
}