using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject weaponEQ;
   
    private IDictionary<string,Weapon> allWeapons;

    private IDictionary<string,GameObject> weaponResources; 

   public static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }


   public Weapon InitializeWeapon(string type) {
       (string,int,int,int,int,int,string) weaponData;
       (Vector3, Vector3,Vector3) transformationData;
       (Vector3,Vector3) cameraData = WeaponsConstants.RIFLE_CAMERA_DATA;
       Vector3 aim;
       switch(type) {
           case "pistol":
            weaponData = WeaponsConstants.PISTOL_DATA;
            transformationData = WeaponsConstants.PISTOL_TRANSFORMATIONS;
            aim = WeaponsConstants.PISTOL_AIM;
            break;
           case "assaultRifle":
            weaponData = WeaponsConstants.ASSAULT_RIFLE_DATA;
            transformationData = WeaponsConstants.RIFLE_TRANSFORMATIONS;
            aim = WeaponsConstants.ASSAULT_RIFLE_AIM;
            break;
           case "shotgun":
            weaponData = WeaponsConstants.SHOT_GUN_DATA;
            transformationData = WeaponsConstants.SHOT_GUN_TRANSFORMATIONS;
            aim = WeaponsConstants.SHOTGUN_AIM;
            break;
           case "huntingRifle":
            weaponData = WeaponsConstants.HUNTING_RIFLE_DATA;
            transformationData = WeaponsConstants.HUNTING_RIFLE_TRANSFORMATIONS;
            aim = WeaponsConstants.HUNTING_RIFLE_AIM;
            break;
           default:
             weaponData = WeaponsConstants.SMG_DATA;
             transformationData = WeaponsConstants.SMG_TRANSFORMATIONS;
             aim = WeaponsConstants.SMG_AIM;
             cameraData = WeaponsConstants.SMG_CAMERA_DATA;
             break;
       }
        var (TYPE,RANGE,DAMAGE,RATE_OF_FIRE,CLIP_CAPACITY,MAX_AMMO,PATH) = weaponData;
        var (position,scale,rotation) = transformationData;
        //GameObject weaponObject = Resources.Load(PATH) as GameObject;
        GameObject weaponObject = weaponResources[type];
        GameObject weaponObjectInstance = Instantiate(weaponObject, weaponObject.transform.position, Quaternion.identity);
        Destroy( weaponObjectInstance.GetComponentInChildren<ParticleSystem>() );
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
                          RANGE,
                          cameraData,
                          aim
                          );
        weapon.Hide();
        allWeapons.Add(type,weapon);
        return weapon;
   }


   public Weapon GetWeapon(string type) {
       return allWeapons.ContainsKey(type) ? allWeapons[type] : null;
   } 
   
   
   void Awake(){
       weaponEQ = GameObject.Find(PlayerConstants.WEAPON_EQ);
       allWeapons = new Dictionary<string, Weapon>();
       InitializeWeaponResources();
   }


   void InitializeWeaponResources() {
        weaponResources = new Dictionary<string,GameObject>();
        foreach(KeyValuePair<string, string> kvp in WeaponsConstants.WEAPON_PATHS) {
            GameObject weaponObject = Resources.Load(kvp.Value) as GameObject;
            weaponResources.Add(kvp.Key, weaponObject);
        }
   }
   
   
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeSpawn(string type, Vector3 position) {
        GameObject weaponObject = weaponResources[type];
        GameObject weaponObjectInstance = Instantiate(weaponObject,position, Quaternion.identity);
        weaponObjectInstance.AddComponent<Weapon>();
        Weapon weapon = weaponObjectInstance.GetComponent<Weapon>();
        weapon.Initialize(type,weaponObjectInstance);
        weaponObjectInstance.transform.localScale = WeaponsConstants.SPAWN_SCALE[type];
        weaponObjectInstance.transform.localRotation = Quaternion.Euler(WeaponsConstants.SPAWN_ROTATIONS[type]);
        weaponObjectInstance.AddComponent<BoxCollider>();
        BoxCollider collider = weaponObjectInstance.GetComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(1f,1.5f,1f);
    }

    // Randomly choose from weapon types and initialize each weapon accordingly
    // Add weapon data in constants file
    public void Spawn() {
        InitializeSpawn("assaultRifle",WeaponsConstants.SpawnPositions[0]);
    }
}
