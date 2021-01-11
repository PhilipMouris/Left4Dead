using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale += new Vector3(2, 0, 2);
        Invoke("Destroy", 5);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
