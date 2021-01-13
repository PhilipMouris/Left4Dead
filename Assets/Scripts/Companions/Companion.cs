using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;
using UnityStandardAssets.Characters.FirstPerson;

public class Companion : MonoBehaviour
{   

    private string type;
    private Animator animator;
    private string[] actions = {"walk","run","fire","idle","jump"};
    private bool isMoving;
    private bool isRunning;
    private bool isShooting;
    private bool isWalking;
    private string currentState = "idle";
    private bool  MoveAcrossNavMeshesStarted = false;
    private CharacterController m_CharacterController;
    private float m_StickToGroundForce=10f;
    private float m_GravityMultiplier = 2f;
    private CollisionFlags m_CollisionFlags;
    private FirstPersonController fps;
    private Vector3 m_MoveDir = Vector3.zero;
    private float m_JumpSpeed = 10f;
    private bool m_Jump = false;
    private bool m_Jumping = false;
    private bool m_PreviouslyGrounded;
    float speed = 10f;
    private Vector3 endPoint;

    private void FixedUpdate() {
            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.z = transform.forward.z >= 0 ? 3f : -3f;
                if (m_Jump)
                {   
                    
                    m_CharacterController.enabled = true;
                    agent.Stop();
                    m_MoveDir.y = m_JumpSpeed;
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
             else
            {  

                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            if(m_CharacterController.enabled) {
             m_CollisionFlags =  m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);
            }
        
    }

        void Update()
    {   
        if(!m_Jumping){
            agent.destination = player.gameObject.transform.position;
            HandleOffLink();
        }   
        if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
                {
                  
                    agent.CompleteOffMeshLink();
                    m_MoveDir.y = 0f;
                    m_Jumping = false;
                    m_CharacterController.enabled = false;
                    animator.SetBool("jump", false);
                    agent.Resume();
                }
                if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
                {
                    m_MoveDir.y = 0f;
                }

                m_PreviouslyGrounded = m_CharacterController.isGrounded;
        if(!m_Jumping || !m_Jump) {
            HandleAnimation();
        }
        
    }


    public void HandleOffLink(){

        if(!agent.isOnOffMeshLink) return;
             UnityEngine.AI.OffMeshLinkData data = agent.currentOffMeshLinkData;
            endPoint = data.endPos + Vector3.up * agent.baseOffset;
            transform.LookAt(endPoint);
            SetJump();
        
    }


    private void SetJump() {
           if (!m_Jump)
                {   
                    m_Jump = true;
                    m_CharacterController.enabled = true;
                    animator.SetBool("jump", true);
                    }
                }
    


    UnityEngine.AI.NavMeshAgent agent;
    Player player;
    // Start is called before the first frame update
    void Awake() {
         m_CharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindObjectOfType<Player>();
        m_CharacterController.enabled = false;
        agent.stoppingDistance = 7;
        fps = GameObject.FindObjectOfType<FirstPersonController>();
       
    }

    private void SetAction(string action) {
        foreach (string actionType in actions)
        {
            if(action == actionType) animator.SetBool(action, true);
            else animator.SetBool(actionType, false);
        }
    }
    public void Run() {
        SetAction(actions[1]);
        currentState = "run";
    }

    public void Walk() {
        SetAction(actions[0]);
        currentState = "walk";
    }

    public void Fire() {
        SetAction(actions[2]);
        currentState = "fire";
    }

    public void Idle() {
         {
            SetAction(actions[3]);
            currentState = "idle";
        }
    }

    public void Jump(bool jump) {
        //if(jump)
        animator.SetBool("jump", jump);
    }


    private void HandleAnimation() {
       
        //HandleJump();

        bool wasRunning = isRunning;
        bool wasWalking = isWalking;
        isWalking = wasWalking || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D); 
        isRunning = wasRunning || Input.GetKey(KeyCode.LeftShift) && isWalking;

    
        if(isZeroVelocity()){
            Idle();
             isRunning = false;
            isMoving = false;
            return;
        }
     
        if(isShooting){
            Fire();
            return;
        }
        if(isRunning){
            Run();
            return;
        }
        if(isWalking) {
            Walk();
            return;
        }
    }

    
    public bool isZeroVelocity() {
        return (agent.velocity.x==0) && (agent.velocity.y==0) && (agent.velocity.z==0);
    }

    void Start()
    {
        
    }



    public void SetIsShooting(bool isShooting) {
        this.isShooting = isShooting;
    }


}
