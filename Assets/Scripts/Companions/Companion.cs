using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;
using UnityStandardAssets.Characters.FirstPerson;
using System.Linq;

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
    private Weapon weapon;
    private int enemyID = 1;
    private IDictionary<int,NormalInfectant> normalInfectants = new Dictionary<int,NormalInfectant>();
    private IDictionary<int,SpecialInfected> tanks = new Dictionary<int,SpecialInfected>();
    private IDictionary<int,SpecialInfectedCharger> chargers = new Dictionary<int, SpecialInfectedCharger>();
    private string enemyType;
    private GameObject nextShotEnemy;

    




    public int AddEnemy(NormalInfectant normal,int id) {
        if(normalInfectants.ContainsKey(id)) return id;
        if(id == null || id ==0 ){
            this.enemyID +=1;
        }
        int usedId = id==null || id==0? enemyID :id;
        normalInfectants.Add(usedId,normal);
        return usedId;
    }

    
    public int AddEnemy(SpecialInfected tank,int id) {
         if(tanks.ContainsKey(id)) return id;
         if(id == null || id ==0 ){
            this.enemyID +=1;
        }
        int usedId = id==null || id==0? enemyID :id;
         tanks.Add(usedId,tank);
        return usedId;
    }

     public int AddEnemy(SpecialInfectedCharger charged,int id) {
          if(chargers.ContainsKey(id)) return id;
        if(id == null || id ==0 ){
            this.enemyID +=1;
        }
        int usedId = id==null || id==0?  enemyID :id;
        chargers.Add(usedId, charged);
        return usedId;
    }




    public void RemoveEnemy(string type,int id) {
        //Debug.Log(id + "REMOVEDDDD");
        switch(type){
            case "normal": normalInfectants.Remove(id);break;
            case "tank": tanks.Remove(id);break;
            case "charger": chargers.Remove(id);break;
        }
    }




    public void Initialize(Weapon weapon,string type) {
        this.weapon = weapon;
        this.type = type;

    }


    public void PrepareShoot() {
        nextShotEnemy = null;
        enemyType ="";
        //Debug.Log(normalInfectants.Count + " COUNT");
        if(chargers.Count > 0){
            nextShotEnemy =  chargers.First().Value.gameObject;
        }
        else {
            if(tanks.Count>0) {
            nextShotEnemy =  chargers.First().Value.gameObject;
        }
        else {
            if(normalInfectants.Count>0){
            enemyType ="normal";
            nextShotEnemy = normalInfectants.First().Value.gameObject;
            }
        }
        }
        RotateToEnemy(nextShotEnemy);
        
    }

   


    public void RotateToEnemy(GameObject enemy)
    {
        if(!enemy) return;
        Vector3 lookAt = enemy.transform.position - gameObject.transform.position;
        //Debug.Log(lookAt.sqrMagnitude);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookAt), 5*Time.deltaTime);
    }


    

    
    public Vector3 GetDirection(Vector3 from, Vector3 to) {
         return (to - from);
    }
    private void FixedUpdate() {
            if (m_CharacterController.isGrounded)
            {       
                
                Vector3 desiredMove = transform.forward;
                RaycastHit hitInfo;
                Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

                //Vector3 dir = GetDirection(transform.position,endPoint);
                //m_MoveDir.z = transform.forward.z >= 0 ? 3f : -3f;
                
                 m_MoveDir.x = desiredMove.z * 10;
                 //m_MoveDir.x = transform.forward.z * 3;
                if (m_Jump)
                {   
                    
                    m_CharacterController.enabled = true;
                    agent.Stop();
                    m_MoveDir.y = m_JumpSpeed*1.4f;
                    m_Jump = false;
                    m_Jumping = true;
                }
                if(m_Jumping) {
                    //transform.LookAt(endPoint);
                    m_MoveDir.z = transform.forward.z * 10;

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




     
    private void HandleShoot() {
        if(Input.GetKey(KeyCode.Q)){
            if(type!=CompanionConstants.rochelle) isShooting = true;
            weapon.SetIsShootingCompanion(true);
            weapon.ShootCompanion(enemyType,nextShotEnemy);
        }
           if (Input.GetKeyUp(KeyCode.Q))
            {   isShooting = false;
                weapon.SetIsShootingCompanion(false);
            }
    }

    void Update()
    {   


        agent.updateRotation = nextShotEnemy== null;
        if(!m_Jumping && !m_Jump){
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
      
       
          if(!m_Jumping && !m_Jump) {
            PrepareShoot();
            HandleShoot();
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
        {   if(actionType != "jump"){
            if(action == actionType) animator.SetBool(action, true);
            else animator.SetBool(actionType, false);
        }
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


    public void SetExtraClip() {
        weapon.SetExtraClip();
    }


    private void HandleAnimation() {
       
        //HandleJump();

        bool wasRunning = isRunning;
        bool wasWalking = isWalking;
        isWalking = wasWalking || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D); 
        isRunning = wasRunning || Input.GetKey(KeyCode.LeftShift) && isWalking;

        if(isShooting){
            Fire();
            return;
        }
        if(isZeroVelocity()){
            Idle();
            isRunning = false;
            isMoving = false;
            return;
        }
        agent.speed = isWalking ? 5f:10f;
     
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
