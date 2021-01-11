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
    int random_timer;
    private int HP;
    private int dps;

    private bool dead = false;
    private float speed;
    private bool chasing = false;

    private bool attacking = false;
    private bool isAttracted;
    private NormalInfectantsManager manager;
    private Transform previousDestination;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<NormalInfectantsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!chasing & !isAttracted)
        {
            UpdateDestination();
        }
        else if (!isAttracted)
        {
            CheckAttack();
        }
    }
    public Transform GetPreviousDestination()
    {
        return previousDestination;
    }
    public bool isDead()
    {
        return dead;
    }
    public void CheckAttack()
    {
        if (!isAttracted)
        {
            previousDestination = mainPlayer.transform;
            agent.destination = mainPlayer.transform.position;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Attack();
            }
            else
            {
                UnAttack();
                Chase();
            }
        }
    }
    public void GetStunned()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("Stun", true);
        agent.isStopped = true;
    }

    public void GetShot(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            animator.SetBool("Dead", true);
            dead = true;
            manager.UpdateDeadMembers(gameObject);
            agent.isStopped = true;
        }
        else
        {
            animator.SetTrigger("GetShot");
        }
    }
    public void GetAttracted(Transform grenadeLocation)
    {
        Debug.Log("Grenade");
        Debug.Log(grenadeLocation.position);
        Debug.Log("Player");
        Debug.Log(mainPlayer.transform.position);
        isAttracted = true;
        UnAttack();
        // UnChase();
        ChaseGrenade(grenadeLocation);
    }
    public void GetUnAttracted()
    {
        isAttracted = false;
        UnChase();
    }
    public void GetBurned(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            animator.SetBool("Dead", true);
            dead = true;
            manager.UpdateDeadMembers(gameObject);
            agent.isStopped = true;
        }
        else
        {
            animator.SetBool("Burn", true);
        }
    }
    private void CheckWalking()
    {
        if (random_timer == 0)
        {
            walking = true;
            animator.SetBool("Walking", true);

            agent.isStopped = false;
        }
        else
        {
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
            previousDestination = GetRandomLocation();
            agent.destination = previousDestination.position;
        }
    }
    public void initialize(int HP, int dps, Transform[] locations, GameObject player)
    {
        this.HP = HP;
        this.dps = dps;
        this.locations = locations;
        this.mainPlayer = player;
    }
    public void UnChase()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("Chase", false);
        previousDestination = GetRandomLocation();
        agent.destination = previousDestination.position;
        agent.speed = 0.1f;
        chasing = false;
    }
    public void Chase()
    {
        if (!isAttracted)
        {
           
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            animator.SetBool("Chase", true);
            previousDestination = mainPlayer.transform;
            agent.destination = mainPlayer.transform.position;
            agent.speed = 0.5f;
            chasing = true;
        }
    }
    public void ChaseGrenade(Transform grenadeDestination)
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("Chase", true);
        previousDestination = grenadeDestination;
        agent.destination = grenadeDestination.position;
        Debug.Log("Agent Destination");
        Debug.Log(agent.destination);
        agent.speed = 0.5f;
        chasing = true;

    }
    public void Attack()
    {
        if (!isAttracted)
        {
            attacking = true;
            animator.SetBool("Attack", true);
        }
    }
    public void UnAttack()
    {
        attacking = false;
        animator.SetBool("Attack", false);
    }


}
