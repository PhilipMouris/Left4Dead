using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NormalInfectantsManager : MonoBehaviour
{
    public GameObject model;
    public GameObject locations;
    public GameObject player;
    private Transform[] locations_list;
    private GameObject[] infected_members;
    private List<GameObject> dead_members = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        infected_members = new GameObject[20];
        locations_list = locations.GetComponentsInChildren<Transform>();

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
    public void UpdateDeadMembers(GameObject deadInfected)
    {
        dead_members.Add(deadInfected);
    }
    public int GetDeadInfected()
    {
        return dead_members.Count;
    }
    public void UnBurnAll()
    {
        for (int i = 0; i < infected_members.Length; i++)
        {
            infected_members[i].GetComponent<Animator>().SetBool("Burn", false);
        }
    }
    public void UnStunAll()
    {
        for (int i = 0; i < infected_members.Length; i++)
        {
            infected_members[i].GetComponent<Animator>().SetBool("Stun", false);
        }
    }
    public void StunAll()
    {
        for (int i = 0; i < infected_members.Length; i++)
        {
            Animator currentAnimator = infected_members[i].GetComponent<Animator>();
            bool attacking = currentAnimator.GetBool("Attack");
            bool chasing = currentAnimator.GetBool("Chase");
            if(attacking || chasing)
                currentAnimator.SetBool("Stun", true);
        }
    }
    public void AttractAll(Transform grenadeLocation)
    {
        for (int i = 0; i < infected_members.Length; i++)
        {
            NavMeshAgent agent = infected_members[i].GetComponent<NavMeshAgent>();
            if (!infected_members[i].GetComponent<NormalInfectant>().isDead())
            {
                Debug.Log("ATTRACT");
                infected_members[i].GetComponent<NormalInfectant>().GetAttracted(grenadeLocation);
            }
        }
    }
    public void UnAttractAll()
    {
        for (int i = 0; i < infected_members.Length; i++)
        {
            NavMeshAgent agent = infected_members[i].GetComponent<NavMeshAgent>();
            if (!infected_members[i].GetComponent<NormalInfectant>().isDead())
            {
                
                infected_members[i].GetComponent<NormalInfectant>().GetUnAttracted();
            }
        }
    }
    public void Spawn()
    {
        for (int i = 0; i < infected_members.Length; i++)
        {
            GameObject instantiated = Instantiate(model, locations_list[i].position, Quaternion.identity);
            NormalInfectant infectant = instantiated.GetComponent<NormalInfectant>();
            infectant.initialize(50, 10, locations_list, player);
            infected_members[i] = instantiated;


        }
    }
}
