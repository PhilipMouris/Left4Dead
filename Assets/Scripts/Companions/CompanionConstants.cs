
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CompanionConstants 
{
   

   public static string zoey = "zoey";

   public static string louis = "louis";

   public static string rochelle = "ellie";
   public static (Vector3,Vector3) ZOEY_TRANSFORMATION_DATA = (
       //new Vector3(60.7964401f,4.08500004f,21.1779518f),
       new Vector3(60.6318398f,4.08599997f,19.9802818f),
       new Vector3(0f,187.903458f,0f)
   );

  public static (Vector3,Vector3) LOUIS_TRANSFORMATION_DATA = (
       //new Vector3(60.7964401f,4.08500004f,21.1779518f),
       new Vector3(57f,4.57299995f,20.2399998f),
       new Vector3(0f,187.903458f,0f)
   );

  public static (Vector3,Vector3) ROCHELLE_TRANSFORMATION_DATA = (
    //new Vector3(56.3800011f,4.09589529f,20.2959995f),
    new Vector3(56.3800011f,4.00899982f,20.2959995f),
    new Vector3(0f,179.156219f,0f)
  );

  public static IDictionary<string, string> COMPANION_PATHS = new Dictionary<string,string>() {
	{zoey,"Prefabs/Companions/Zoey/ZoeyGun"},
  {louis,"Prefabs/Companions/Louis/LouisGun"},
  {rochelle, "Prefabs/Companions/rochelle/rochelleGun"}
  };


  public static IDictionary<string, (Vector3 ,Vector3)> COMPANION_TRANSFORMATION = 
        new Dictionary<string, (Vector3,Vector3)>() {
        {zoey, ZOEY_TRANSFORMATION_DATA},
        {louis,LOUIS_TRANSFORMATION_DATA},
        {rochelle,ROCHELLE_TRANSFORMATION_DATA}
  };


   public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) ZOEY_WEAPON_DATA = ( "huntingRifle",
                                             70, 
                                             90, 
                                             240, 
                                             15, 
                                             15*6, 
                                            "Prefabs/Weapons/HuntingRifle/Prefab/Scar"
                                             );
  
  
   public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH)  LOUIS_WEAPON_DATA =  ("assaultRifle",
                                             20, 
                                             33, 
                                             600, 
                                             50, 
                                             50*4, 
                                             "Prefabs/Weapons/Rifle/Prefabs/RifleShooting"
                                             );


    public static (string TYPE,  
                int RANGE, 
                int DAMAGE, 
                int RATE_OF_FIRE, 
                int CLIP_CAPACITY, 
                int MAX_AMMO, 
                string PATH) ROCHELL_WEAPON_DATA = ("pistol",
                                             15, 
                                             36, 
                                             300, 
                                             15, 
                                             15*3, 
                                             "Prefabs/Weapons/Pistol/Modern Guns - Handgun/_Prefabs/Handgun Black/PistolShooting"
                                             );


  public static IDictionary<string,   (string,int,int,int,int,int,string)> COMPANION_WEAPONS = 
        new Dictionary<string,  (string,int,int,int,int,int,string)>() {
        {zoey, ZOEY_WEAPON_DATA},
        {louis,LOUIS_WEAPON_DATA},
        {rochelle,ROCHELL_WEAPON_DATA}
  };


}
