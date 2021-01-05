using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float moveSpeed = 2.5f;
    private Camera camera;

    private Animator animator;

    private int HP;

    private Weapon[] weapons;

    private Companion companion;

    private Gernade[] gernades;

    private RageMeter rageMeter;

    void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("NormalInfected")){
            other.gameObject.GetComponent<NormalInfectant>().Chase();
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("NormalInfected")){
            other.gameObject.GetComponent<NormalInfectant>().UnChase();
        }
    }
    
    
    void Awake()
    {
        //  this.camera = GameObject.Find(PlayerConstants.MAIN_CAMERA);
         // animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        CheckShoot();
    }
    void CheckShoot(){
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit)){
            NormalInfectant hit_member =  hit.collider.gameObject.GetComponent<NormalInfectant>();
            if(hit_member)
                if(Input.GetMouseButtonDown(0))
                    hit_member.GetShot(36); //Dummy Placholder of Damage- Should be replaced with Damage from Current Weapon
        }
    }
    
    
  

    // Update is called once per frame
    void Update()
    {
        // HandleMovement();
    }


    private void HandleMovement()
    {
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        //  this.transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime);
    }
    public void ResetHealth(){
        
    }
}
