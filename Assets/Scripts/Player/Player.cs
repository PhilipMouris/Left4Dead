using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
 
    
    private Animator animator;

    private Animator pistolAnimator;

    private int HP;

    private Weapon[] weapons;

    private Companion companion;


    private RageMeter rageMeter;

    private bool isWeaponDrawn;

    private GameObject pistol;

    private GameObject weaponCamera;

    private Vector3 rayCenter;

    private GameObject crossHair;

    private bool isEnemyInAimRange;

    private GameObject normalInfectantInRange;
    private List<Gernade> gernades = new List<Gernade>();

    private Gernade currentGernade;

    private bool thrown= false;

    private float throwingPower = 3f;



    void OnTriggerStay(Collider other){
        
        if(other.gameObject.CompareTag(NormalInfectantConstants.TAG)){
            Debug.Log("CHASE IN");
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
        animator = GetComponentInChildren<Animator>();
        pistol = GameObject.Find(PlayerConstants.EQUIPPED);
        pistolAnimator = pistol.GetComponent<Animator>();
        crossHair = GameObject.Find(PlayerConstants.CROSS_HAIR);
        isWeaponDrawn = false;
        isEnemyInAimRange = false;
    }

    void FixedUpdate(){
        HandleRayCast();
      
    }
    
    
    void HandleRayCast() {
        if(!isWeaponDrawn) return;
        Ray ray = Camera.main.ViewportPointToRay(rayCenter);
        normalInfectantInRange = null;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, PlayerConstants.PISTOL_RANGE)) {
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
      public void CollectGernade(Gernade gernade){
        Debug.Log("Added Gernade");
        ResetGrenadeInfo();
        gernades.Add(gernade); //Need to Check for Type of Gernade and If max Limit is Exceeded
        Debug.Log(gernades.Count);
    }
    Gernade DeactivateGrenadeProps(Gernade grenade){
        Animator animator = grenade.gameObject.GetComponentInChildren<Animator>();
        // if(animator){
        //     Destroy(animator);
        // }
        Light[] lights = grenade.gameObject.GetComponentsInChildren<Light>();
        for(int i = 0;i<lights.Length;i++){
            Debug.Log(lights[i].type + " Type") ;
            if(lights[i].type.Equals("Spot")){
                Destroy(lights[i].gameObject);
            }
        }
        return grenade;
    }
    public void ThrowGrenade()
    {
        Debug.Log(gernades.Count);
        if(gernades.Count>0){
            Debug.Log("Throwing");
            currentGernade = gernades[0];
            currentGernade.gameObject.SetActive(true);
            currentGernade.gameObject.transform.position = transform.position - new Vector3(0,0,1.5f);
            currentGernade.gameObject.transform.rotation = transform.rotation;
            Rigidbody grenadeRigidbody =currentGernade.gameObject.AddComponent<Rigidbody>();
            grenadeRigidbody.useGravity=true;
            currentGernade = DeactivateGrenadeProps(currentGernade);
            grenadeRigidbody.AddForce((transform.forward+transform.up) * throwingPower, ForceMode.Impulse);
            gernades.RemoveAt(0);
        }else{
            Debug.Log("No Gernade Available");
        }
        
    }
    


     void HandleDrawWeapon(){
        if(Input.GetButtonDown(PlayerConstants.DRAW_WEAPON_INPUT)){
            Debug.Log("Draw Weapon");
            this.isWeaponDrawn = !isWeaponDrawn;
            animator.SetBool(PlayerConstants.DRAW_PISTOL, isWeaponDrawn);
        }
        
    }    

    void HandleFire(){
        if(Input.GetButtonDown("Fire1") && isWeaponDrawn){
            animator.SetTrigger(PlayerConstants.SHOOT);
            pistolAnimator.SetTrigger(PlayerConstants.FIRE);
            if(normalInfectantInRange){
                normalInfectantInRange.GetComponent<NormalInfectant>().GetShot(1000);
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

    void HandleGrenade(){
        if(Input.GetMouseButton(1)){
            if(throwingPower<PlayerConstants.THROWING_POWER_MAX){
                throwingPower += 0.2f;
            }
        }
        if(Input.GetMouseButtonUp(1)){
            Debug.Log("Throwing Power");
            Debug.Log(throwingPower);
            ThrowGrenade(); 
            ResetGrenadeInfo();
        }
    }
    void ResetGrenadeInfo(){
        throwingPower = 3f;
        thrown=false;
    }
    // Update is called once per frame
    void Update()
    {   

        HandleDrawWeapon();
        HandleFire();
        HandleGrenade();
        
    }


    public void ResetHealth(){
        
    }
}
