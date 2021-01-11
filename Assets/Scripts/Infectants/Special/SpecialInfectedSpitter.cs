using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfectedSpitter : MonoBehaviour
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
        if (!isAttacking && !isChasing && !isIdle)
            AlternatePosition();
        if (PlayerInRange() && !isChasing && !isAttacking && !isIdle)
            StartChasing();
        if (PlayerAtStoppingDistance() && isChasing && !isAttacking && !isIdle)
            Attack();
        if (isChasing)
            Chase();
        if (isAttacking)
            RotateToPlayer();
        // for testing purposes
        if (Input.GetKeyDown("n"))
            GetShot(50);
    }

    public void UnAttack()
    {
        isChasing = false;
        isAttacking = false;
        isIdle = true;
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Run", false);
        Invoke("ContinueChasing", attackInterval);
    }

    public void ContinueChasing()
    {
        agent.ResetPath();
        agent.destination = player.transform.position;
        agent.stoppingDistance = 5;
        isIdle = false;
        animator.SetBool("Idle", false);
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
        Debug.Log("Attack");
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

    public bool PlayerAttacked()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= 5;
    }

    public bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= 15;
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

    public void GetShot(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            animator.SetBool("Dead", true);
            manager.UpdateDeadMembers(gameObject);
            agent.isStopped = true;
            isDead = true;
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
