using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NormalInfectant : MonoBehaviour
{
    // Start is called before the first frame update



    private NavMeshAgent agent;
    private Transform[] locations;
    private Animator animator;
    private GameObject mainPlayer;
    private bool walking = false;
    int random_timer ;
    private int HP;
    private int dps;
    
    private bool dead = false;
    private float speed;
    private bool chasing = false;

    private bool attacking = false;
    private NormalInfectantsManager manager;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<NormalInfectantsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( !chasing)
        {
            UpdateDestination();
        }else{
            CheckAttack();
        }
    }
    public void CheckAttack(){
        agent.destination = mainPlayer.transform.position;
        if(agent.remainingDistance<=agent.stoppingDistance){
            Attack();
        }else{
            UnAttack();
            Chase();
        }
    }
    public void GetShot(int damage){
        Debug.Log("SHOTTTTT");
        HP = HP - damage;
        if(HP<=0){
            Debug.Log("KKK???");
            animator.SetBool("Dead",true);
            dead=true;
            manager.UpdateDeadMembers(gameObject);
            agent.isStopped=true;
        }else{
            animator.SetTrigger("GetShot");
        }
    }
    private void CheckWalking()
    {
        if (random_timer==0)
        {
            walking = true;
            animator.SetBool("Walking",true);
            
            agent.isStopped=false;
        }else{
            random_timer--;
        }
    }
    private Transform GetRandomLocation()
    {
        int index = Random.Range(0, locations.Length - 1);
        return locations[index];
    }
    private void UpdateDestination()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.destination = GetRandomLocation().position;
        }
    }
    public void initialize(int HP, int dps, Transform[] locations,GameObject player)
    {
        this.HP = HP;
        this.dps = dps;
        this.locations = locations;
        this.mainPlayer = player;
    }
    public void UnChase(){
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("Chase",false);
        agent.destination = GetRandomLocation().position;
        agent.speed = 0.1f;
        chasing=false;
    }
    public void Chase()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("Chase",true);
        agent.destination = mainPlayer.transform.position;
        agent.speed = 0.5f;
        chasing=true;
    }
    public void Attack()
    {
        attacking=true;
        animator.SetBool("Attack",true);
    }
    public void UnAttack()
    {
        attacking=false;
        animator.SetBool("Attack",false);
    }


}
