using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private HealthPackManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<HealthPackManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            manager.UpdateLocations(gameObject);
            // player.GetComponent<Player>().ResetHealth();
        }

    }

}
