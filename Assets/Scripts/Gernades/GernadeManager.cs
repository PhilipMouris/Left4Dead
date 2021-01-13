using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GernadeManager : MonoBehaviour
{
    public GameObject molotov;
    public GameObject stun;
    public GameObject pipe;
    public GameObject locations;
    private GameObject player;
    public AudioSource source;
    private GameObject[] all_items;
    private List<GameObject> occupied_locations ;
    private List<GameObject> free_locations;

    private int occupied_num = 3;
    // Start is called before the first frame update

    void Awake() {
         player = GameObject.Find(EngineConstants.PLAYER);
         Debug.Log(player.name + " NAMEEE");
    }
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
   
    public GameObject GetMolotovPrefab(){
        return molotov;
    }
    public GameObject GetStunPrefab(){
        return stun;
    }
    public GameObject GetPipePrefab(){
        return pipe;
    }

    public void Spawn(){
        occupied_locations = new List<GameObject>();
        free_locations = new List<GameObject>();
        Transform[] all_locations =  locations.GetComponentsInChildren<Transform>();
        // Debug.Log(all_locations.Length + " Locations");
        all_items = new GameObject[all_locations.Length-1];
        for (int i =1;i<all_locations.Length;i++){
            GameObject item;
            if(i<4){
                item = Instantiate(molotov,all_locations[i].position + new Vector3(0f,0.3f,0f),Quaternion.identity);
            }else if(i<7){
                item = Instantiate(stun,all_locations[i].position + new Vector3(0f,0.3f,0f),Quaternion.identity);
            }else{
                item = Instantiate(pipe,all_locations[i].position + new Vector3(0f,0.3f,0f),Quaternion.identity);
            }
           
            item.SetActive(false);
            all_items[i-1] = item;
            
        }
        for (int i =0;i<all_items.Length;i++){
            if(i==0 || i==5 || i==9){
                all_items[i].SetActive(true);
                occupied_locations.Add(all_items[i]);
            }else{
                free_locations.Add(all_items[i]);
            }
        }
    }
    public Gernade InitializeGrenade(string type){
        switch(type){
            case "Molotov": return new MolotovCocktail();
            case "Pipe": return new PipeBomb();
            case "Stun": return new StunGernade();
        }
        return null;
    }


}
