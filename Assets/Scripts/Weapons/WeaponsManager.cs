using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    // Start is called before the first frame update
   
   
   public Weapon InitializeWeapon((string,int,int,int,int,int,string) weaponData) {
        var (TYPE,RANGE,DAMAGE,RATE_OF_FIRE,CLIP_CAPACITY,MAX_AMMO,PATH) = weaponData;
        GameObject weaponObject = Resources.Load(PATH) as GameObject;
        GameObject weaponObjectInstance = Instantiate(weaponObject, weaponObject.transform.position, Quaternion.identity);
        weaponObjectInstance.AddComponent<Weapon>();
        Weapon weapon = weaponObjectInstance.GetComponent<Weapon>();
        weapon.Initialize(TYPE,
                          DAMAGE,
                          CLIP_CAPACITY,
                          RATE_OF_FIRE,
                          MAX_AMMO,
                          weaponObjectInstance
                          );
        return weapon;
   } 
   
   
   
   
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Randomly choose from weapon types and initialize each weapon accordingly
    // Add weapon data in constants file
    public void Spawn() {
        Debug.Log("SPAWN WEAPONS");
    }
}
