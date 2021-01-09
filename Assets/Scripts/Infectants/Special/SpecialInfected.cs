using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialInfected : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform[] locations;
    private Animator animator;
    private GameObject mainPlayer;
    private SpecialInfectedManager manager;
    private int HP;
    private int dps;
    private int attackInterval;
    private bool isWalking = true;
    private float walkingLowerBound;
    private float walkingUpperBound;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<SpecialInfectedManager>();
        walkingLowerBound = transform.position.z;
        walkingUpperBound = transform.position.z + 3;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isWalking && (transform.position.z == walkingUpperBound || transform.position.z == walkingLowerBound))
          //  Turn();
        //else
        if (isWalking)
           Walk();
    }

    public void Walk()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time, walkingUpperBound - walkingLowerBound) + walkingLowerBound);
    }

    public void Turn()
    {
        animator.SetBool("Turn", true);
    }

    public void initialize(int HP, int dps, int attackInterval, Transform[] locations, GameObject player)
    {
        this.HP = HP;
        this.dps = dps;
        this.attackInterval = attackInterval;
        this.locations = locations;
        this.mainPlayer = player;
    }
}
