using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfectedCharger : SpecialInfectedGeneral
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
    private bool isPaused = false;
    private bool isStunned = false;

    private string type = "charger";
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
        HP = 600;
        dps = 75;
        attackInterval = 5;
        //isPaused = GameObject.Find("GameManager").GetComponent<GameManager>().isGamePaused()
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
        
        if(PlayerInRange()) {
               if(companionID==0 && !isDead)
                    if((gameManager.GetIsRescued() && gameManager.GetRescueLevel())|| !gameManager.GetRescueLevel())
                         companionID = manager.AddToCompanion(upCast,companionID,type);
        }
        else {
               if(companionID!= 0){
                    manager.RemoveEnemy(type,companionID);
                    companionID = 0;
            }
        }
        
    }

    public void CheckCamera()
    {   
        if(GameManager.crafting_bool) return;
        if(GameManager.isPauseScreen) return;
        
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
        gameManager.SetHealth(-dps);
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

    public override void GetShot(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            animator.SetBool("Dead", true);
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

    public override void Stun()
    {
        isStunned = true;
        isChasing = false;
        isAttacking = false;
        isIdle = false;
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
