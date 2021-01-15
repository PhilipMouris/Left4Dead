using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    private int spawnIndex;
    private GameObject specialInfectantInRange;
    private GameManager gameManager;

    private AudioMixerGroup SFXGroup;
    private Vector3 cameraPosition;

    private bool isCompanion;

    private bool isShootingCompanion;

    public int layerMask = 1 << 10;


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
         specialInfectantInRange = null;
         RaycastHit hit;
          if (Physics.Raycast(ray, out hit, range, ~layerMask)) {
              hitPoint = hit.point;
              collided = hit.collider.gameObject;
          if(collided.CompareTag(NormalInfectantConstants.TAG)){
                  normalInfectantInRange = collided;
                   SetCrossHairRed();
                   return;
             //Debugging
            }
            Debug.Log(collided.tag + "SPECIALL");
            if(SpecialInfectantConstants.TAGS.Contains(collided.tag)){
                    Debug.Log("SHOOT SPECIAL");
                   specialInfectantInRange = collided;
                   SetCrossHairRed();
                   return;
            }

          }
             //Debug.DrawLine(ray.origin, hit.point);
        SetCrossHairGreen();
    }
    
    

    void FixedUpdate(){
        if(!isCompanion)
             HandleRayCast();
    }


    private void DrawBulletHole() {
         GameObject bulletHoleInstance = Instantiate(bulletHole, hitPoint, crossHair.transform.rotation);
         bulletHoleInstance.transform.SetParent(bulletHoles.transform,true);
         bulletHoleInstance.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
    }

    public void PlayReloadAndDestroy() {
        // AudioSource audioSource =  gameObject.AddComponent<AudioSource>();
        // audioSource.outputAudioMixerGroup = SFXGroup;
        // audioSource.clip = reload;
        // audioSource.Play();
        if(gameObject)
            Destroy(gameObject);
    }
    private void PlayAudio(AudioClip clip) {
        AudioSource audioSource =  gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = SFXGroup;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }


    public void HandleFire() {
         if( (Input.GetButton("Fire1") && isDrawn && !this.isCompanion) || this.isShootingCompanion){
             if(currentAmmo<=0) {
                 if(!outOfAmmo.isPlaying) {
                    outOfAmmo.outputAudioMixerGroup = SFXGroup; 
                    outOfAmmo.clip = dryFire;
                    outOfAmmo.Play();
                 }
                 return;
             }
             PlayAudio(clip);
             if(type!= WeaponsConstants.WEAPON_TYPES["PISTOL"] && muzzle && !muzzle.active ) muzzle.SetActive(true);
             if(animator) animator.SetTrigger(WeaponsConstants.FIRE);
             if(collided && ! collided.CompareTag(NormalInfectantConstants.TAG) && !SpecialInfectantConstants.TAGS.Contains(collided.tag) ) 
                    DrawBulletHole();
             if(normalInfectantInRange) {
                int dealtDamage = gameManager.GetIsRaged() ? 2*dmg : dmg;
                normalInfectantInRange.GetComponent<NormalInfectant>().GetShot(dealtDamage);
                //if(isDead) gameManager.EnemyDead("normal");
             }
             else {
                if(specialInfectantInRange){
                    specialInfectantInRange.GetComponent<SpecialInfectedGeneral>().GetShot(dmg);
                //  if(isDead) gameManager.EnemyDead("special");
                } 
             }  

             currentAmmo -= 1;
             nextFire = 1/(rateOfFire/60.0);
             hidMuzzleAfter = 0.15f;
          
        }
    
    }

    public void ShootCompanion(string type, GameObject enemy) {
        switch(type) {
            case "normal":normalInfectantInRange = enemy;break;
            default: specialInfectantInRange = enemy;break;
        }

    }

    public void SetIsShootingCompanion(bool isShooting){
        this.isShootingCompanion = isShooting;
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
        clip =  Resources.Load<AudioClip>($"Audio/SFX/{type}");
        reload = Resources.Load<AudioClip>("Audio/SFX/reload");
        dryFire = Resources.Load<AudioClip>("Audio/SFX/dryFire");
        SFXGroup = GameObject.Find("SFXManager").GetComponent<SFXManager>().SFXGroup;
        //clip =  Resources.Load<AudioClip>($"Sounds/Weapons/{type}");
        //reload = Resources.Load<AudioClip>("Sounds/Weapons/reload");
        //dryFire = Resources.Load<AudioClip>("Sounds/Weapons/dryFire");
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void Initialize(string type, GameObject weapon,int spawnIndex) {
        this.type = type;
        this.weapon = weapon;
        reload = Resources.Load<AudioClip>("Sounds/Weapons/reload");
        SFXGroup = GameObject.Find("SFXManager").GetComponent<SFXManager>().SFXGroup;
        this.spawnIndex = spawnIndex;
    }
    // string TYPE,  
    //             int RANGE, 
    //             int DAMAGE, 
    //             int RATE_OF_FIRE, 
    //             int CLIP_CAPACITY, 
    //             int MAX_AMMO, 
    //             string PATH 
    public void InitializeCompanionWeapon((string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) data) {
        var (TYPE,RANGE,DAMAGE,RATE_OF_FIRE,CLIP_CAPACITY,MAX_AMMO,PATH) = data;
        this.type = TYPE;
        this.dmg = DAMAGE;
        this.clipCapacity = CLIP_CAPACITY;
        this.rateOfFire = RATE_OF_FIRE;
        this.range = CLIP_CAPACITY;
        this.maxAmmo = MAX_AMMO;
        this.currentAmmo = clipCapacity;
        if(type != "pistol")
            Destroy( this.gameObject.GetComponentInChildren<ParticleSystem>() );
        isDrawn = true;
        this.animator = this.gameObject.GetComponentsInChildren<Animator>().Length == 0 ? null : this.gameObject.GetComponentsInChildren<Animator>()[0] ;
        clip =  Resources.Load<AudioClip>($"Audio/SFX/{type}");
        reload = Resources.Load<AudioClip>("Audio/SFX/reload");
        dryFire = Resources.Load<AudioClip>("Audio/SFX/dryFire");
        isCompanion = true;
        outOfAmmo =  gameObject.AddComponent<AudioSource>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        outOfAmmo.playOnAwake = false;
        SFXGroup = GameObject.Find("SFXManager").GetComponent<SFXManager>().SFXGroup;
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


    public void SetExtraClip() {
        int newAmmo = this.currentAmmo + this.clipCapacity;
        if(newAmmo <= maxAmmo) {
            this.currentAmmo = newAmmo;
        }
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

    void OnTriggerEnter(Collider collidedPlayer)
    {
        
        if (collidedPlayer.gameObject.CompareTag("Player"))
        {
             collidedPlayer.gameObject.GetComponentInChildren<Player>().SetWeaponInRange(this);
        }
    }
    void OnTriggerExit(Collider collidedPlayer)
    {
        
        if (collidedPlayer.gameObject.CompareTag("Player"))
        {   
            collidedPlayer.gameObject.GetComponentInChildren<Player>().SetWeaponInRange(null);
         
        }
    }

    public int GetSpawnIndex() {
        return spawnIndex;
    }

    
}
