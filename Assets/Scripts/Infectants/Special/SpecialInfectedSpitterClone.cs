using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfectedSpitterClone : MonoBehaviour
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
        HP = 100;
        dps = 20;
        attackInterval = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing && !isAttacking)
            AlternatePosition();
        if (PlayerInRange() && !isChasing && !isAttacking)
            StartChasing();
        if (PlayerAtStoppingDistance() && isChasing)
            Attack();
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


}
