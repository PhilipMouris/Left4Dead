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

    private bool stoppedChasing;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<NormalInfectantsManager>();
        stoppedChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!stoppedChasing) {

        if (!isAttracted)
        {
            if (PlayerInRange() && !chasing && !attacking)
                StartChasing();
            
             if(chasing && isDead()){
                UnChase();
                GameObject.Find("GameManager").GetComponent<GameManager>().SetChasing(chasing);
                stoppedChasing = true;
            }    
            if (chasing)
                GameObject.Find("GameManager").GetComponent<GameManager>().SetChasing(chasing);
                //stoppedChasing = false;
                Chase();
            if (PlayerAtStoppingDistance() && chasing && !attacking)
                Attack();
            if (PlayerOutsideStoppingDistance() && !chasing && attacking)
                UnAttack();
            if (PlayerOutOfRange())
            {
                UnChase();
                UnAttack();
                UpdateDestination();
                //GameObject.Find("GameManager").GetComponent<GameManager>().SetChasing(chasing);
            }
        }
        }

        // if (!chasing &!attacking & !isAttracted)
        // {
        //     UpdateDestination();
        // }
        // else if (!isAttracted)
        // {
        //     CheckAttack();
        // }
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
    public void RotateToPlayer()
    {
        Debug.Log("Rotating");
        Vector3 lookAt = mainPlayer.transform.position - gameObject.transform.position;
        // lookAt.y = 0;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookAt), Time.deltaTime);
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
        isAttracted = true;

    }
    public void Attack()
    {
        if (!isAttracted)
        {
            attacking = true;
            chasing = false;
            animator.SetBool("Attack", true);
            RotateToPlayer();
        }
    }
    public void UnAttack()
    {
        attacking = false;
        animator.SetBool("Attack", false);
    }
    public bool PlayerAtStoppingDistance()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    public bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, mainPlayer.transform.position) <= 10;
    }
    public bool PlayerOutOfRange()
    {
        return Vector3.Distance(transform.position, mainPlayer.transform.position) > 10;
    }

    public void Chase2()
    {
        agent.destination = mainPlayer.transform.position;
        agent.stoppingDistance = 5;
    }
    public bool PlayerOutsideStoppingDistance()
    {
        return Vector3.Distance(transform.position, mainPlayer.transform.position) > 5;
    }
    public void StartChasing()
    {
        chasing = true;
        animator.SetBool("Chase", true);
        animator.SetBool("Attack", false);
    }


}
