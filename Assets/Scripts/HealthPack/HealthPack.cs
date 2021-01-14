using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private HealthPackManager manager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {   gameManager = FindObjectOfType<GameManager>();
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
            gameManager.SetHealth(50);
            // player.GetComponent<Player>().ResetHealth();
        }

    }

}
