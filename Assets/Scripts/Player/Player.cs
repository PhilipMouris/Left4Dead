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

    private GameObject weaponCamera;

    IDictionary<string, GameObject> crossHairs;

    IDictionary<string,GameObject> muzzles;
    
    private GameObject crossHair;
    
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
            crossHairs.Add(kvp.Value, currentCrossHair);
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
    
    public void HandlePutDownWeapon() {
        if(Input.GetButtonDown(PlayerConstants.PUT_DOWN_WEAPON_INPUT)){
           this.isWeaponDrawn = false;
           animator.SetTrigger(PlayerConstants.SWITCH);
        }
    }
  



    // Update is called once per frame
    void Update()
    {   

        HandlePutDownWeapon();
        
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
        string weaponType = weapon.GetType();
        crossHair = crossHairs[weaponType];
        currentWeapon.SetCrossHair(crossHair);
        if(isWeaponDrawn){
             if(weaponType != WeaponsConstants.WEAPON_TYPES["PISTOL"]) currentWeapon.SetMuzzle(muzzles[weaponType]);
             HandleDrawWeapon();
             crossHairs[weaponType].SetActive(true);
        }
    
    }



    public bool GetIsweaponDrawn() {
        return isWeaponDrawn;
    }
  
}
