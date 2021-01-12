using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponsConstants

{ 
  //public  string[] WEAPON_TYPES = {"shotgun", "assaultRifle", "huntingRifle", "smg", "pistol"};
  
  public static IDictionary<string, string> WEAPON_TYPES = new Dictionary<string,string>() {
	{"PISTOL", "pistol"},
	{"ASSAULT_RIFLE", "assaultRifle"},
  {"SMG","smg"},
  {"HUNTING_RIFLE","huntingRifle"},
  {"SHOTGUN","shotgun"}
  };
  //  public static IDictionary<string, string> GRENADE_TYPES = new Dictionary<string,string>() {
  //    {"MOLOTOV_COCKTAIL","molotov"},
  //    {"PIPE_BOMB","pipe"},
  //    {"STUN_BOMB","stun"}
  // };

  public static string[] WEAPON_TYPES_LIST = {"assaultRifle","smg","huntingRifle","shotgun"};
  public static IDictionary<string,string> WEAPON_PATHS = new Dictionary<string,string>() {
    {"pistol",  "Prefabs/Weapons/Pistol/Modern Guns - Handgun/_Prefabs/Handgun Black/PistolShooting"},
    {"assaultRifle","Prefabs/Weapons/Rifle/Prefabs/RifleShooting"} ,
    {"smg", "Prefabs/Weapons/smg/MP7(v3)"},
    {"huntingRifle",  "Prefabs/Weapons/HuntingRifle/Prefab/Scar"},
    {"shotgun",  "Prefabs/Weapons/shotgun/MARMO32"}
  };

  
  public const string FIRE = "Fire";

  public const string SHOOT = "shoot";

  public const string DRAW_PISTOL = "drawPistol";


  public const string TAG = "weapon";

  // PISTOL DATA
  public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) PISTOL_DATA = ( "pistol",
                                             15, 
                                             36, 
                                             300, 
                                             15, 
                                             -1, 
                                             "Prefabs/Weapons/Pistol/Modern Guns - Handgun/_Prefabs/Handgun Black/PistolShooting"
                                             );

public static  (Vector3 position, 
                Vector3 scale, 
                Vector3 rotation) PISTOL_TRANSFORMATIONS = (new Vector3(0.08407628f,-0.02758249f,-0.03026863f),
                                                            new Vector3(1.7f,1f,1f),
                                                            new Vector3(13.166f,96.78f,91.854f)
                                                          
                                                        );
  

public static (Vector3 position, 
               Vector3 rotation 
               ) PISTOsL_CAMERA_DATA = (
                                        new Vector3(0.05913162f,1.538786f,0.2859898f),
                                        new Vector3(18.599f,-5.792f,-0.117f)
               );

