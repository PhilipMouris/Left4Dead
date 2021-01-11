using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{
    Ingredients collected_ingredient;
    private int alcohol = 0, gunpowder = 0, canister = 0, cloth = 0, sugar = 0, 
                molotov = 0, stun_grenade = 0, pipe_bomb = 0, health_pack = 0;
    private GameObject AlcoholObject;
    private GameObject GunpowderObject;
    private GameObject CanisterObject;
    private GameObject ClothObject;
    private GameObject SugarObject;
    private GameObject MolotovObject;
    private GameObject StunObject;
    private GameObject PipeObject;
    private GameObject HealthPackObject;
    private GameObject CraftingCanvas;

    public GameObject button;
        

    //public Color SelectableColor;
    // Start is called before the first frame update
    void Awake(){
        CraftingCanvas = GameObject.Find("CraftingCanvas");
        AlcoholObject = GameObject.Find("AlcoholImage");
        GunpowderObject = GameObject.Find("GunpowderImage");
        CanisterObject =  GameObject.Find("CanisterImage");
        ClothObject =  GameObject.Find("ClothImage");
        SugarObject =  GameObject.Find("SugarImage");
        MolotovObject = GameObject.Find("MolotovImage");
        StunObject  = GameObject.Find("StunImage");
        PipeObject = GameObject.Find("PipeImage");
        HealthPackObject = GameObject.Find("HealthPackImage");
    }
    void Start()
    {
        
        

        
    }

    private void incrementItems() {
        if(CraftingCanvas != null) {
            AlcoholObject.GetComponentInChildren<Text>().text = alcohol.ToString();
            GunpowderObject.GetComponentInChildren<Text>().text = gunpowder.ToString();
            CanisterObject.GetComponentInChildren<Text>().text = canister.ToString();
            ClothObject.GetComponentInChildren<Text>().text = cloth.ToString();
            SugarObject.GetComponentInChildren<Text>().text = sugar.ToString();
            MolotovObject.GetComponentInChildren<Text>().text = molotov.ToString();
            StunObject.GetComponentInChildren<Text>().text = stun_grenade.ToString();
            PipeObject.GetComponentInChildren<Text>().text = pipe_bomb.ToString();
            HealthPackObject.GetComponentInChildren<Text>().text = health_pack.ToString();
        }
    }

    private IEnumerator ShowError() {
        //yield WaitForSeconds(5);
        button.SetActive(true); // Enable the text so it shows
        yield return new WaitForSecondsRealtime(1);
        button.SetActive(false); // Disable the text so it is hidden
    }

    // Update is called once per frame
    void Update()
    {
        incrementItems();
    }

    public void AddIngredient(GameObject ingredient)
    {
        string tag = ingredient.tag;
         
        Debug.Log(tag + " TAG");
        switch (tag)
        {
            case "alcohol":
                alcohol++;
                //Debug.Log(alcohol);
                //GameObject.Find("AlcoholImage").GetComponentInChildren<Text>().text = alcohol.ToString();
                break;

            case "canister":
                canister++;
                //GameObject.Find("CanisterImage").GetComponentInChildren<Text>().text = canister.ToString();
                break;

            case "gunpowder":
                gunpowder++;
                //GameObject.Find("GunpowderImage").GetComponentInChildren<Text>().text = gunpowder.ToString();
                break;

            case "cloth":
                cloth++;
                //GameObject.Find("ClothImage").GetComponentInChildren<Text>().text = cloth.ToString();
                break;

            case "sugar":
                sugar++;
                //GameObject.Find("SugarImage").GetComponentInChildren<Text>().text = sugar.ToString();
                break;

            default:
                break;

        }
        

    }


    public void CraftMolotov()
    {
        Debug.Log("Molotov molotov");
        if (alcohol >= 2)// && cloth >= 2)
        {
            GameObject.Find("FPSController 1").GetComponent<Player>().CraftGrenade(new MolotovCocktail());
            molotov++;
            alcohol -= 2;
            //cloth -= 2;
            GameObject.Find("MolotovImage").GetComponentInChildren<Text>().text = molotov.ToString();
            GameObject.Find("AlcoholImage").GetComponentInChildren<Text>().text = alcohol.ToString();
            GameObject.Find("ClothImage").GetComponentInChildren<Text>().text = cloth.ToString();

        }

        else {
             
            StartCoroutine("ShowError");
        }


    }

    public void CraftStunGrenade()
    {
        if (sugar >= 1 && gunpowder >= 2)
        {
            GameObject.Find("FPSController 1").GetComponent<Player>().CraftGrenade(new StunGernade());
            sugar--;
            gunpowder -= 2;
            GameObject.Find("StunImage").GetComponentInChildren<Text>().text = stun_grenade.ToString();
            GameObject.Find("SugarImage").GetComponentInChildren<Text>().text = sugar.ToString();
            GameObject.Find("GunpowderImage").GetComponentInChildren<Text>().text = gunpowder.ToString();
        }
        else {
            StartCoroutine(ShowError());
        }
    }
    public void CraftPipeBomb()
    {
        if (alcohol >= 1 && gunpowder >= 1 && canister >= 1)
        {
            GameObject.Find("FPSController 1").GetComponent<Player>().CraftGrenade(new PipeBomb());
            alcohol--;
            gunpowder--;
            canister--;
            GameObject.Find("PipeImage").GetComponentInChildren<Text>().text = pipe_bomb.ToString();
            GameObject.Find("AlcoholImage").GetComponentInChildren<Text>().text = alcohol.ToString();
            GameObject.Find("GunpowderImage").GetComponentInChildren<Text>().text = gunpowder.ToString();
            GameObject.Find("CanisterImage").GetComponentInChildren<Text>().text = canister.ToString();
        }
        else {
            StartCoroutine(ShowError());
        }
    }
    public void CraftHealthPack()
    {
        if (alcohol >= 2 && cloth >= 2)
        {
            health_pack++;
            alcohol -= 2;
            cloth -= 2;
            GameObject.Find("HealthPackImage").GetComponentInChildren<Text>().text = health_pack.ToString();
            GameObject.Find("AlcoholImage").GetComponentInChildren<Text>().text = alcohol.ToString();
            GameObject.Find("ClothImage").GetComponentInChildren<Text>().text = cloth.ToString();
        }
        else {
            StartCoroutine(ShowError());
        }
    }

    
}
