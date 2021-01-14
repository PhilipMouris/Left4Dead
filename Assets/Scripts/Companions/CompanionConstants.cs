
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CompanionConstants 
{
   

   private static string zoey = "ZOEY";

   private static string louis = "LOUIS";
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
  public static IDictionary<string, string> COMPANION_PATHS = new Dictionary<string,string>() {
	{zoey,"Prefabs/Companions/Zoey/ZoeyGun"},
  {louis,"Prefabs/Companions/Louis/LouisGun"}
  };


  public static IDictionary<string, (Vector3 ,Vector3)> COMPANION_TRANSFORMATION = 
        new Dictionary<string, (Vector3,Vector3)>() {
        {zoey, ZOEY_TRANSFORMATION_DATA},
        {louis,LOUIS_TRANSFORMATION_DATA}
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
                string PATH)  LOUIS_WEAPON_DATA = ( "huntingRifle",
                                             70, 
                                             90, 
                                             240, 
                                             15, 
                                             15*6, 
                                            "Prefabs/Weapons/HuntingRifle/Prefab/Scar"
                                             );



  public static IDictionary<string,   (string,int,int,int,int,int,string)> COMPANION_WEAPONS = 
        new Dictionary<string,  (string,int,int,int,int,int,string)>() {
        {zoey, ZOEY_WEAPON_DATA},
        {louis,LOUIS_WEAPON_DATA}
  };


}
