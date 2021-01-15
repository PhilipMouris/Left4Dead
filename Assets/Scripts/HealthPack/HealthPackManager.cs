using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackManager : MonoBehaviour
{
    public GameObject model;
    public GameObject locations;
    public GameObject player;
    private GameObject[] all_items;
    private List<GameObject> occupied_locations ;
    private List<GameObject> free_locations;
    private int occupied_num = 3;
    // Start is called before the first frame update
    void Start()
    {
       Spawn();
    }
    public void UpdateLocations(GameObject item){
        
        occupied_locations.Remove(item);
        int new_item_index = Random.Range(0,free_locations.Count-1);
        GameObject new_item = free_locations[new_item_index];
        new_item.gameObject.SetActive(true);
        occupied_locations.Add(new_item);
        free_locations.RemoveAt(new_item_index);
        free_locations.Add(item);
        item.SetActive(false);
    }

    public void Spawn(){
        occupied_locations = new List<GameObject>();
        free_locations = new List<GameObject>();
        Transform[] all_locations =  locations.GetComponentsInChildren<Transform>();
        all_items = new GameObject[all_locations.Length-1];
        for (int i =1;i<all_locations.Length;i++){
            GameObject item = Instantiate(model,all_locations[i].position,Quaternion.identity);
            item.SetActive(false);
            all_items[i-1] = item;
        }
        for (int i =0;i<all_items.Length;i++){
            if(i<occupied_num+1){
                all_items[i].SetActive(true);
                occupied_locations.Add(all_items[i]);
            }else{
                free_locations.Add(all_items[i]);
            }
        }
    }


}
