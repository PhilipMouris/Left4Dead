using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBomb : Gernade
{
   
    public AudioClip beeping;
    private float Delay = 4f;
    private float GrenadeRadius = 15f;
    private int DamageRate = 100;
    private float SecondDelay = 1f;
    private bool isCollided = false;
    private bool isExploded = false;
    private float ExplosionForce = 400f;
    private Animator beepingAnimator;
    private bool inside = false;
    private Player player;
    private AudioSource explosionSource;
    // private string type = "pipe";

    void Awake(){
        beepingAnimator = GetComponentInChildren<Animator>();
        beepingAnimator.enabled=false;
        this.player = GameObject.Find("Player").GetComponent<Player>();
         this.maxCapacity = WeaponsConstants.PIPE_MAX;
         type = "pipe";
    }
    void UpdateLocations(){
        manager.UpdateLocations(gameObject);
    }

    void Update()

    {
        // Debug.Log(inside);
        if (inside == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.GetComponent<Animator>().SetTrigger("pickupGernade");
                 GameObject copy = Instantiate(gameObject);
                player.ResetState();
                copy.SetActive(false);
                bool collected= this.hudManager.CollectGernade(copy.GetComponent<Gernade>());
                if(collected)
                    Invoke("UpdateLocations",1);
            }
        }
        if (isCollided == true & SecondDelay > 0f && !isExploded)
        {
            SecondDelay -= Time.deltaTime;

        }
        else if (isCollided== true & SecondDelay <= 0f && !isExploded)
        {
            
            GetAudioSource().PlayOneShot(beeping);
            GetAudioSource().loop=false;
            SecondDelay = 1f;
            Delay--;
        }
        if (Delay <= 0 & !isExploded)
        {
            Explode();
            UnAttractAll();
        }

    }
     void OnTriggerEnter(Collider collidedPlayer)
    {
        
        if (collidedPlayer.gameObject.CompareTag("Player"))
        {
            // Debug.Log("INSIDEE");
            inside=true;
           // player = collidedPlayer.GetComponent<Player>();
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
        if (other.gameObject.CompareTag("Ground") && !isCollided)
        {
            // Debug.Log("COLLIDED");
            isCollided=true;
            beepingAnimator.enabled=true;
            AttractAll();
        }
    }
    void AttractAll(){
        infectantManager.AttractAll(gameObject.GetComponent<Rigidbody>().transform);
    }
    void UnAttractAll(){
        infectantManager.UnAttractAll();
    }
    public void UnBurnAll(){
        infectantManager.UnBurnAll();
    }
     public void UnStunAll(){
        infectantManager.UnStunAll();
    }
    
   public void Explode()
    {
        Debug.Log("INSIDEExplode");
        isExploded = true;
        
        explosionSource = GetAudioSource();
        explosionSource.outputAudioMixerGroup = GetAudioMixerGroup();
        explosionSource.PlayOneShot(explosionSound);
        GameObject explosion = Instantiate(particleEffect, transform.position, transform.rotation);

        
        Collider[] touchedObjects = Physics.OverlapSphere(transform.position, GrenadeRadius);

        foreach (Collider touchedObject in touchedObjects)
        {
            if (touchedObject.CompareTag("NormalInfected"))
            {   
                Rigidbody rigidbody = touchedObject.gameObject.AddComponent<Rigidbody>();
                rigidbody.mass = 5;
                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(ExplosionForce, transform.position, GrenadeRadius);
                }

                var target = touchedObject.gameObject.GetComponent<NormalInfectant>();

                target.GetBurned(DamageRate);
                Destroy(rigidbody,2.1f);

            }

        }

        float totalExplosionDelay = explosion.GetComponent<ParticleSystem>().main.duration + explosion.GetComponent<ParticleSystem>().startLifetime;
      
        Destroy(explosion, totalExplosionDelay);
    
        Destroy(gameObject,totalExplosionDelay);
        
      
        Debug.Log("Destroyed");
        Debug.Log(isCollided + " INSIDE");
    }
}
