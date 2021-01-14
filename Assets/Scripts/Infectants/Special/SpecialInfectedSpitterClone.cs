using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfectedSpitterClone : SpecialInfectedGeneral
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
    private bool isDead = false;
    public GameObject spit;

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
        if (Input.GetKeyDown("m"))
            GetShot(20);
        if (isDead)
            return;
        if (!isChasing && !isAttacking)
            AlternatePosition();
        if (PlayerInRange() && !isChasing && !isAttacking)
            StartChasing();
        if (PlayerAtStoppingDistance() && isChasing)
            Attack();
        if (isAttacking)
            RotateToPlayer();
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

    public void AlternatePosition()
    {
        if (transform.position.z <= walkingLowerBound)
            agent.destination = new Vector3(transform.position.x, transform.position.y, walkingUpperBound);
        if (transform.position.z >= walkingUpperBound)
            agent.destination = new Vector3(transform.position.x, transform.position.y, walkingLowerBound);
    }

    public void StartChasing()
    {
        isChasing = true;
        animator.SetTrigger("Run");
        agent.destination = player.transform.position;
        agent.stoppingDistance = 10;
    }

    public void ContinueChasing()
    {
        if (!PlayerAttacked())
        {
            isAttacking = false;
            isChasing = true;
            animator.SetTrigger("Run");
            agent.ResetPath();
            agent.destination = player.transform.position;
            agent.stoppingDistance = 10;
        }
        else
        {
            Attack();
        }
    }

    public void Attack()
    {
        isAttacking = true;
        agent.Stop();
        isChasing = false;
        animator.SetTrigger("Attack");
        Invoke("ContinueChasing", 7);
        Invoke("Spit", 2.0f);
    }

    public bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= 20;
    }

    public bool PlayerAtStoppingDistance()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    public bool PlayerAttacked()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= 10;
    }

    public void RotateToPlayer()
    {
        Vector3 lookAt = player.transform.position - transform.position;
        lookAt.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), Time.deltaTime);
    }

    public void Spit()
    {
        GameObject spitBall = Instantiate(spit, transform.GetChild(2).transform.position, Quaternion.identity);
        spitBall.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
    }

    public override void GetShot(int damage)
    {
        if (isDead)
            return;
        HP = HP - damage;
        if (HP <= 0)
        {
            CancelInvoke();
            animator.SetTrigger("Dead");
            agent.isStopped = true;
            isDead = true;
            manager.UpdateDeadMembers(gameObject);
            manager.Die();
            manager.RemoveEnemy(type,companionID);
            manager.UpdateDeadMembers(gameObject);
        }
        else
        {
            animator.SetTrigger("GetShot");
        }
    }
}
