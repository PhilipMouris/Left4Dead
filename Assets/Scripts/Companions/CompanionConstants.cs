
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CompanionConstants 
{
   

   private static string zoey = "ZOEY";
   public static (Vector3,Vector3) ZOEY_TRANSFORMATION_DATA = (
       //new Vector3(60.7964401f,4.08500004f,21.1779518f),
       new Vector3(60.6318398f,4.08599997f,19.9802818f),
       new Vector3(0f,187.903458f,0f)
   );

  public static IDictionary<string, string> COMPANION_PATHS = new Dictionary<string,string>() {
	{zoey,"Prefabs/Companions/Zoey/ZoeyGun"},
  };


  public static IDictionary<string, (Vector3 ,Vector3)> COMPANION_TRANSFORMATION = 
        new Dictionary<string, (Vector3,Vector3)>() {
        {zoey, ZOEY_TRANSFORMATION_DATA}
  };

}
