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

    private GameObject molotovButton;
    private GameObject healthPackButton;
    private GameObject pipeButton;
    private GameObject stunButton;

    private GernadeManager gernadeManager;
    private HUDManager hudManager;

    //public Color SelectableColor;
    // Start is called before the first frame update
    void Awake()
    {
        // FindObjects();
       
    }
    public void FindObjects()
    {
        CraftingCanvas = GameObject.Find("CraftingCanvas");
        AlcoholObject = GameObject.Find("AlcoholImage");
        GunpowderObject = GameObject.Find("GunpowderImage");
        CanisterObject = GameObject.Find("CanisterImage");
        ClothObject = GameObject.Find("ClothImage");
        SugarObject = GameObject.Find("SugarImage");
        MolotovObject = GameObject.Find("MolotovImage");
        StunObject = GameObject.Find("StunImage");
        PipeObject = GameObject.Find("PipeImage");
        HealthPackObject = GameObject.Find("HealthPackImage");
        molotovButton = GameObject.Find("MolotovButton");
        stunButton = GameObject.Find("StunButton");
        pipeButton = GameObject.Find("PipeButton");
        healthPackButton = GameObject.Find("HealthPackButton");
        gernadeManager = GameObject.FindObjectOfType<GernadeManager>();
        Debug.Log(gernadeManager + " MANAGER");
        hudManager = GameObject.FindObjectOfType<HUDManager>();
        if (CraftingCanvas != null)
        {
            // Debug.Log("Added Molotov");
            molotovButton.GetComponent<Button>().onClick.AddListener(this.CraftMolotov);
            stunButton.GetComponent<Button>().onClick.AddListener(this.CraftStunGrenade);
            pipeButton.GetComponent<Button>().onClick.AddListener(() => this.CraftPipeBomb());
            // Debug.Log(pipeButton.GetComponent<Button>());
            healthPackButton.GetComponent<Button>().onClick.AddListener(this.CraftHealthPack);
        }
    }
    void Start()
    {


        // FindObjects();
        // gernadeManager = GameObject.FindObjectOfType<GernadeManager>();
        // Debug.Log(gernadeManager + " MANAGER");
        // hudManager = GameObject.FindObjectOfType<HUDManager>();

    }

    private void incrementItems()
    {
        if (CraftingCanvas != null)
        {
            // Debug.Log("UPDATE");
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

    private IEnumerator ShowError()
    {
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
        Debug.Log(tag);

        switch (tag)
        {
            case "alcohol":
                alcohol++;
                // AlcoholObject.GetComponentInChildren<Text>().text = alcohol.ToString();
                break;

            case "canister":
                canister++;
                // CanisterObject.GetComponentInChildren<Text>().text = canister.ToString();
                break;

            case "gunpowder":
                gunpowder++;
                // GunpowderObject.GetComponentInChildren<Text>().text = gunpowder.ToString();
                break;

            case "cloth":
                cloth++;
                // ClothObject.GetComponentInChildren<Text>().text = cloth.ToString();
                break;

            case "sugar":
                sugar++;
                // SugarObject.GetComponentInChildren<Text>().text = sugar.ToString();
                break;

            default:
                Debug.Log("NO UPDATES");
                break;

        }


    }


    public void CraftMolotov()
    {
        Debug.Log("Molotov molotov");
        if (alcohol >= 2 && cloth >= 2)
        {
            // GameObject.Find("FPSController 1").GetComponent<Player>().CraftGrenade(new MolotovCocktail());
            //GameObject.Find("Player").GetComponent<Player>().CraftGrenade(new Gernade());
            // gernadeManager = GameObject.FindObjectOfType<GernadeManager>();
            // Debug.Log(gernadeManager + " MANAGER");
            
            GameObject gernade = Instantiate(gernadeManager.GetMolotovPrefab(),transform.position,Quaternion.identity);
            gernade.SetActive(false);
            Gernade componenet = gernade.GetComponent<Gernade>();
            // Debug.Log(componenet+ " COMP");
            bool collected = hudManager.CollectGernade(componenet);
            if (collected)
            {
                molotov++;
                alcohol -= 2;
                cloth -= 2;
                MolotovObject.GetComponentInChildren<Text>().text = molotov.ToString();
                AlcoholObject.GetComponentInChildren<Text>().text = alcohol.ToString();
                ClothObject.GetComponentInChildren<Text>().text = cloth.ToString();
            }

        }

        else
        {

            StartCoroutine("ShowError");
        }


    }

    public void CraftStunGrenade()
    {
        if (sugar >= 1 && gunpowder >= 2)
        {
           
            GameObject gernade = Instantiate(gernadeManager.GetStunPrefab());
            gernade.SetActive(false);
            Gernade componenet = gernade.GetComponent<Gernade>();
            // Debug.Log(componenet+ " COMP");
            bool collected = hudManager.CollectGernade(componenet);
            if (collected)
            {
                stun_grenade++;
                sugar--;
                gunpowder -= 2;
                StunObject.GetComponentInChildren<Text>().text = stun_grenade.ToString();
                SugarObject.GetComponentInChildren<Text>().text = sugar.ToString();
                GunpowderObject.GetComponentInChildren<Text>().text = gunpowder.ToString();
            }
        }
        else
        {
            StartCoroutine(ShowError());
        }
    }
    public void CraftPipeBomb()
    {
        if (alcohol >= 1 && gunpowder >= 1 && canister >= 1)
        {
            // GameObject.Find("FPSController 1").GetComponent<Player>().CraftGrenade(new PipeBomb());
            // gernadeManager = GameObject.FindObjectOfType<GernadeManager>();
            // Debug.Log(gernadeManager + " MANAGER");
            // hudManager = GameObject.FindObjectOfType<HUDManager>();
            GameObject gernade = Instantiate(gernadeManager.GetPipePrefab());
            gernade.SetActive(false);
            Gernade componenet = gernade.GetComponent<Gernade>();
            // Debug.Log(componenet+ " COMP");
            bool collected = hudManager.CollectGernade(componenet);
            if (collected)
            {
                pipe_bomb++;
                alcohol--;
                gunpowder--;
                canister--;
                PipeObject.GetComponentInChildren<Text>().text = pipe_bomb.ToString();
                AlcoholObject.GetComponentInChildren<Text>().text = alcohol.ToString();
                GunpowderObject.GetComponentInChildren<Text>().text = gunpowder.ToString();
                CanisterObject.GetComponentInChildren<Text>().text = canister.ToString();
            }
        }
        else
        {
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
            HealthPackObject.GetComponentInChildren<Text>().text = health_pack.ToString();
            AlcoholObject.GetComponentInChildren<Text>().text = alcohol.ToString();
            ClothObject.GetComponentInChildren<Text>().text = cloth.ToString();
        }
        else
        {
            StartCoroutine(ShowError());
        }
    }


}
