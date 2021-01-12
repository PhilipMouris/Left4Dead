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

 

    private RageMeter rageMeter;

    private bool isWeaponDrawn;

    private GameObject weaponCamera;

    IDictionary<string, GameObject> crossHairs;

    IDictionary<string,GameObject> muzzles;
    
    private GameObject crossHair;
    
    private Weapon currentWeapon;

    private bool reloading = false;

    private float reloadTime = 0.4f;


    private Gernade currentGernade;

    private bool thrown = false;


    



    void OnTriggerStay(Collider other){
        
        if(other.gameObject.CompareTag(NormalInfectantConstants.TAG)){
            // Debug.Log("CHASE IN");
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
        animator = GetComponent<Animator>();
        crossHairs = new Dictionary<string, GameObject>();
        muzzles = new Dictionary<string,GameObject>();
        InitializeCrossHairsAndMuzzles();
        weaponCamera = GameObject.Find(PlayerConstants.WEAPON_CAMERA);
        isWeaponDrawn = false;
    }

    void Start() {

    }

    

    void InitializeCrossHairsAndMuzzles() {
        foreach(KeyValuePair<string, string> kvp in WeaponsConstants.WEAPON_TYPES) {
            GameObject currentCrossHair = GameObject.Find($"{kvp.Value}CrossHair");
            GameObject currentMuzzle =  GameObject.Find($"{kvp.Value}Muzzle");
            this.crossHairs.Add(kvp.Value, currentCrossHair);
            if(kvp.Key == "PISTOL"){
                this.crossHair = currentCrossHair;
            }
            else {
                 if(currentMuzzle != null ){
                    muzzles.Add(kvp.Value, currentMuzzle);
                    currentMuzzle.SetActive(false);
                 }
                if(currentCrossHair != null) currentCrossHair.SetActive(false);
            }
        }
        
    }
    
    
    
     public void HandleDrawWeapon(){
            this.isWeaponDrawn = true;
            //animator.SetBool(WeaponsConstants.DRAW_PISTOL, isWeaponDrawn);
            currentWeapon.SetIsDrawn(isWeaponDrawn);
            animator.SetTrigger($"draw{currentWeapon.GetType()}");
        }
    

    private void PutDown() {
        this.isWeaponDrawn = false;
        currentWeapon.SetIsDrawn(false);
    }
    public void HandlePutDownWeapon() {
        if(Input.GetButtonDown(PlayerConstants.PUT_DOWN_WEAPON_INPUT)){
           PutDown();
           animator.SetTrigger(PlayerConstants.SWITCH);
        }
    }



    private void HandleReload() {
        if(Input.GetButtonDown(PlayerConstants.RELOAD_INPUT) && isWeaponDrawn) {
            bool canReload = currentWeapon.Reload();
            if(!canReload) return;
            animator.SetTrigger(PlayerConstants.RELOAD_INPUT);
            PutDown();
            reloading = true;
        }
    }


    private void HandleReloadTime() {
        if(!reloading) return;
        if(reloadTime < 0) {
            HandleDrawWeapon();
        }
        reloadTime -= Time.deltaTime;
        if(reloadTime <=0) {
            HandleDrawWeapon();
            reloading = false;
            reloadTime = 0.4f;
        }
    }
  



    // Update is called once per frame
    


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
        string weaponType = weapon.GetType();
        crossHair = crossHairs[weaponType];
        currentWeapon.SetCrossHair(crossHair);
        if(isWeaponDrawn){
             if(weaponType != WeaponsConstants.WEAPON_TYPES["PISTOL"]) currentWeapon.SetMuzzle(muzzles[weaponType]);
             HandleDrawWeapon();
             crossHairs[weaponType].SetActive(true);
        }
    
    }
    public void SetGrenade(Gernade grenade){
        this.currentGernade = grenade;
        //SHould Add here logic of animating the grenade on player if needed
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
    public void ThrowGrenade(float throwingPower)
    {
            currentGernade.gameObject.SetActive(true);
            currentGernade.gameObject.transform.position = transform.position - new Vector3(0,-1,1.5f);
            currentGernade.gameObject.transform.rotation = transform.rotation;
            Rigidbody grenadeRigidbody =currentGernade.gameObject.AddComponent<Rigidbody>();
            grenadeRigidbody.useGravity=true;
            currentGernade = DeactivateGrenadeProps(currentGernade);
            grenadeRigidbody.AddForce((transform.forward+transform.up) * throwingPower, ForceMode.Impulse);
        
    }
    // void HandleGrenade(){
    //     if(Input.GetMouseButton(1)){
    //         if(throwingPower<PlayerConstants.THROWING_POWER_MAX){
    //             throwingPower += 0.2f;
    //         }
    //     }
    //     if(Input.GetMouseButtonUp(1)){
    //         // Debug.Log(gernades.Count + " COUNT?????");
    //         ThrowGrenade(); 
    //         ResetGrenadeInfo();
    //     }
    // }


    void Update()
    {   
        // Debug.Log(this.gernades.Count);
        HandleReload();
        HandlePutDownWeapon();
        HandleReloadTime();
        // HandleGrenade();
        
    }
   





    public bool GetIsweaponDrawn() {
        return isWeaponDrawn;
    }

    // public void CraftGrenade(Gernade grenade){
    //     gernades.Add(grenade);
    // }

    public void SetIsWeaponDrawn(bool isWeaponDrawn)
    {
        this.isWeaponDrawn = isWeaponDrawn;
    }
  
}