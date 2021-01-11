using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    public GameObject[] models;
    public GameObject locations;
    /*public GameObject alcohol_locations;
    public GameObject gunpowder_locations;
    public GameObject canister_locations;
    public GameObject sugar_locations;
    public GameObject cloth_locations;
    */
    public GameObject player;
    private List<GameObject> all_items = new List<GameObject>();
    private List<GameObject> occupied_locations ;
    private List<GameObject> free_locations;
    private int occupied_num = 11;
    private int num_models = 2;
    private Transform[] all_locations;

    private List<Transform> ingredients_locations = new List<Transform>();

    private System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        all_locations =  locations.GetComponentsInChildren<Transform>();
        GenerateLocationsList(all_locations);
        ShuffleModels();
        for(int i = 0; i<models.Length; i++) {
            Spawn(models[i]);
        }
        
    }


   private void GenerateLocationsList(Transform[] locations) {
       foreach(Transform location in locations) {
           ingredients_locations.Add(location);
       }

   }

    public void ShuffleModels() {
        for (int i = 0; i < models.Length; i++ )
        {
            GameObject temp = models[i];
            int r = Random.Range(i, models.Length);
            models[i] = models[r];
            models[r] = temp;
        }

    }

    public void UpdateLocations(GameObject item){
        
        occupied_locations.Remove(item);
        //int new_item_index = Random.Range(0,free_locations.Count-1);
        //GameObject new_item = free_locations[new_item_index];
        //new_item.gameObject.SetActive(true);
        //occupied_locations.Add(new_item);
        //free_locations.RemoveAt(new_item_index);
        free_locations.Add(item);
        item.SetActive(false);
    }
   


    public void Spawn(GameObject model){
        occupied_locations = new List<GameObject>();
        free_locations = new List<GameObject>();
        //Transform[] all_locations =  locations.GetComponentsInChildren<Transform>();
        //all_items = new GameObject[all_locations.Length];
        int spawned = 0;
        while(ingredients_locations.Count != 0 && spawned < num_models) {
            GameObject item = Instantiate(model,ingredients_locations[spawned].position,Quaternion.identity);
            item.SetActive(false);
            all_items.Add(item);
            ingredients_locations.RemoveAt(spawned);
            spawned++;

           
        }
        for (int i =0;i<all_items.Count;i++){
            if(i<occupied_num+1){
                all_items[i].SetActive(true);
                occupied_locations.Add(all_items[i]);
            }else{
                free_locations.Add(all_items[i]);
            }
        }
    }


}