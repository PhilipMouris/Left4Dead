using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Utils
{
    // Start is called before the first frame update
    public static float RandomlyChooseFrom(float[] items)
    {
        int choice = UnityEngine.Random.Range(0, items.Length);
        return items[choice];
    }

    public static double ManhattenDistance(Vector3 positionA,Vector3 positionB){
        double horizontalDistance = Math.Pow((positionA.x - positionB.x), 2);
        double verticalDistance = Math.Pow((positionA.z - positionB.z), 2);
        return Math.Sqrt(horizontalDistance + verticalDistance);
    }


    public static BoxCollider AddBoxCollider(GameObject gameObject){
         gameObject.AddComponent<BoxCollider>();
         BoxCollider collider = gameObject.GetComponent<BoxCollider>();
         collider.isTrigger = true;
         return collider;
    }
}
