using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private GameObject weaponUI;

    private RageMeter rageMeter;

    private GameObject gernadeUI;
    private Player player;
    private GameObject equipmentContainer;

    private bool isLastAddedRight;

    private int rightAddedCount;

    private int leftAddedCount;

    private List<WeaponUI> rightWeapons = new List<WeaponUI>();

    private List<WeaponUI> leftWeapons = new List<WeaponUI>();

    private WeaponUI currentlySelectedWeapon;

    // private GrenadeUI currentSelectedGrenadeUI;

    private Gernade currentHeldGernade;
    private GrenadeUI currentSelectedGernadeUI;

    private string currentHeldGernadeType;


    private bool isSelectedWeaponRight;

    private GameObject healthBar;

    private Color textGreen = new Color(0.08627450980392157f, 0.5098039215686274f, .058823529411764705f, 1f);

    private Color healthGreen = new Color(0.147f, 0.566f, 0.142f, 1.000f);

    private Color orange = new Color(255, 126, 0, 255);


    private Color red = new Color(134, 0, 0, 255);

    private AnimatedBar animatedHealthBar;
    private AnimatedBar animatedPowerBar;

    private GameObject levelInfo;
    private List<Gernade> gernades = new List<Gernade>();

    private Weapon companionWeapon;
    private RawImage companionImage;
    private TextMeshProUGUI companionCurrentAmmo;
    private TextMeshProUGUI companionName;

    public static IDictionary<string, List<Gernade>> all_gernades = new Dictionary<string, List<Gernade>>(){
        {"molotov", new List<Gernade>()},
        {"pipe",new List<Gernade>()},
        {"stun",new List<Gernade>()}
    };
    private List<GrenadeUI> gernadeUIs = new List<GrenadeUI>();






    // Start is called before the first frame update


    void Awake()
    {
        rageMeter = gameObject.AddComponent<RageMeter>();
        rageMeter.SetRageBar(GameObject.Find(HUDConstants.RAGE_BAR));
        weaponUI = Resources.Load(HUDConstants.WEAPON_UI_PATH) as GameObject;
        gernadeUI = Resources.Load(HUDConstants.GERNADE_UI_PATH) as GameObject;
        equipmentContainer = GameObject.Find(HUDConstants.EQUIPMENT_CONTAINER);
        TextMeshProUGUI health = GameObject.Find(HUDConstants.HEALTH).GetComponent<TextMeshProUGUI>();
        healthBar = GameObject.Find(HUDConstants.HEALTH_BAR);
        levelInfo = GameObject.Find("LevelInfo");
        GameObject powerBar = GameObject.Find(HUDConstants.POWER_BAR);
        healthBar.AddComponent<AnimatedBar>();
        animatedPowerBar = powerBar.AddComponent<AnimatedBar>();
        animatedHealthBar = healthBar.GetComponent<AnimatedBar>();
        animatedPowerBar.SetSwitchColor(true);
        animatedHealthBar.Initialize(
            health,
            new Color[] { healthGreen, orange, red },
            new Color[] { textGreen, orange, red },
            2f,
            0.7f,
            100
        );
        animatedPowerBar.Initialize(2f, 0.7f, 0);
        //healthBarImage = healthBar.GetComponent<Image>();
        isLastAddedRight = false;
        rightAddedCount = 0;
        leftAddedCount = 0;

    }
    // private Weapon companionWeapon;
    // private RawImage companionImage;
    // private TextMeshProUGUI companionCurrentAmmo;
    // private TextMeshProUGUI companionName;

    public void InitializeCompanion(string type, Weapon weapon)
    {
        Texture2D img = Resources.Load($"HUD Icons/{type}") as Texture2D;
        TextMeshProUGUI name = GameObject.Find("/HUD/CompanionPanel/AmmoAndNamePanel/Name/Text (TMP)").GetComponent<TextMeshProUGUI>();
        this.companionWeapon = weapon;
        companionCurrentAmmo = GameObject.Find("/HUD/CompanionPanel/AmmoAndNamePanel/AmmoBar/Text (TMP)").GetComponent<TextMeshProUGUI>();
        companionCurrentAmmo.text = $"{weapon.GetCurrentAmmo()}";
        RawImage rawImage = GameObject.Find("/HUD/CompanionPanel/PlayerPortrait/").GetComponent<RawImage>();
        rawImage.texture = img;
        //Debug.Log(name + "OKK????");
        name.text = type;


    }



    public void SetPlayer(Player mainPlayer)
    {
        this.player = mainPlayer;
    }

    public Weapon SwitchWeapon()
    {
        int currentIndex = currentlySelectedWeapon.GetIndex();
        if (isSelectedWeaponRight)
        {
            if (currentIndex < rightWeapons.Count - 1)
            {
                currentlySelectedWeapon.SetIsSelected(false);
                rightWeapons[currentIndex + 1].SetIsSelected(true);
                isSelectedWeaponRight = true;
                this.currentlySelectedWeapon = rightWeapons[currentIndex + 1];
                return currentlySelectedWeapon.GetWeapon();
            }
            if (leftWeapons.Count > 0)
            {
                currentlySelectedWeapon.SetIsSelected(false);
                leftWeapons[leftWeapons.Count - 1].SetIsSelected(true);
                isSelectedWeaponRight = false;
                this.currentlySelectedWeapon = leftWeapons[leftWeapons.Count - 1];
                return currentlySelectedWeapon.GetWeapon();
            }
            return currentlySelectedWeapon.GetWeapon();

        }


        if (leftWeapons.Count > 0 && currentIndex >= 1)
        {
            currentlySelectedWeapon.SetIsSelected(false);
            leftWeapons[currentIndex - 1].SetIsSelected(true);
            isSelectedWeaponRight = false;
            this.currentlySelectedWeapon = leftWeapons[currentIndex - 1];
            return currentlySelectedWeapon.GetWeapon();
        }

        currentlySelectedWeapon.SetIsSelected(false);
        rightWeapons[0].SetIsSelected(true);
        isSelectedWeaponRight = true;
        this.currentlySelectedWeapon = rightWeapons[0];
        return currentlySelectedWeapon.GetWeapon();

    }
    public void SetCurrentLevel(int level)
    {
        levelInfo.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Level " + level.ToString();
    }
    public void SetCurrentObjective(string objective)
    {
        levelInfo.GetComponentsInChildren<TextMeshProUGUI>()[1].text = objective;
    }
    public void SetExtraObjective(string extra)
    {
        levelInfo.GetComponentsInChildren<TextMeshProUGUI>()[2].text = extra;
    }
    public bool CheckAllEmptyGrenades()
    {
        foreach (var g in all_gernades)
        {
            if (g.Value.Count > 0)
            {
                return false;
            }
        }
        return true;
    }
    private int GetGrenadeCount(string type)
    {
        return all_gernades[type].Count;
    }
    public Gernade SwitchGrenade()
    {
        int currentIndex = currentSelectedGernadeUI.GetIndex();
        int new_index = (currentIndex + 1) % gernadeUIs.Count;
        string currentType = gernadeUIs[new_index].GetGernadeType();
        if (!CheckAllEmptyGrenades())
        {
            while (all_gernades[currentType].Count == 0)
            {
                new_index = (new_index + 1) % gernadeUIs.Count;
                currentType = gernadeUIs[new_index].GetGernadeType();
            }

            UpdateCurrentGrenade(all_gernades[currentType][0], currentType);
            return all_gernades[currentType][0];
        }
        else
        {
            UpdateCurrentGrenade(null, null);
            UnSelectAllGrenades();
            return null;
        }


    }
    public void RemoveCurrentGernade()
    {
        all_gernades[currentHeldGernadeType].Remove(currentHeldGernade);
        if (all_gernades[currentHeldGernadeType].Count == 0)
        {
            SwitchGrenade();
        }
        else
        {
            UpdateCurrentGrenade(all_gernades[currentHeldGernadeType][0], currentHeldGernadeType);
        }
        UpdateGrenadeUICounts();
    }
    public void UpdateGrenadeUICounts()
    {

        foreach (GrenadeUI g in gernadeUIs)
        {
            g.SetCurrentCount(all_gernades[g.GetGernadeType()].Count);
        }

    }
    public void AddAllGernades()
    {
        int[] positions = new int[] { HUDConstants.CENTER_SCREEN, HUDConstants.WEAPON_UI_SPACING, -HUDConstants.WEAPON_UI_SPACING };
        for (int i = 0; i < positions.Length; i++)
        {
            int position = positions[i];
            GameObject grenadeUIInstance = Instantiate(gernadeUI, gernadeUI.transform.position, Quaternion.identity);
            grenadeUIInstance.AddComponent<GrenadeUI>();
            RectTransform rectTransform = grenadeUIInstance.GetComponent<RectTransform>();
            grenadeUIInstance.transform.SetParent(equipmentContainer.transform, true);
            rectTransform.anchoredPosition = new Vector3(gernadeUI.transform.position.x, position, 0);
            rectTransform.localScale = new Vector3(1, 1, 1);
            grenadeUIInstance.GetComponent<GrenadeUI>().Initialize(WeaponsConstants.GRENADE_TYPES[i], i);
            gernadeUIs.Add(grenadeUIInstance.GetComponent<GrenadeUI>());
        }
    }
    public void AddWeapon(Weapon weapon, bool isSelected)
    {
        if (rightWeapons.Count + leftWeapons.Count == HUDConstants.MAX_WEAPONS)
            return;
        int position = !isLastAddedRight ? HUDConstants.CENTER_SCREEN + (rightAddedCount * HUDConstants.WEAPON_UI_SPACING) :
                            HUDConstants.CENTER_SCREEN - (leftAddedCount * HUDConstants.WEAPON_UI_SPACING);
        GameObject weaponUIInstance = Instantiate(weaponUI, weaponUI.transform.position, Quaternion.identity);
        weaponUIInstance.AddComponent<WeaponUI>();
        RectTransform rectTransform = weaponUIInstance.GetComponent<RectTransform>();
        weaponUIInstance.transform.SetParent(equipmentContainer.transform, true);
        rectTransform.anchoredPosition = new Vector3(0, position, 0);
        rectTransform.localScale = new Vector3(1, 1, 1);
        int index = this.isLastAddedRight ? leftWeapons.Count : rightWeapons.Count;
        weaponUIInstance.GetComponent<WeaponUI>().Initialize(weapon, isSelected, index);
        WeaponUI script = weaponUIInstance.GetComponent<WeaponUI>();
        if (isSelected)
        {
            currentlySelectedWeapon = script;
            isSelectedWeaponRight = !this.isLastAddedRight;
            leftAddedCount += 1;
        }

        if (!isLastAddedRight)
        {
            rightAddedCount += 1;
            rightWeapons.Add(script);
        }
        else
        {
            leftAddedCount += 1;
            leftWeapons.Add(script);
        }
        this.isLastAddedRight = !isLastAddedRight;

    }

    public void ChangeHealth(int health)
    {
        animatedHealthBar.Change(health);
    }

    public int GetHealth()
    {
        return 0;
        //return currentHealth;
    }
    public void SelectGrenadeUI(string type)
    {
        foreach (var g in gernadeUIs)
        {
            if (g.GetGernadeType().Equals(type))
            {
                g.SetIsSelected(true);
                currentSelectedGernadeUI = g;

            }
            else
            {
                g.SetIsSelected(false);
            }
        }
    }
    public void UnSelectAllGrenades()
    {
        foreach (var g in gernadeUIs)
        {
            g.SetIsSelected(false);
        }

    }
    public void UpdateCurrentGrenade(Gernade gernade, string type)
    {
        SelectGrenadeUI(type);
        player.SetGrenade(gernade);
        currentHeldGernade = gernade;
        currentHeldGernadeType = type;
    }
    public bool CollectGernade(Gernade gernade)
    {
        Debug.Log("Added Gernade");
        string type = gernade.GetGernadeType();
        if (ExceededMax(gernade, all_gernades[type].Count))
            return false;


        if (CheckAllEmptyGrenades())
        {
            UpdateCurrentGrenade(gernade, type);
            Debug.Log("Set Player Grenade Initially");
        }
        all_gernades[type].Add(gernade);
        UpdateGrenadeUICounts();

        return true;
    }
    public bool ExceededMax(Gernade gernade, int count)
    {
        if (gernade.GetMaxCapacity() < count)
        {
            return true;
        }
        return false;
    }

    public void ChangeRage(int amount)
    {
        rageMeter.ChangeRage(amount);
    }

    public bool ActivateRage()
    {
        return rageMeter.ActivateRage();
    }

    public void ChangePowerBar(int amount)
    {
        animatedPowerBar.Change(amount);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (companionWeapon)
        {
            companionCurrentAmmo.text = $"{companionWeapon.GetCurrentAmmo()}";
        }
    }
}