public static (Vector3 position, 
               Vector3 rotation 
               ) PISTOL_CAMERA_DATA = (
             // new Vector3(0.1358f,1.6335f,-0.0219f),
                                        // new Vector3(5.88f,-5.67f,0.006f)
                                        // new Vector3(0.05913162f,1.538786f,0.2859898f),
                                        // new Vector3(18.599f,-5.792f,-0.117f)
                                        
                                        // new Vector3(-0.007f,1.555f,0.0048f),
                                        new Vector3(-0.0209999997f,1.58000004f,0.0780000016f),
                                        new Vector3(5.88f,-5.67f,0.006f)
               );






  // SHOT GUN DATA
  public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) SHOT_GUN_DATA = ( "shotgun",
                                             20, 
                                             250, 
                                             200, 
                                             10, 
                                             130, 
                                             "Prefabs/Weapons/shotgun/MARMO32"
                                             );
                                           
  
 public static  (Vector3 position, 
                    Vector3 scale, 
                    Vector3 rotation) SHOT_GUN_TRANSFORMATIONS = (
                                                            // new Vector3(0.25f,-0.026f,0.015f),
                                                            // new Vector3(0.1f,0.1f,0.1f),
                                                            // new Vector3(-90f,180f,0f)
                                                            
                                                            new Vector3(0.150999993f,-0.0289999992f,0.0250000004f),
                                                             new Vector3(0.1f,0.1f,0.1f),
                                                            new Vector3(270.756409f,261.248047f,286.122375f)
                                                           

                                                        );

  
  // SMG DATA
  public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) SMG_DATA = ( "smg",
                                             15, 
                                             20, 
                                             900, 
                                             50, 
                                             70, 
                                             "Prefabs/Weapons/smg/MP7(v3)"
                                             );

  public static  (Vector3 position, 
                    Vector3 scale, 
                    Vector3 rotation) SMG_TRANSFORMATIONS = (new Vector3(0.139200002f,-0.0140000004f,0.0110999998f),
                                                            new Vector3(1,1.29999995f,1f),
                                                            new Vector3(355.381287f,105.65963f,96.798317f)
                                                          
                                                        );

  public static (Vector3 position, 
               Vector3 rotation 
               ) SMG_CAMERA_DATA = (
             // new Vector3(0.1358f,1.6335f,-0.0219f),
                                        // new Vector3(5.88f,-5.67f,0.006f)
                                        // new Vector3(0.05913162f,1.538786f,0.2859898f),
                                        // new Vector3(18.599f,-5.792f,-0.117f)
                                        
                                        // new Vector3(-0.007f,1.555f,0.0048f),
                                        new Vector3(0.0308999997f,1.55799997f,0.0780000016f),
                                        new Vector3(5.88f,-5.67f,0.006f)
               );


  

  

  // HUNTING RIFLE DATA
  public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) HUNTING_RIFLE_DATA = ( "huntingRifle",
                                             70, 
                                             90, 
                                             240, 
                                             15, 
                                             165, 
                                            "Prefabs/Weapons/HuntingRifle/Prefab/Scar"
                                             );
 
   public static  (Vector3 position, 
                    Vector3 scale, 
                    Vector3 rotation) HUNTING_RIFLE_TRANSFORMATIONS = (new Vector3(0.236599997f,-0.0304000005f,0.0456999987f),
                                                            new Vector3(0.800000012f,0.699999988f,0.5f),
                                                            new Vector3(277.279877f,19.9621258f,166.510986f)
                                                          
                                                        );

                  
 
 
 
 
 
 
 
 
 
 
 
 
 
  // ASSAULT RIFLE DATA
  public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) ASSAULT_RIFLE_DATA = ("assaultRifle",
                                             20, 
                                             33, 
                                             600, 
                                             50, 
                                             450, 
                                             "Prefabs/Weapons/Rifle/Prefabs/RifleShooting"
                                             );
                                         

    public static  (Vector3 position, 
                    Vector3 scale, 
                    Vector3 rotation) RIFLE_TRANSFORMATIONS = (
                                                            // new Vector3(0.25f,-0.026f,0.015f),
                                                            // new Vector3(0.7f,0.5f,0.5f),
                                                            // new Vector3(0.517f,-79.043f,-90.019f)

                                                            //  new Vector3(0.248999998f,-0.0149999997f,0.00800000038f),
                                                            //  new Vector3(0.699999988f,0.699999988f,0.600000024f),
                                                            //  new Vector3(2.02892876f,287.376648f,263.721405f)

                                                            new Vector3(0.229000002f,-0.0240000002f,0.0170000009f),
                                                            new Vector3(0.400000006f,0.5f,0.5f),
                                                            new Vector3(1.01758265f,281.122681f,261.188141f)
                                                           
                                                        );


    
public static (Vector3 position, 
               Vector3 rotation 
               ) RIFLE_CAMERA_DATA = (
             // new Vector3(0.1358f,1.6335f,-0.0219f),
                                        // new Vector3(5.88f,-5.67f,0.006f)
                                        // new Vector3(0.05913162f,1.538786f,0.2859898f),
                                        // new Vector3(18.599f,-5.792f,-0.117f)
                                        
                                        // new Vector3(-0.007f,1.555f,0.0048f),
                                        new Vector3(-0.0209999997f,1.58000004f,0.0780000016f),
                                        new Vector3(5.88f,-5.67f,0.006f)
               );



public static Vector3 PISTOL_AIM = new Vector3(0.51f,0.51f,0);

public static Vector3 ASSAULT_RIFLE_AIM = new Vector3(0.51f,0.5f,0);

public static Vector3 SMG_AIM = ASSAULT_RIFLE_AIM;

public static Vector3 SHOTGUN_AIM = new Vector3(0.55f,0.5f,0);



public static Vector3 HUNTING_RIFLE_AIM = SHOTGUN_AIM;

public static string[] GRENADE_TYPES = {"molotov","pipe","stun"};
  public static int MOLOTOV_MAX = 3;

  public static int PIPE_MAX = 2;

  public static int STUN_MAX = 2;
}



