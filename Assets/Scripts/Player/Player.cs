using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float moveSpeed = 2.5f;
    // private GameObject camera;

    private Animator animator;

    private Animator pistolAnimator;

    private int HP;

    private Weapon[] weapons;

    private Companion companion;

    private Gernade[] gernades;

    private RageMeter rageMeter;

    private bool isWeaponDrawn;

    private GameObject pistol;

    private GameObject weaponCamera;




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
        weaponCamera = GameObject.Find("WeaponCamera");
        animator = GetComponent<Animator>();
        pistol = GameObject.Find(PlayerConstants.EQUIPPED);
        pistolAnimator = pistol.GetComponent<Animator>();
        Debug.Log(pistolAnimator);

        Debug.Log("PISTOLLL");
        isWeaponDrawn = false;
    }

    void FixedUpdate(){
        //Debug.Log(Input.mousePosition);
        //Debug.Log("MOUSEEE");
       // Debug.Log(pistol.transform.position);
        // Debug.Log("PISTOL");

        //Ray ray = Camera.main.ScreenPointToRay(pistol.transform.position);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.7F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, 100))
            Debug.Log("Hit something!");
            Debug.DrawLine(ray.origin, hit.point);
    }
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(PlayerConstants.DRAW_WEAPON_INPUT)){
            this.isWeaponDrawn = !isWeaponDrawn;
            animator.SetBool(PlayerConstants.DRAW_PISTOL, isWeaponDrawn);
        }
        if(Input.GetButtonDown("Fire1") && isWeaponDrawn){
            animator.SetTrigger(PlayerConstants.SHOOT);
            pistolAnimator.SetTrigger("Fire");
        }
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
