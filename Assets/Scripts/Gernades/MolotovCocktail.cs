using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovCocktail : Gernade
{
    public GameObject fire;
    private float Delay = 5f;
    private float GrenadeRadius = 15f;
    private float ExplosionForce = 400f;
    private int DamageRate = 25;
    private float SecondDelay = 1f;
    private bool isExploded = false;
    private GameObject createdFire;
    private bool inside = false;

    private AudioSource explosionSource;
    // private string type = "molotov";

    
    private Player player;

    void Awake() {
         this.player = GameObject.Find("Player").GetComponent<Player>();
         this.maxCapacity = WeaponsConstants.MOLOTOV_MAX;
         type = "molotov";
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
                player.ResetState();
               GameObject copy = Instantiate(gameObject);
                copy.SetActive(false);
                bool collected= this.hudManager.CollectGernade(copy.GetComponent<Gernade>());
                if(collected)
                    Invoke("UpdateLocations",1);
                 
            }
        }
        if (isExploded == true & SecondDelay > 0f)
        {
            SecondDelay -= Time.deltaTime;

        }
        else if (isExploded == true & SecondDelay <= 0f)
        {
            incurDamage();
            SecondDelay = 1f;
            Delay--;
        }
        if (Delay <= 0)
        {
            Destroy(createdFire);
            Destroy(gameObject);
            UnBurnAll();
            Debug.Log("Destroyed");
        }

    }
    void OnTriggerEnter(Collider collidedPlayer)
    {
        
        if (collidedPlayer.gameObject.CompareTag("Player"))
        {
            // Debug.Log("INSIDEE");
            inside=true;
            
        }
    }
    void OnTriggerExit(Collider collidedPlayer)
    {
        
        if (collidedPlayer.gameObject.CompareTag("Player"))
        {
            // Debug.Log("INSIDEE");
            inside=false;
            
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
    void incurDamage()
    {
        Collider[] touchedObjects = Physics.OverlapSphere(transform.position, GrenadeRadius);

        foreach (Collider touchedObject in touchedObjects)
        {
            if (touchedObject.CompareTag("NormalInfected"))
            {

                var target = touchedObject.gameObject.GetComponent<NormalInfectant>();

                target.GetBurned(DamageRate);

            }
            if (SpecialInfectantConstants.TAGS.Contains(touchedObject.tag))
            {
                var target = touchedObject.gameObject.GetComponent<SpecialInfectedGeneral>();

                target.GetShot(DamageRate);
            }
        }
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
            if (SpecialInfectantConstants.TAGS.Contains(touchedObject.tag))
            {
                var target = touchedObject.gameObject.GetComponent<SpecialInfectedGeneral>();

                target.GetShot(DamageRate);
            }

        }

        float totalExplosionDelay = explosion.GetComponent<ParticleSystem>().main.duration + explosion.GetComponent<ParticleSystem>().startLifetime;
        Destroy(explosion, totalExplosionDelay);
        Debug.Log(isExploded + " INSIDE");
    }
}
