using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    
    
    void Awake(){
        weaponUI = Resources.Load(HUDConstants.WEAPON_UI_PATH) as GameObject;
        Debug.Log(weaponUI.name + " NAME");
        equipmentContainer = GameObject.Find(HUDConstants.EQUIPMENT_CONTAINER);
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
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
