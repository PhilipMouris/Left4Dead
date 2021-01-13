using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfectedCharger : MonoBehaviour
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
        HP = 600;
        dps = 75;
        attackInterval = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
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
        CheckCamera();
        // for testing purposes
        if (Input.GetKeyDown("m"))
            GetShot(50);
    }

    public void CheckCamera()
    {   
        if(GameManager.crafting_bool) return;
        if (player.gameObject.transform.GetChild(1).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Fall"))
        {
            SwitchToThirdPerson();
        }
        else
            SwitchToFirstPerson();
    }

    public void UnAttack()
    {
        isChasing = false;
        isAttacking = false;
        isIdle = true;
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Run", false);
        if (PlayerAttacked())
        {
            DecreaseHealth();
            player.gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Fall");
            player.gameObject.transform.GetChild(1).GetComponent<Player>().SetIsWeaponDrawn(false);
        }
        Invoke("ContinueChasing", attackInterval);
    }

    public void ContinueChasing()
    {
        agent.enabled = true;
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
        isChasing = false;
        isAttacking = true;
        animator.SetBool("Attack", true);
        animator.SetBool("Run", false);
        agent.Stop();
        agent.enabled = false;
        Invoke("UnAttack", 2);
    }

    public void DecreaseHealth()
    {
        gameManager.SetHealth(gameManager.GetHealth() - dps);
    }

    public bool PlayerAtStoppingDistance()
    {
        if (!agent.enabled)
            return false;
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
            agent.isStopped = true;
            isDead = true;
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

    public void SwitchToThirdPerson()
    {
        GameObject.Find("ThirdPersonCamera").GetComponent<Camera>().enabled = true;
        GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().enabled = false;
        GameObject.Find("WeaponCamera").GetComponent<Camera>().enabled = false;
    }

    public void SwitchToFirstPerson()
    {
        GameObject.Find("ThirdPersonCamera").GetComponent<Camera>().enabled = false;
        GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().enabled = true;
        GameObject.Find("WeaponCamera").GetComponent<Camera>().enabled = true;
    }
}
