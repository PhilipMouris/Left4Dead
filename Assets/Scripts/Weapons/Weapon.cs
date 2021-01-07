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

    private int range;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Shoot() {
        animator.SetTrigger(WeaponsConstants.FIRE);

    }


    public void Initialize(string type, int dmg, int clipCapacity,int rateOfFire, int maxAmmo, GameObject weapon, int range) {
        this.type = type;
        this.dmg = dmg;
        this.clipCapacity = clipCapacity;
        this.rateOfFire = rateOfFire;
        this.currentAmmo = clipCapacity;
        this.maxAmmo = maxAmmo;
        this.weapon = weapon;
        this.animator = weapon.GetComponentsInChildren<Animator>()[0];
        this.totalAmmo = maxAmmo;
        this.range = range;
        
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
}
