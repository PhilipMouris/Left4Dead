using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    private AmmoPackManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<AmmoPackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void Initialize() {
        Utils.AddBoxCollider(gameObject);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            manager.UpdateLocations(gameObject);
            GameObject.Find("Player").GetComponent<Player>().ResetAmmo();
            Debug.Log("Ammo Collected");
            // player.GetComponent<Player>().ResetAmmo();
        }

    }

}
