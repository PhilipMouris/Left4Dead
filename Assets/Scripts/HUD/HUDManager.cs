using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{   
    private GameObject weaponUI;
    private GameObject equipmentContainer;

    private bool isLastAddedRight;

    private int rightAddedCount;

    private int leftAddedCount;

    private List<WeaponUI> rightWeapons = new List<WeaponUI>();

    private List<WeaponUI> leftWeapons = new List<WeaponUI>();

    private WeaponUI currentlySelectedWeapon;

    private bool isSelectedWeaponRight;

    private GameObject healthBar;

    private Color textGreen = new Color(0.08627450980392157f,0.5098039215686274f,.058823529411764705f,1f);

    private Color healthGreen = new Color(0.147f, 0.566f, 0.142f, 1.000f);

    private Color orange = new Color(255,126,0,255);


    private Color red = new Color (134, 0, 0,255);

    private  AnimatedBar animatedHealthBar;


    
    void Awake(){
        weaponUI = Resources.Load(HUDConstants.WEAPON_UI_PATH) as GameObject;
        equipmentContainer = GameObject.Find(HUDConstants.EQUIPMENT_CONTAINER);
        TextMeshProUGUI health = GameObject.Find(HUDConstants.HEALTH).GetComponent<TextMeshProUGUI>();
        healthBar = GameObject.Find(HUDConstants.HEALTH_BAR);
        healthBar.AddComponent<AnimatedBar>();
        animatedHealthBar = healthBar.GetComponent<AnimatedBar>();
        animatedHealthBar.Initialize(
            health,
            new Color[] {healthGreen,orange,red},
            new Color[] {textGreen,orange,red},
            2f,
            0.7f,
            100
        );
        //healthBarImage = healthBar.GetComponent<Image>();
        isLastAddedRight = false;
        rightAddedCount = 0;
        leftAddedCount = 0;

    }

    public Weapon SwitchWeapon(){
        int currentIndex = currentlySelectedWeapon.GetIndex();
        if(isSelectedWeaponRight){
            if(currentIndex < rightWeapons.Count -1){
                currentlySelectedWeapon.SetIsSelected(false);
                rightWeapons[currentIndex + 1].SetIsSelected(true);
                isSelectedWeaponRight = true;
                this.currentlySelectedWeapon = rightWeapons[currentIndex+1];
                return currentlySelectedWeapon.GetWeapon();
            }
            if(leftWeapons.Count > 0){
                currentlySelectedWeapon.SetIsSelected(false);
                leftWeapons[leftWeapons.Count - 1].SetIsSelected(true);
                isSelectedWeaponRight = false;
                this.currentlySelectedWeapon = leftWeapons[leftWeapons.Count - 1];
                return currentlySelectedWeapon.GetWeapon();
            }
            return currentlySelectedWeapon.GetWeapon();

           }
        

        if(leftWeapons.Count > 0 && currentIndex >= 1){
            currentlySelectedWeapon.SetIsSelected(false);
            leftWeapons[currentIndex -1].SetIsSelected(true);
            isSelectedWeaponRight = false;
            this.currentlySelectedWeapon = leftWeapons[currentIndex -1 ];
            return currentlySelectedWeapon.GetWeapon();
        }

        currentlySelectedWeapon.SetIsSelected(false);
        rightWeapons[0].SetIsSelected(true);
        isSelectedWeaponRight = true;
        this.currentlySelectedWeapon = rightWeapons[0];
        return currentlySelectedWeapon.GetWeapon();

    }
    
    
    public void AddWeapon(Weapon weapon, bool isSelected) {
        if(rightWeapons.Count + leftWeapons.Count == HUDConstants.MAX_WEAPONS)
            return;
        int position = !isLastAddedRight ? HUDConstants.CENTER_SCREEN + (rightAddedCount * HUDConstants.WEAPON_UI_SPACING):
                            HUDConstants.CENTER_SCREEN - (leftAddedCount * HUDConstants.WEAPON_UI_SPACING);
        GameObject weaponUIInstance = Instantiate(weaponUI, weaponUI.transform.position, Quaternion.identity);
        weaponUIInstance.AddComponent<WeaponUI>();
        RectTransform rectTransform = weaponUIInstance.GetComponent<RectTransform>();
        weaponUIInstance.transform.SetParent(equipmentContainer.transform,true);
        rectTransform.anchoredPosition = new Vector3(0, position, 0);
        rectTransform.localScale = new Vector3(1,1,1);
        int index = this.isLastAddedRight? leftWeapons.Count : rightWeapons.Count ;
        weaponUIInstance.GetComponent<WeaponUI>().Initialize(weapon, isSelected, index);
        WeaponUI script =  weaponUIInstance.GetComponent<WeaponUI>();
        if(isSelected) {
            currentlySelectedWeapon = script;
            isSelectedWeaponRight =  !this.isLastAddedRight;
            leftAddedCount += 1;
        }

        if(!isLastAddedRight){
            rightAddedCount +=1;
            rightWeapons.Add(script);
        }
        else {
            leftAddedCount += 1;
            leftWeapons.Add(script);
        }
        this.isLastAddedRight = !isLastAddedRight;

    }

    public void ChangeHealth(int health) {
        animatedHealthBar.Change(health);
    }

    public int GetHealth()
    {   
        return 0;
        //return currentHealth;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
      //HandleHealthBar();
    }
}
