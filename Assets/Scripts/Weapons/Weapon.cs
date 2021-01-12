using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    private int dmg;
    private int clipCapacity;
    private int rateOfFire;
    private int maxAmmo;
    private int  currentAmmo;
    private GameObject weapon;
    private Animator animator;
    private int totalAmmo;
    private string type;
    private Vector3 aim;
    private SpriteRenderer crossHair;
    private int range;
    private GameObject muzzle;
    private bool isDrawn;
    private (Vector3,Vector3) cameraData;
    private GameObject collided;
    private Vector3 hitPoint;
    private GameObject bulletHoles;
    private GameObject bulletHole;
    private GameObject normalInfectantInRange;
    private double nextFire = -1f;
    private double hidMuzzleAfter = 0.15f;
    private bool hideMuzzle = false;
    private Color red = new Color (255, 0, 0, 1);
    private Color green =  new Color (0, 255, 0, 1);
    private Color grey = new Color(0.5f,0.5f,0.5f,1);
    private AudioSource outOfAmmo;
    private AudioClip clip;
    private AudioClip reload;
    private AudioClip dryFire;

    private Vector3 cameraPosition;


    void Awake() {
        bulletHoles = GameObject.Find(PlayerConstants.BULLET_HOLES);
        bulletHole = Resources.Load(PlayerConstants.BULLET_HOLE_PATH) as GameObject;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
    
    if(nextFire > 0)
     {  
        nextFire -= Time.deltaTime;
        hideMuzzle = true;
        //audio.Pause();
    } else
        {
            HandleFire();
        }

        if (Input.GetButtonUp("Fire1")){
              if(type != WeaponsConstants.WEAPON_TYPES["PISTOL"]){
                 hideMuzzle = true;
              }
              //audio.Pause();
        }

        HideMuzzle();
        if(currentAmmo <=0) SetCrossHairGrey();

    }


    void HideMuzzle() {
        if(!hideMuzzle) return;
        hidMuzzleAfter -= Time.deltaTime;
        if(hidMuzzleAfter < 0) {
            if(muzzle!=null && muzzle.active) muzzle.SetActive(false);
            hideMuzzle = false;
        }

    }

    private void HandleRayCast() {
        if(!isDrawn) return;
        Ray ray; 
        if(Camera.main) {
            ray = Camera.main.ViewportPointToRay(aim);
        }
        else return;
         normalInfectantInRange = null;
         RaycastHit hit;
          if (Physics.Raycast(ray, out hit, range)) {
              hitPoint = hit.point;
              collided = hit.collider.gameObject;
          if(collided.CompareTag(NormalInfectantConstants.TAG)){
                  normalInfectantInRange = collided;
                   SetCrossHairRed();
                   return;
             //Debugging
          
            }
          }
             //Debug.DrawLine(ray.origin, hit.point);
        SetCrossHairGreen();
    }
    
    

    void FixedUpdate(){
        HandleRayCast();
    }


    private void DrawBulletHole() {
         GameObject bulletHoleInstance = Instantiate(bulletHole, hitPoint, crossHair.transform.rotation);
         bulletHoleInstance.transform.SetParent(bulletHoles.transform,true);
         bulletHoleInstance.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
    }

    private void PlayAudio(AudioClip clip) {
        AudioSource audioSource =  gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }


    public void  HandleFire() {
         if(Input.GetButton("Fire1") && isDrawn){
             if(currentAmmo<=0) {
                 if(!outOfAmmo.isPlaying) {
                    outOfAmmo.clip = dryFire;
                    outOfAmmo.Play();
                 }
                 return;
             }
             PlayAudio(clip);
             if(type!= WeaponsConstants.WEAPON_TYPES["PISTOL"] && !muzzle.active) muzzle.SetActive(true);
             if(animator) animator.SetTrigger(WeaponsConstants.FIRE);
             if(collided && ! collided.CompareTag(NormalInfectantConstants.TAG) ) DrawBulletHole();
             if(normalInfectantInRange) normalInfectantInRange.GetComponent<NormalInfectant>().GetShot(dmg);   
             currentAmmo -= 1;
             nextFire = 1/(rateOfFire/60.0);
             hidMuzzleAfter = 0.15f;
          
        }
    
}


    public void Initialize(string type, int dmg, int clipCapacity,int rateOfFire, int maxAmmo, GameObject weapon, int range,(Vector3,Vector3) cameraData, Vector3 aim) {
        this.type = type;
        this.dmg = dmg;
        this.clipCapacity = clipCapacity;
        this.rateOfFire = rateOfFire;
        this.currentAmmo = clipCapacity;
        this.maxAmmo = maxAmmo;
        this.weapon = weapon;
        this.animator = weapon.GetComponentsInChildren<Animator>().Length == 0 ? null : weapon.GetComponentsInChildren<Animator>()[0] ;
        this.totalAmmo = maxAmmo;
        this.range = range;
        this.cameraData = cameraData;
        this.aim = aim;
        outOfAmmo =  gameObject.AddComponent<AudioSource>();
        outOfAmmo.playOnAwake = false;
        clip =  Resources.Load<AudioClip>($"Sounds/Weapons/{type}");
        reload = Resources.Load<AudioClip>("Sounds/Weapons/reload");
        dryFire = Resources.Load<AudioClip>("Sounds/Weapons/dryFire");
    }

    public bool Reload() {
        if(type== WeaponsConstants.WEAPON_TYPES["PISTOL"]){
            currentAmmo = clipCapacity;
            PlayAudio(reload);
            return true;
        }
        if(currentAmmo == clipCapacity) return false;
        if(totalAmmo <= 0) return false;
        if(totalAmmo>=clipCapacity) currentAmmo = clipCapacity;
        else currentAmmo = totalAmmo;
        totalAmmo -= currentAmmo;
        PlayAudio(reload);
        return true;
        }

    public void Reset() {
        this.currentAmmo = clipCapacity;
        this.totalAmmo = maxAmmo;
    }
    

    private void SetCrossHairGreen(){
        if(crossHair==null) return;
        //SpriteRenderer sprite = crossHair.GetComponent<SpriteRenderer>();
        crossHair.color = green;
    }

    private void SetCrossHairRed() {
        if(crossHair==null) return;
         //SpriteRenderer sprite = crossHair.GetComponent<SpriteRenderer>();
         crossHair.color = red; 
    }

    private void SetCrossHairGrey() {
        if(crossHair==null) return;
        crossHair.color = grey;
    }


    public void SetCrossHair(GameObject crossHair) {
        this.crossHair = crossHair.GetComponent<SpriteRenderer>();
    }

    public void SetMuzzle(GameObject muzzle) {
        this.muzzle = muzzle;
    }

    public int GetDmg(){
        return dmg;
    }

    public int GetClipCapacity(){
        return  clipCapacity;
    }

    public int GetRateOfFire(){
        return rateOfFire;
    }


    public int GetMaxAmmo(){
        return maxAmmo;
    }

    public int GetTotalAmmo(){
        return totalAmmo;
    }

    public int GetCurrentAmmo() {
        return currentAmmo;
    }

    public string GetType(){
        return type;
    }

    public int GetRange() {
        return range;
    }

    public (Vector3,Vector3) GetCameraData(){
        return cameraData;
    }

    public void Hide() {
        if(muzzle!=null && muzzle.active) muzzle.SetActive(false);
        weapon.SetActive(false);
    }

    public void UnHide() {
        weapon.SetActive(true);
    }

    public Vector3 GetAim() {
        return aim;
    }

    public void SetIsDrawn(bool isDrawn) {
        this.isDrawn = isDrawn;
    }

    public GameObject GetWeapon() {
        return weapon;
    }
}
