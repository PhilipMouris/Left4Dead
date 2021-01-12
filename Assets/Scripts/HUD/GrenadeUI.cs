using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrenadeUI : MonoBehaviour
{
    // Start is called before the first frame update
    private string gernadeType;
    private RawImage grenadeImage;
    private TextMeshProUGUI currentCount;

   

    private Color green = new Color(0,255,0,255);

    private Color white = new Color(255,255,255,255);

    private int index;

    private Color orange = new Color(255,126,0,255);

    private Color red = new Color (134, 0, 0,255);

    //public int test;

    private bool isSelected;
    public void Initialize(string type,int index) {
        this.gernadeType = type;
        Texture displayImage = Resources.Load($"{HUDConstants.ICON_PATH}/{type}") as Texture;
        GameObject grenadeImageObject = this.gameObject.transform.GetChild(0).gameObject;
        GameObject currentCountObject = this.gameObject.transform.GetChild(1).gameObject;
        currentCount = currentCountObject.GetComponent<TextMeshProUGUI>();
        SetCurrentCount(0);
        grenadeImage = grenadeImageObject.GetComponent<RawImage>();
        grenadeImage.texture = displayImage;
      
        this.isSelected = false;
        this.index = index;

    }

    private void SetText(int amount, TextMeshProUGUI textObject ){
        if(amount<0){
            textObject.text = "";
            return;
        }
        if(amount < 9 ) {
            textObject.text = "  " + amount; 
            return;
        }
        if(amount < 100) textObject.text = " " + amount;
        else textObject.text = "" + amount;
    }


    private void SetTextColor( TextMeshProUGUI textObject, int current, int total ) {
        float ratio = (float)current/total;
        if(ratio>0.5) textObject.color = white;
        if(ratio<0.5) textObject.color = orange;
        if(ratio<0.25) textObject.color = red;
    }
    public string GetGernadeType(){
        return this.gernadeType;
    }
    public void SetCurrentCount(int count){
        SetText(count,currentCount);
        // SetTextColor(currentCount,count);
    }

    // private void SetTotalAmmo() {
    //     int ammo = grenade.GetTotalAmmo();
    //     SetText(ammo,totalAmmo);
    //     SetTextColor(totalAmmo, ammo, grenade.GetMaxAmmo());
    // }

    private void SetColor() {
        if(isSelected) {
            grenadeImage.color = green;
        }
        else {
            grenadeImage.color = white;
        }
    }

    public void SetIsSelected(bool isSelected){
        this.isSelected = isSelected;

    }

    
    public int GetIndex() {
        return index;
    } 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
        
    }
}
