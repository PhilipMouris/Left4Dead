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
    private Transform[] horde_locations_list;
    private GameObject[] infected_members;
    private GameObject[] horde_infected_members;
    private List<GameObject> dead_members = new List<GameObject>();
    private GameManager gameManager;
    private GameObject hordeLocations;
    private bool insideDanger = false;

    // Start is called before the first frame update
    void Start()
    {   
        gameManager = GameObject.FindObjectOfType<GameManager>();
        locations_list = locations.GetComponentsInChildren<Transform>();
        // hordeLocations = GameObject.Find("HordeLocations");
        infected_members = new GameObject[locations_list.Length-1];
       
        //Spawn();
        
    }

    public void Die() {
        gameManager.EnemyDead("normal");
    }

    public int AddNormalInfectantToCompanion(NormalInfectant normal){
        return gameManager.AddEnemyToCompanion(normal, normal.companionID);
    }

    public void RemoveNormalInfectant(int id) {
        gameManager.RemoveNormalFromCompanion(id);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            AttractHorde();
            insideDanger=true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            UnAttractHorde();
            insideDanger=false;
        }
    }
    public void SetHordeLocations(GameObject locations){
        hordeLocations = locations;
    }
    public bool isInsideDanger(){
        return insideDanger;
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
    public int GetRemainingNormalInfected(){
        return infected_members.Length+horde_infected_members.Length-dead_members.Count;
    }
    public void UnBurnAll()
    {
        for (int i = 0; i < infected_members.Length; i++)
        {
            infected_members[i].GetComponent<Animator>().SetBool("Burn", false);
        }
         for (int i = 0; i < horde_infected_members.Length; i++)
        {
            horde_infected_members[i].GetComponent<Animator>().SetBool("Burn", false);
        }
    }
    public void UnStunAll()
    {
        for (int i = 0; i < infected_members.Length; i++)
        {
            infected_members[i].GetComponent<Animator>().SetBool("Stun", false);
        }
        for (int i = 0; i < horde_infected_members.Length; i++)
        {
            horde_infected_members[i].GetComponent<Animator>().SetBool("Stun", false);
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
        for (int i = 0; i < horde_infected_members.Length; i++)
        {
            Animator currentAnimator = horde_infected_members[i].GetComponent<Animator>();
            bool attacking = currentAnimator.GetBool("Attack");
            bool chasing = currentAnimator.GetBool("Chase");
            if(attacking || chasing)
                currentAnimator.SetBool("Stun", true);
        }
    }
    public void AttractHorde(){
        for (int i = 0; i < horde_infected_members.Length; i++)
        {
            NavMeshAgent agent = horde_infected_members[i].GetComponent<NavMeshAgent>();
            if (!horde_infected_members[i].GetComponent<NormalInfectant>().isDead())
            {
                Debug.Log("ATTRACT");
                // horde_infected_members[i].GetComponent<NormalInfectant>().StartChasing();
                 horde_infected_members[i].GetComponent<NormalInfectant>().SetHordeMemberChasing(true);
        }
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
         for (int i = 0; i < horde_infected_members.Length; i++)
        {
            NavMeshAgent agent = horde_infected_members[i].GetComponent<NavMeshAgent>();
            if (!horde_infected_members[i].GetComponent<NormalInfectant>().isDead())
            {
                Debug.Log("ATTRACT");
                horde_infected_members[i].GetComponent<NormalInfectant>().GetAttracted(grenadeLocation);
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
         for (int i = 0; i < horde_infected_members.Length; i++)
        {
            NavMeshAgent agent = horde_infected_members[i].GetComponent<NavMeshAgent>();
            if (!horde_infected_members[i].GetComponent<NormalInfectant>().isDead())
            {
                Debug.Log("ATTRACT");
                horde_infected_members[i].GetComponent<NormalInfectant>().GetUnAttracted();
            }
        }
    }
      public void UnAttractHorde()
    {
        for (int i = 0; i < horde_infected_members.Length; i++)
        {
            NavMeshAgent agent = horde_infected_members[i].GetComponent<NavMeshAgent>();
            if (!horde_infected_members[i].GetComponent<NormalInfectant>().isDead())
            {
                
                horde_infected_members[i].GetComponent<NormalInfectant>().SetHordeMemberChasing(false);
              
            }
        }
    }
    public void Spawn()
    {
        for (int i = 0; i < infected_members.Length; i++)
        {
            GameObject instantiated = Instantiate(model, locations_list[i].position, Quaternion.identity);
            NormalInfectant infectant = instantiated.GetComponent<NormalInfectant>();
            infectant.initialize(50, 5, locations_list, player,false);
            infected_members[i] = instantiated;


        }
    }
    public void SpawnHorde()
    {
        horde_locations_list = hordeLocations.GetComponentsInChildren<Transform>();
        horde_infected_members = new GameObject[horde_locations_list.Length-1];
        for (int i = 0; i < horde_infected_members.Length; i++)
        {
            GameObject instantiated = Instantiate(model, horde_locations_list[i].position, Quaternion.identity);
            NormalInfectant infectant = instantiated.GetComponent<NormalInfectant>();
            infectant.initialize(50, 5, horde_locations_list, player,true);
            horde_infected_members[i] = instantiated;
        }
    }
}
