using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject weaponEQ;
   
    private IDictionary<string,Weapon> allWeapons;

    private IDictionary<string,GameObject> weaponResources;

    private bool fullWeapons;
    private bool[] weaponsSpawnPositions;
    //private List<Weapon> spawnedWeapons;

    private int spawnCount;  

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
        if(allWeapons.Count == WeaponsConstants.WEAPON_TYPES.Count) fullWeapons = true;
        return weapon;
   }


   public Weapon GetWeapon(string type) {
       return allWeapons.ContainsKey(type) ? allWeapons[type] : null;
   } 
   
   
   void Awake(){
       fullWeapons = false;
       spawnCount = 0;
       weaponsSpawnPositions = new bool[WeaponsConstants.SpawnPositions.Length];
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
        Spawn(15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Weapon InitializeSpawn(string type, Vector3 position,int index) {
        GameObject weaponObject = weaponResources[type];
        GameObject weaponObjectInstance = Instantiate(weaponObject,position, Quaternion.identity);
        weaponObjectInstance.AddComponent<Weapon>();
        Weapon weapon = weaponObjectInstance.GetComponent<Weapon>();
        weapon.Initialize(type,weaponObjectInstance,index);
        weaponObjectInstance.transform.localScale = WeaponsConstants.SPAWN_SCALE[type];
        Vector3 rotation =WeaponsConstants.SPAWN_ROTATIONS[type];
        rotation.y = UnityEngine.Random.Range(30, 120);
        weaponObjectInstance.transform.localRotation = Quaternion.Euler(rotation);
        weaponObjectInstance.AddComponent<BoxCollider>();
        BoxCollider collider = weaponObjectInstance.GetComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = type=="shotgun"? new Vector3(15f,15f,15f) : type=="assaultRifle" ? new Vector3(1.4f,2f,1.4f) : new Vector3(1.4f,1.5f,1.4f);
        return weapon;
    
    
    }


    private string BiasedChooseFrom(string[] weaponTypes) {
        float random = Random.value * 100f;
        if (random < 40) return weaponTypes[0];
        if (random < 80) return weaponTypes[1];
        if(random<90) return weaponTypes[2];
        return weaponTypes[3];

    }

    public void PickUp(Weapon weapon, bool playReload) {
        int weaponIndex = weapon.GetSpawnIndex();
        spawnCount -=1;
        if(playReload) weapon.PlayReloadAndDestroy();
        else Destroy(weapon.GetWeapon());
        if(spawnCount < 1) Spawn(5);
        weaponsSpawnPositions[weaponIndex] = false; 

    }

    // Randomly choose from weapon types and initialize each weapon accordingly
    // Add weapon data in constants file
    public void Spawn(int spawnNumber) {
        if(!fullWeapons) {
         InitializeSpawn("huntingRifle", WeaponsConstants.SpawnPositions[0],0 );
         InitializeSpawn("shotgun", WeaponsConstants.SpawnPositions[1],1 );
         weaponsSpawnPositions[0] = true;
         weaponsSpawnPositions[1] = true;
        }
        string type;
        Vector3 spawnPosition;
        int index;
        Weapon weapon;
        for(int i=0;i<spawnNumber;i++){
            type = !fullWeapons ? BiasedChooseFrom(WeaponsConstants.WEAPON_TYPES_LIST) : Utils.RandomlyChooseFrom(WeaponsConstants.WEAPON_TYPES_LIST);
            bool isPositionTaken = true;
            while(isPositionTaken){
                 (Vector3,int) result = Utils.RandomlyChooseFrom(WeaponsConstants.SpawnPositions,true);
                 if(!weaponsSpawnPositions[result.Item2]){
                     isPositionTaken = false;
                     weaponsSpawnPositions[result.Item2] = true;
                     weapon = InitializeSpawn(type, result.Item1 , i);
                     spawnCount +=1;
                 }
               
            }
        }

    }
}
