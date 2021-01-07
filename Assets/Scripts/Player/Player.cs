using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
 

    private Animator animator;

    private Animator pistolAnimator;

    private int HP;

    //private List<Weapon> weapons =  new List<Weapon>();

    private Companion companion;

    private Gernade[] gernades;

    private RageMeter rageMeter;

    private bool isWeaponDrawn;

    private GameObject pistol;

    private GameObject weaponCamera;

    private Vector3 rayCenter;

    private GameObject crossHair;

    private bool isEnemyInAimRange;

    private GameObject normalInfectantInRange;

    private Weapon currentWeapon;





    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag(NormalInfectantConstants.TAG)){
            other.gameObject.GetComponent<NormalInfectant>().Chase();
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag(NormalInfectantConstants.TAG)){
            other.gameObject.GetComponent<NormalInfectant>().UnChase();
        }
    }
    
    
    void Awake()
    {  
        rayCenter = new Vector3(0.5F, 0.7F, 0);
        animator = GetComponent<Animator>();
        //pistol = GameObject.Find(PlayerConstants.EQUIPPED);
        //pistolAnimator = pistol.GetComponent<Animator>();
        crossHair = GameObject.Find(PlayerConstants.CROSS_HAIR);
        isWeaponDrawn = false;
        isEnemyInAimRange = false;
        //this.weapons = new List<Weapon>();
    }

    void FixedUpdate(){
        HandleRayCast();
      
    }
    
    
    void HandleRayCast() {
        if(!isWeaponDrawn) return;
        Ray ray = Camera.main.ViewportPointToRay(rayCenter);
        normalInfectantInRange = null;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, currentWeapon.GetRange())) {
            GameObject collided = hit.collider.gameObject;
             if(collided.CompareTag(NormalInfectantConstants.TAG)){
                   normalInfectantInRange = collided;
                   if(!isEnemyInAimRange) {
                        SetCrossHairGreen();
                        isEnemyInAimRange = true;
                      
                   }
             }
             else {
               
                 if(isEnemyInAimRange) {
                    SetCrossHairRed();
                    isEnemyInAimRange = false;
                  
                 }
             }
            // Debugging purposes only
            // Debug.DrawLine(ray.origin, hit.point);
          
        }
        else {
              if(isEnemyInAimRange) {
                    SetCrossHairRed();
                    isEnemyInAimRange = false;
                 }

        }

    }
    
     public void HandleDrawWeapon(){
            this.isWeaponDrawn = true;
            animator.SetBool(WeaponsConstants.DRAW_PISTOL, isWeaponDrawn);
        }
    
    public void HandlePutDownWeapon() {
        if(Input.GetButtonDown(PlayerConstants.PUT_DOWN_WEAPON_INPUT)){
           this.isWeaponDrawn = false;
           animator.SetBool(WeaponsConstants.DRAW_PISTOL, false);
        }
    }
  

    private void HandleFire(){
        if(Input.GetButtonDown("Fire1") && isWeaponDrawn){
            //animator.SetTrigger(WeaponsConstants.SHOOT);
            //pistolAnimator.SetTrigger(WeaponsConstants.FIRE);
            currentWeapon.Shoot();
            if(normalInfectantInRange){
                normalInfectantInRange.GetComponent<NormalInfectant>().GetShot(currentWeapon.GetDmg());
            }
        }
    }
    
    void SetCrossHairGreen(){
        SpriteRenderer sprite = crossHair.GetComponent<SpriteRenderer>();
        sprite.color = new Color (0, 255, 0, 1); 
    }

    void SetCrossHairRed() {
         SpriteRenderer sprite = crossHair.GetComponent<SpriteRenderer>();
         sprite.color = new Color (255, 0, 0, 1); 
    }


    // Update is called once per frame
    void Update()
    {   

        HandlePutDownWeapon();
        HandleFire();
        
    }


    public void SetWeapon(Weapon weapon) {
        this.currentWeapon = weapon;
    }

    public bool GetIsweaponDrawn() {
        return isWeaponDrawn;
    }
  
}
