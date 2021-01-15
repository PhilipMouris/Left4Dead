using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfectedSpitter : SpecialInfectedGeneral
{
    private SpecialInfectedManager manager;
    private GameManager gameManager;
    private int HP;
    private int dps;
    private int attackInterval;
    private float walkingLowerBound;
    private float walkingUpperBound;
    private Animator animator;
    private NavMeshAgent agent;
    private GameObject player;
    private bool isChasing = false;
    private bool isAttacking = false;
    private bool isIdle = false;
    private bool isDead = false;

    private int companionID = 0;

     private string type = "spitter";

    private SpecialInfectedGeneral upCast;

    void Awake() {
        upCast = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<SpecialInfectedManager>();
        gameManager = FindObjectOfType<GameManager>();
        walkingLowerBound = transform.position.z;
        walkingUpperBound = transform.position.z + 10;
        animator = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        HP = 100;
        dps = 20;
        attackInterval = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && !isChasing)
            AlternatePosition();
        if (PlayerInRange() && !isChasing && !isAttacking)
            StartChasing();
        if (PlayerAtStoppingDistance() && isChasing && !isAttacking)
            Attack();
        if (isChasing && !PlayerAttacked())
            Chase();
        if (isAttacking)
            RotateToPlayer();
        // for testing purposes
        if (Input.GetKeyDown("n"))
            GetShot(50);
         if(PlayerInRange()) {
               if(companionID==0 && !isDead)
                         companionID = manager.AddToCompanion(upCast,companionID,type);
        }
        else {
               if(companionID!= 0){
                    manager.RemoveEnemy(type,companionID);
                    companionID = 0;
            }
        }
    }

    public void UnAttack()
    {
        isChasing = false;
        isAttacking = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Run", false);
        Invoke("ContinueChasing", attackInterval);
    }

    public void ContinueChasing()
    {
        isAttacking = false;
        agent.ResetPath();
        agent.destination = player.transform.position;
        agent.stoppingDistance = 10;
        StartChasing();
    }

    public void StartChasing()
    {
        isChasing = true;
        animator.SetBool("Run", true);
        animator.SetBool("Attack", false);
    }

    public void Attack()
    {
        isChasing = false;
        isAttacking = true;
        animator.SetBool("Attack", true);
        animator.SetBool("Run", false);
        agent.Stop();
        Invoke("UnAttack", 2);
    }

    public void DecreaseHealth()
    {
        gameManager.SetHealth(gameManager.GetHealth() - dps);
    }

    public bool PlayerAtStoppingDistance()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    public bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= 15;
    }

    public bool PlayerAttacked()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= 10;
    }

    public void Chase()
    {
        agent.destination = player.transform.position;
        agent.stoppingDistance = 10;
    }

    public void AlternatePosition()
    {
        if (transform.position.z <= walkingLowerBound)
            agent.destination = new Vector3(transform.position.x, transform.position.y, walkingUpperBound);
        if (transform.position.z >= walkingUpperBound)
            agent.destination = new Vector3(transform.position.x, transform.position.y, walkingLowerBound);
    }

    public override void GetShot(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            animator.SetBool("Dead", true);
            manager.UpdateDeadMembers(gameObject);
            agent.isStopped = true;
            isDead = true;
            manager.Die();
            manager.RemoveEnemy(type,companionID);
            manager.UpdateDeadMembers(gameObject);
        }
        else
        {
            animator.SetTrigger("GetShot");
        }
    }

    public void RotateToPlayer()
    {
        Vector3 lookAt = player.transform.position - transform.position;
        lookAt.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), Time.deltaTime);
    }
}
