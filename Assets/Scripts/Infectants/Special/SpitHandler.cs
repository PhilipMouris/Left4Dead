using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitHandler : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.gameObject.name == "Map_v1")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale += new Vector3(2, 0, 2);
            Invoke("Destroy", 5);
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FPSController")
            InvokeRepeating("DecreaseHealth", 0, 1);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "FPSController")
            CancelInvoke("DecreaseHealth");
    }

    public void Destroy()
    {
        CancelInvoke("DecreaseHealth");
        Destroy(gameObject);
    }

    public void DecreaseHealth()
    {
        gameManager.SetHealth(gameManager.GetHealth() - 20);
    }
}
