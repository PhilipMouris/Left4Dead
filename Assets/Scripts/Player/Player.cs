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

    private List<Gernade> gernades = new List<Gernade>();

    private Gernade currentGernade;

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
    public void CollectGernade(Gernade gernade){
        gernades.Add(gernade); //Need to Check for Type of Gernade and If max Limit is Exceeded
    }
    public void ThrowGrenade()
    {
        if(gernades.Count>0){
            Debug.Log("Throwing");
            currentGernade = gernades[0];
            currentGernade.gameObject.SetActive(true);
            currentGernade.gameObject.transform.position = transform.position - new Vector3(0,0,1.5f);
            currentGernade.gameObject.transform.rotation = transform.rotation;
            Rigidbody grenadeRigidbody =currentGernade.gameObject.AddComponent<Rigidbody>();
            grenadeRigidbody.useGravity=true;
            grenadeRigidbody.AddForce((transform.forward+transform.up) * 10, ForceMode.Impulse);
            gernades.RemoveAt(0);
        }else{
            Debug.Log("No Gernade Available");
        }
        
    }
    
    
  

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            ThrowGrenade();
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
