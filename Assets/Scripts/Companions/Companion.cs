using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class Companion : MonoBehaviour
{   

    private string type;
    private Animator animator;
    private string[] actions = {"walk","run","fire","idle","jump"};
    private bool isMoving;
    private bool isRunning;
    private bool isShooting;
    private string currentState = "idle";
    private bool  MoveAcrossNavMeshesStarted = false;
    private CharacterController m_CharacterController;
    private float m_StickToGroundForce=10f;
    private float m_GravityMultiplier = 2f;
    private CollisionFlags m_CollisionFlags;

    //private LerpControlledBob m_JumpBob = new LerpControlledBob();
    
    private Vector3 m_MoveDir = Vector3.zero;
    private float m_JumpSpeed = 10f;
    private bool m_Jump = false;
    private bool m_Jumping = false;
    private bool m_PreviouslyGrounded;
    float speed = 10f;

    private void FixedUpdate() {
            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    //PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
             else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

    }

        void Update()
    {   
        //Debug.Log(agent.velocity + " VELOCITYYYY");
        agent.destination = player.gameObject.transform.position;

          if (!m_Jump)
                {
                    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                }

                if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
                {
                    //StartCoroutine(m_JumpBob.DoBobCycle());
                    //PlayLandingSound();
                    m_MoveDir.y = 0f;
                    m_Jumping = false;
                }
                if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
                {
                    m_MoveDir.y = 0f;
                }

                m_PreviouslyGrounded = m_CharacterController.isGrounded;
        //HandleAnimation();
        
    }


    UnityEngine.AI.NavMeshAgent agent;
    Player player;
    // Start is called before the first frame update
    void Awake() {
         m_CharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindObjectOfType<Player>();
        //agent.destination = GameObject.Find("FPSController").transform.position;
        agent.stoppingDistance = 7;
       
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
    

        if(isZeroVelocity()){
            Idle();
            return;
        }
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)
                        || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        isRunning = Input.GetKey(KeyCode.LeftShift);
        
        if(isShooting){
            Fire();
            return;
        }
        if(isRunning){
            Run();
            return;
        }
        if(isMoving) {
            Walk();
            return;
        }
    }

    private bool isZeroVelocity() {
        return (agent.velocity.y == 0) && (agent.velocity.x == 0) && (agent.velocity.z == 0);
    }

    private bool isJumping() {
        //return Math.Abs(agent.velocity.y )> 2;
        //Math.Abs(agent.currenOfMeshLinkData.startPos.y -agent.currenOfMeshLinkData.startPos.y) > 0.8;
        return agent.isOnOffMeshLink;
    }
    void Start()
    {
        
    }

// public void HandleJump() {
//      if(agent.isOnOffMeshLink && !MoveAcrossNavMeshesStarted){
//          Debug.Log("HEREEE");
//          StartCoroutine(MoveAcrossNavMeshLink());
//          MoveAcrossNavMeshesStarted=true;
//     }
 
// }
   
// IEnumerator MoveAcrossNavMeshLink()
// {
//         UnityEngine.AI.OffMeshLinkData data = agent.currentOffMeshLinkData;
//         agent.updateRotation = false;
 
//         Vector3 startPos = agent.transform.position;
//         Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
//         float duration = (endPos-startPos).magnitude/agent.velocity.magnitude;
//         float t = 0.0f;
//         float tStep = 1.0f/duration;
//         while(t<1.0f){
//             transform.position = Vector3.Lerp(startPos,endPos,t);
//             agent.destination = transform.position;
//             t+=tStep*Time.deltaTime;
//             yield return null;
//         }
//         transform.position = endPos;
//         agent.updateRotation = true;
//         agent.CompleteOffMeshLink();
//         MoveAcrossNavMeshesStarted = false;
 
// }
    // Update is called once per frame


    public void SetIsShooting(bool isShooting) {
        this.isShooting = isShooting;
    }


         private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
}