//SPAWN DATA
  public static IDictionary<string, Vector3> SPAWN_ROTATIONS = new Dictionary<string,Vector3>() {
	{"assaultRifle", new Vector3(0f,0,90f)},
  {"smg", new Vector3(0,0,90f)},
  {"huntingRifle", new Vector3(90f,0,0)},
  {"shotgun", new Vector3(90,0,0)}
  };

  public static IDictionary<string, Vector3> SPAWN_SCALE = new Dictionary<string,Vector3>() {
	{"assaultRifle", new Vector3(1f,1f,1f)},
  {"smg", new Vector3(2f,2f,2f)},
  {"huntingRifle", new Vector3(1.5f,1.5f,1.5f)},
  {"shotgun", new Vector3(0.15f,0.15f,0.15f)}
  };
  public static Vector3[] SpawnPositions = {
    // new Vector3(61.5169792f,4.12f,18.3255501f),
    // new Vector3(60.5169792f,4.12f,18.3255501f),
    // new Vector3(58.5169792f,4.12f, 18.3255501f),
    // new Vector3(57.5169792f,4.12f,18.3255501f)
    new Vector3(60.5169792f,4.11000013f,10.94555f),
    new Vector3(56.9099998f,4.11000013f,10.94555f),
    new Vector3(62.1500015f,4.0f,-2.21000004f),
    new Vector3(72.0199966f,4.0f,-2.21000004f),
    new Vector3(78.2399979f,4.0f,2.3900001f),
    new Vector3(83.5999985f,4.0f,2.3900001f),
    new Vector3(83.5999985f,4.0f,7.13000011f),
    new Vector3(83.5999985f,4.0f,12.3999996f),
    new Vector3(83.5999985f,4.0f,16.0400009f),
    /// A1
    // A2
    new Vector3(78.5999985f,4.19000006f,-42.9099998f),
    new Vector3(78.5999985f,4.19000006f,-30.2999992f),
    new Vector3(72.8000031f,4.19000006f,-30.2999992f),
    new Vector3(72.8000031f,4.19000006f,-10.8999996f),
    new Vector3(87.4300003f,4.19000006f,-18.8500004f),
    new Vector3(87.4300003f,4.19000006f,-9.82999992f),
    new Vector3(62.5999985f,4.19000006f,-45.5999985f),
    new Vector3(50.2000008f,4.19000006f,-38.0400009f),


    // A3 
    new Vector3(37.8899994f,4.0f,-37.0400009f),
    new Vector3(29.6599998f,4.0f,-38.0400009f),
    new Vector3(29.6599998f,4.0f,-49.7000008f),
    new Vector3(29.6599998f,4.0f,-49.7000008f),
    new Vector3(10.6899996f,3.7f,-39.0099983f),
    new Vector3(8f,3.7f,-28.8999996f),
    new Vector3(8f,3.7f,-19.8999996f),
    new Vector3(15.2200003f,2f,-26.9300003f),
    new Vector3(27.5200005f,2.8f,-30.29300003f),
    new Vector3(39.2999992f,4.19f,-26f),
    // new Vector3(49.1100006f,4.19000006f,-29.9200001f),


    // A4
    new Vector3(49.1100006f,4f,-29.9200001f),
    new Vector3(49.1100006f,3.7f,-85.5999985f),
    new Vector3(45.8100014f,3.7f,-87.6900024f),
    new Vector3(28.8500004f,3.7f,-85.4499969f),
    new Vector3(23.5200005f,3.7f,-72.3600006f),
    new Vector3(12.1999998f,3.7f,-72.3600006f),
    new Vector3(7.8499999f,3.7f,-85.7699966f),
    new Vector3(-4.44999981f,3.7f,-80.4000015f),
    new Vector3(-4.44999981f,4.0000013f,-73.5999985f),
    new Vector3(-4.44999981f,4.0000013f,-64.6500015f),
    new Vector3(-16.9599991f,4.0000013f,-53.5f),
    new Vector3(-4.69999981f,3.7000013f,-38.7000008f),
    new Vector3(-17.3299999f,3.7f,-26.8400002f),
    new Vector3(-17.3299999f,3.7f,-18.6700001f),
    new Vector3(-17.3299999f,3.7f,-16.3500004f),
    new Vector3(-25.7299995f,2f,-16.3500004f),
    new Vector3(-25.7299995f,2f,-3.28999996f),
    new Vector3(-23.3999996f,2f,-43.5999985f),
    new Vector3(-23.3999996f,2f,-67.4000015f)
    // Vector3(-25.1299992,4.11000013,-16.3500004)
    // Vector3(-25.1299992,4.11000013,-1.64999998)

  };
}




