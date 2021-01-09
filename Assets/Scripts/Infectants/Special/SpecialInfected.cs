using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfected : MonoBehaviour
{
    private SpecialInfectedManager manager;
    private int HP;
    private int dps;
    private int attackInterval;
    private float walkingLowerBound;
    private float walkingUpperBound;
    private Animator animator;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<SpecialInfectedManager>();
        walkingLowerBound = transform.position.z;
        walkingUpperBound = transform.position.z + 10;
        animator = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
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
        AlternatePosition();
        // for testing purposes
        if (Input.GetKeyDown("m"))
            GetShot(500);
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



    /**public void initialize(int HP, int dps, int attackInterval, Transform[] locations, GameObject player)
    {
        this.HP = HP;
        this.dps = dps;
        this.attackInterval = attackInterval;
        this.locations = locations;
        this.mainPlayer = player;
    }**/
}
