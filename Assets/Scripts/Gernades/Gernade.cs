using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gernade : MonoBehaviour
{
    
    public GameObject particleEffect;
     protected GernadeManager manager;
     protected bool inside = false;
     protected Player player;
    
    // Start is called before the first frame update
    
    void Start()
    {
        manager = FindObjectOfType<GernadeManager>();
        
    }
    void OnTriggerEnter(Collider collidedPlayer)
    {
        
        if (collidedPlayer.gameObject.CompareTag("Player"))
        {
            // Debug.Log("INSIDEE");
            inside=true;
            player = collidedPlayer.GetComponent<Player>();
        }
    }

    // Update is called once per frame

}
