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

    private string type;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Initialize(string type, int dmg, int clipCapacity,int rateOfFire, int maxAmmo) {
        this.type = type;
        this.dmg = dmg;
        this.clipCapacity = clipCapacity;
        this.rateOfFire = rateOfFire;
        this.maxAmmo = maxAmmo;
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

    public string getType(){
        return type;
    }
}
