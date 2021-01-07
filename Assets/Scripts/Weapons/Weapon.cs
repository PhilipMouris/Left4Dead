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


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Shoot() {
        // SHOOT LOGIC HERE

    }


    public void Initialize(int dmg, int clipCapacity,int rateOfFire, int maxAmmo, GameObject weapon) {
        this.dmg = dmg;
        this.clipCapacity = clipCapacity;
        this.rateOfFire = rateOfFire;
        this.maxAmmo = maxAmmo;
        this.weapon = weapon;
        this.animator = GetComponent<Animator>();
        this.totalAmmo = maxAmmo;
        
    }

    public int getDmg(){
        return dmg;
    }

    public int getClipCapacity(){
        return  clipCapacity;
    }

    public int getRateOfFire(){
        return rateOfFire;
    }


    public int getMaxAmmo(){
        return maxAmmo;
    }

    public int getTotalAmmo(){
        return totalAmmo;
    }

    public int getCurrentAmmo() {
        return currentAmmo;
    }
}
