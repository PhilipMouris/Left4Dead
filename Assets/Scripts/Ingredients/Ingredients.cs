using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    private string displayName;
    private IngredientsManager manager;

    void Start()
    {
        manager = FindObjectOfType<IngredientsManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(string name)
    {
        Utils.AddBoxCollider(gameObject);
        SetDisplayName(name);
    }

    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    public string getDisplayName(string name)
    {
        return displayName;
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            
            manager.UpdateLocations(gameObject);
            //Player current_player = player.GetComponent<Player>();
            //GameObject ingredient = this.gameObject.GetComponent<Ingredients>();
            Debug.Log("Add");
            //Craft current_ingredient = player.GetComponent<Craft>();
            //current_ingredient.AddIngredient(ingredient);
            GameObject.Find("GameManager").GetComponent<Craft>().AddIngredient(this.gameObject);
            
        }

    }
}