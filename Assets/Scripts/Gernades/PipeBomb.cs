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
   
    void Awake(){
        beepingAnimator = GetComponentInChildren<Animator>();
        beepingAnimator.enabled=false;
    }
    void Update()
    {
        // Debug.Log(inside);
        if (inside == true)
        {
            // Debug.Log("INSIDEEe2");
            if (Input.GetKeyDown(KeyCode.E))
            {
                manager.UpdateLocations(gameObject);
                player.CollectGernade(gameObject.GetComponent<Gernade>());
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
            player = collidedPlayer.GetComponent<Player>();
        }
    }
    void OnTriggerExit(Collider collidedPlayer)
    {
        
        if (collidedPlayer.gameObject.CompareTag("Player"))
        {
            // Debug.Log("INSIDEE");
            inside=false;
            player = collidedPlayer.GetComponent<Player>();
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
        
        GetAudioSource().PlayOneShot(explosionSound);
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
