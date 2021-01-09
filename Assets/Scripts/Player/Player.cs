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

    //private GameObject pistol;

    private GameObject weaponCamera;

    private Vector3 rayCenter;

    //private GameObject crossHair;
    IDictionary<string, GameObject> crossHairs;

    private bool isEnemyInAimRange;

    private GameObject normalInfectantInRange;

    private Weapon currentWeapon;

    private GameObject crossHair;

    //private Camera weaponCamera;





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
        rayCenter = new Vector3(0.51F, 0.52F, 0);
        animator = GetComponent<Animator>();
        crossHairs = new Dictionary<string, GameObject>();
        InitializeCrossHairs();
        weaponCamera = GameObject.Find(PlayerConstants.WEAPON_CAMERA);
        isWeaponDrawn = false;
        isEnemyInAimRange = false;
        //this.weapons = new List<Weapon>();
    }

    void FixedUpdate(){
        HandleRayCast();
      
    }

    void InitializeCrossHairs() {
        foreach(KeyValuePair<string, string> kvp in WeaponsConstants.WEAPON_TYPES) {
            //Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
            GameObject currentCrossHair = GameObject.Find($"{kvp.Value}CrossHair");
            crossHairs.Add(kvp.Value, currentCrossHair);
            if(kvp.Key == "PISTOL"){
                this.crossHair = currentCrossHair;
            }
            else {
                if(currentCrossHair != null) currentCrossHair.SetActive(false);
            }
        }

    }
    
    
    void HandleRayCast() {
        if(!isWeaponDrawn) return;
        Ray ray = Camera.main.ViewportPointToRay(rayCenter);
        normalInfectantInRange = null;
        RaycastHit hit;
        // if (Physics.Raycast(ray, out hit, currentWeapon.GetRange())) {
        if (Physics.Raycast(ray, out hit, 100)) {
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
                  
                 } else SetCrossHairGreen();
             }
            // Debugging purposes only
            Debug.DrawLine(ray.origin, hit.point);
          
        }
        else {
              if(isEnemyInAimRange) {
                    SetCrossHairRed();
                    isEnemyInAimRange = false;
                 }
             else   SetCrossHairGreen();

        }

    }
    
     public void HandleDrawWeapon(){
            this.isWeaponDrawn = true;
            //animator.SetBool(WeaponsConstants.DRAW_PISTOL, isWeaponDrawn);
            animator.SetTrigger($"draw{currentWeapon.GetType()}");
        }
    
    public void HandlePutDownWeapon() {
        if(Input.GetButtonDown(PlayerConstants.PUT_DOWN_WEAPON_INPUT)){
           this.isWeaponDrawn = false;
           animator.SetTrigger(PlayerConstants.SWITCH);
        }
    }
  

    private void HandleFire(){
        if(Input.GetButtonDown("Fire1") && isWeaponDrawn){
            //animator.SetTrigger(WeaponsConstants.SHOOT);
            //pistolAnimator.SetTrigger(WeaponsConstants.FIRE);
            currentWeapon.Shoot();
            animator.SetTrigger(WeaponsConstants.FIRE);
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


    // Update is called once per frame
    void Update()
    {   

        HandlePutDownWeapon();
        HandleFire();
        
    }


    public void SetWeapon(Weapon weapon) {
        animator.SetTrigger(PlayerConstants.SWITCH);
        if(currentWeapon && isWeaponDrawn){
            currentWeapon.Hide();
        }
        // GameObject test =  crossHairs[weapon.GetType()];
        if(currentWeapon) crossHairs[currentWeapon.GetType()].SetActive(false);
        var(position,rotation) = weapon.GetCameraData();
        weaponCamera.transform.localPosition = position;
        weaponCamera.transform.localRotation = Quaternion.Euler(rotation);
        this.currentWeapon = weapon;
        currentWeapon.UnHide();
        if(isWeaponDrawn){
             HandleDrawWeapon();
             crossHairs[weapon.GetType()].SetActive(true);
             crossHair = crossHairs[weapon.GetType()];
        }
    
    }

    public bool GetIsweaponDrawn() {
        return isWeaponDrawn;
    }
  
}
