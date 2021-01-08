using UnityEngine;
public class WeaponsConstants
{ 
 // public  string[] WEAPON_TYPES = {"tacticalShotgun", "assaultRifle", "huntingRifle", "submachinGun", "pistol"};
  
  public const string FIRE = "Fire";

  public const string SHOOT = "shoot";

  public const string DRAW_PISTOL = "drawPistol";

  // PISTOL DATA
  public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) PISTOL_DATA = ( "pistol",
                                             20, 
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
               ) PISTOL_CAMERA_DATA = (
                                        new Vector3(0.05913162f,1.538786f,0.2859898f),
                                        new Vector3(18.599f,-5.792f,-0.117f)
               );



  // SHOT GUN DATA
  public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) SHOT_GUN_DATA = ( "shotGun",
                                             20, 
                                             350, 
                                             200, 
                                             10, 
                                             130, 
                                             "Prefabs/Weapons/Pistol/Modern Guns - Handgun/_Prefabs/Handgun Black/PistolShooting"
                                             );
  
  // SMG DATA
  public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) SMG_DATA = ( "smg",
                                             20, 
                                             20, 
                                             900, 
                                             50, 
                                             70, 
                                             "Prefabs/Weapons/Pistol/Modern Guns - Handgun/_Prefabs/Handgun Black/PistolShooting"
                                             );
  

  

  // HUNTING RIFLE DATA
  public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) HUNTING_RIFLE_DATA = ( "huntingRifle",
                                             20, 
                                             90, 
                                             240, 
                                             15, 
                                             165, 
                                             "Prefabs/Weapons/Pistol/Modern Guns - Handgun/_Prefabs/Handgun Black/PistolShooting"
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
                    Vector3 rotation) RIFLE_TRANSFORMATIONS = (new Vector3(0.25f,-0.026f,0.015f),
                                                            new Vector3(0.57f,0.5f,0.5f),
                                                            new Vector3(0.517f,-79.043f,-90.019f)
                                                          
                                                        );


    
public static (Vector3 position, 
               Vector3 rotation 
               ) RIFLE_CAMERA_DATA = (
                                        new Vector3(0.1358f,1.6335f,-0.0219f),
                                        new Vector3(5.88f,-5.67f,0.006f)
                                        // new Vector3(0.05913162f,1.538786f,0.2859898f),
                                        // new Vector3(18.599f,-5.792f,-0.117f)
               );




}