using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfected : SpecialInfectedGeneral
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
    private bool isStunned = false;
    private bool isDead;
   private string type = "tank";
    public int companionID = 0;

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
        if (gameObject.tag == "Tank")
        {
            HP = 1000;
            dps = 30;
            attackInterval = 1;
        }
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
        if (PlayerOutsideStoppingDistance() && !isChasing && isAttacking)
            UnAttack();
        if (isChasing)
            Chase();
        // for testing purposes
        if (Input.GetKeyDown("m"))
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
        isChasing = true;
        isAttacking = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Run", true);
        agent.ResetPath();
        agent.destination = player.transform.position;
        agent.stoppingDistance = 5;
        CancelInvoke();
    }

    public bool PlayerOutsideStoppingDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position) > 5;
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
      
    }

    public void DecreaseHealth()
    {   
        gameManager.SetHealth(-dps);
    }

    public bool PlayerAtStoppingDistance()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    public bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= 10;
    }

    public void Chase()
    {
        agent.destination = player.transform.position;
        agent.stoppingDistance = 5;
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
            agent.isStopped = true;
            manager.UpdateDeadMembers(gameObject);
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

    public override void Stun()
    {
        isStunned = true;
        isChasing = false;
        isAttacking = false;
        agent.isStopped = true;
        animator.speed = 0.01f;
    }

    public override void Unstun()
    {
        agent.isStopped = false;
        agent.ResetPath();
        agent.destination = player.transform.position;
        agent.stoppingDistance = 5;
        animator.speed = 1;
        StartChasing();
        isStunned = false;
    }

    public override bool GetIsStunned()
    {
        return isStunned;
    }
}
