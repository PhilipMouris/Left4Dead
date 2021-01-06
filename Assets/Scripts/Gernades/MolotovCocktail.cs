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
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !isExploded)
        {
            Explode(other.gameObject);
        }
    }

    void incurDamage()
    {
        Collider[] touchedObjects = Physics.OverlapSphere(transform.position, GrenadeRadius);

        foreach (Collider touchedObject in touchedObjects)
        {
            if (touchedObject.CompareTag("NormalInfected"))
            {


                var target = touchedObject.gameObject.GetComponent<NormalInfectant>();

                target.GetShot(DamageRate);

            }
        }
    }
    public void Explode(GameObject other)
    {
        Debug.Log("INSIDEExplode");
        isExploded = true;

        GameObject explosion = Instantiate(particleEffect, transform.position, transform.rotation);

        createdFire = Instantiate(fire, transform.position, transform.rotation);
        Collider[] touchedObjects = Physics.OverlapSphere(transform.position, GrenadeRadius);

        foreach (Collider touchedObject in touchedObjects)
        {
            if (touchedObject.CompareTag("NormalInfected"))
            {
                Rigidbody rigidbody = touchedObject.gameObject.AddComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(ExplosionForce, transform.position, GrenadeRadius);
                }

                var target = touchedObject.gameObject.GetComponent<NormalInfectant>();

                target.GetShot(DamageRate);
                Destroy(rigidbody,1.5f);

            }

        }

        float totalExplosionDelay = explosion.GetComponent<ParticleSystem>().main.duration + explosion.GetComponent<ParticleSystem>().startLifetime;
        Destroy(explosion, totalExplosionDelay);
        Debug.Log(isExploded + " INSIDE");
    }
}
