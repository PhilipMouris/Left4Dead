using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfected : MonoBehaviour
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
        InvokeRepeating("DecreaseHealth", attackInterval, attackInterval);
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

    public void GetShot(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            animator.SetBool("Dead", true);
            manager.UpdateDeadMembers(gameObject);
            agent.isStopped = true;
        }
        else
        {
            animator.SetTrigger("GetShot");
        }
    }
}
