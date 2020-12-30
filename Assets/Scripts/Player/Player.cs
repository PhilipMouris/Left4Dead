using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float moveSpeed = 2.5f;
    private GameObject camera;

    private Animator animator;

    private int HP;

    private Weapon[] weapons;

    private Companion companion;

    private Gernade[] gernades;

    private RageMeter rageMeter;




    
    
    void Awake()
    {
         this.camera = GameObject.Find(PlayerConstants.MAIN_CAMERA);
         // animator = GetComponent<Animator>();
    }
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

         this.transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime);
    }
}
