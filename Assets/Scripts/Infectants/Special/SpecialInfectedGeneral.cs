using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInfectedGeneral : MonoBehaviour
{
    // Start is called before the first frame update
   
    protected NormalInfectantsManager normalInfectantsManager;
   
    public virtual void GetShot(int dmg){
        
    }

    // Update is called once per frame
    
    public virtual void Stun()
    {

    }

    public virtual void Unstun()
    {

    }

    public virtual bool GetIsStunned()
    {
        return false;
    }

    public virtual void GetAttracted(Transform grenadeLocation)
    {

    }

    public virtual void GetUnAttracted()
    {

    }

}
