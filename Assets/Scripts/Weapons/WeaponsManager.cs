using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject weaponEQ;
   
   

   public static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
   public Weapon InitializeWeapon((string,int,int,int,int,int,string) weaponData, (Vector3, Vector3,Vector3) transformationData ) {
        var (TYPE,RANGE,DAMAGE,RATE_OF_FIRE,CLIP_CAPACITY,MAX_AMMO,PATH) = weaponData;
        var (position,scale,rotation) = transformationData;
        GameObject weaponObject = Resources.Load(PATH) as GameObject;
        GameObject weaponObjectInstance = Instantiate(weaponObject, weaponObject.transform.position, Quaternion.identity);
        weaponObjectInstance.AddComponent<Weapon>();
        Weapon weapon = weaponObjectInstance.GetComponent<Weapon>();
        weaponObjectInstance.transform.SetParent(weaponEQ.transform,true);
        weaponObjectInstance.transform.localScale = scale;
        weaponObjectInstance.transform.localPosition = position;
        weaponObjectInstance.transform.localRotation =  Quaternion.Euler(rotation);
        SetLayerRecursively(weaponObjectInstance,8);
        //weaponObjectInstance.layer = 8;

        weapon.Initialize(TYPE,
                          DAMAGE,
                          CLIP_CAPACITY,
                          RATE_OF_FIRE,
                          MAX_AMMO,
                          weaponObjectInstance,
                          RANGE
                          );
        return weapon;
   } 
   
   
   void Awake(){
       weaponEQ = GameObject.Find(PlayerConstants.WEAPON_EQ);
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
