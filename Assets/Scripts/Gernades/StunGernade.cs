using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGernade : Gernade
{
    public GameObject fire;
    private float Delay = 3f;
    private float GrenadeRadius = 15f;
    private int DamageRate = 0;
    private float SecondDelay = 1f;
    private bool isExploded = false;
    private GameObject createdFire;
    private bool inside = false;

    private Player player;

    private AudioSource explosionSource;
    // private string type = "stun";


    void Awake() {
        this.player = GameObject.Find("Player").GetComponent<Player>();
        this.maxCapacity = WeaponsConstants.STUN_MAX;
        type = "stun";
    }

  
    void Update()
    {
        // Debug.Log(inside);
        if (inside == true)
        {
            // Debug.Log("INSIDEEe2");
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject copy = Instantiate(gameObject);
                copy.SetActive(false);
                bool collected= this.hudManager.CollectGernade(copy.GetComponent<Gernade>());
                if(collected)
                    manager.UpdateLocations(gameObject);
            }
        }
        if (isExploded == true & SecondDelay > 0f)
        {
            SecondDelay -= Time.deltaTime;

        }
        else if (isExploded == true & SecondDelay <= 0f)
        {
            infectantManager.StunAll();
            SecondDelay = 1f;
            Delay--;
        }
        if (Delay <= 0)
        {
            Destroy(createdFire);
            Destroy(gameObject);
            UnStunAll();
            Debug.Log("Destroyed");
        }

    }
     void OnTriggerEnter(Collider collidedPlayer)
    {
        
        if (collidedPlayer.gameObject.CompareTag("Player"))
        {
            // Debug.Log("INSIDEE");
            inside=true;
            //player = collidedPlayer.GetComponent<Player>();
        }
    }
    void OnTriggerExit(Collider collidedPlayer)
    {
        
        if (collidedPlayer.gameObject.CompareTag("Player"))
        {
            // Debug.Log("INSIDEE");
            inside=false;
            //player = collidedPlayer.GetComponent<Player>();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !isExploded)
        {
            Explode(other.gameObject);
        }
    }
    public void UnBurnAll(){
        infectantManager.UnBurnAll();
    }
     public void UnStunAll(){
        infectantManager.UnStunAll();
    }
    
    public void Explode(GameObject other)
    {
        Debug.Log("INSIDEExplode");
        isExploded = true;

        GameObject explosion = Instantiate(particleEffect, transform.position, transform.rotation);
        explosionSource = GetAudioSource();
        explosionSource.outputAudioMixerGroup = GetAudioMixerGroup();
        explosionSource.PlayOneShot(explosionSound);
        createdFire = Instantiate(fire, transform.position, transform.rotation);
        infectantManager.StunAll();

        float totalExplosionDelay = explosion.GetComponent<ParticleSystem>().main.duration + explosion.GetComponent<ParticleSystem>().startLifetime;
        Destroy(explosion, totalExplosionDelay);
        Debug.Log(isExploded + " INSIDE");
    }
}
