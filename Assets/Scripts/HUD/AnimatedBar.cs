using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AnimatedBar : MonoBehaviour
{   
    private bool switchColors;
    private TextMeshProUGUI textReference;

    private Color[] barColors;

    private Color[] textColors;
    private float step;

    private float waitTime;

    private int target;

    private int currentPercentage;

    private float updateAmount;
    private Image image;

     private Color green = new Color(0.147f, 0.566f, 0.142f, 1.000f);

    private Color orange = new Color(255, 126, 0, 255);


    private Color red = new Color(134, 0, 0, 255);

    private Color[] defaultColors;

    public int offset = 1;

    private bool reachedTarget;

    public bool GetReachedTarget() {
        return reachedTarget && target !=0;
    }



    void Awake() {
        this.defaultColors =  new Color[] { red,orange,green};
    }
    public void Initialize(TextMeshProUGUI textReference, Color[] barColors, Color[] textColors, float waitTime, float step, int startingPercentage,int offset) {
        this.switchColors = true;
        this.textReference = textReference;
        this.barColors = barColors;
        this.textColors = textColors;
        image = this.gameObject.GetComponent<Image>();
        this.currentPercentage = startingPercentage * offset;
        this.target = currentPercentage;
        if(this.textReference)textReference.text = $"+{target}";
        this.waitTime = waitTime;
        this.step = step;
        this.offset = offset;
    }

    public void Initialize( float waitTime, float step, int startingPercentage) {
        this.waitTime = waitTime;
        this.step = step;
        this.currentPercentage = startingPercentage;
        this.target = currentPercentage;
        image = this.gameObject.GetComponent<Image>();
        image.fillAmount = startingPercentage/100;
        barColors = defaultColors;
    }

    public void Change(int amount) {
        int max = 100 * offset;
        target += amount;
        if(target<0) target = 0;
        if(target>max) target = max;
        if(textReference) textReference.text = $"+{target}";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void HandleColor() {
        float currentFill = image.fillAmount;
        if(currentFill > 0.6) {
                image.color = barColors[0];
                if(textReference)
                textReference.color = textColors[0];
            }
            if(currentFill<=0.6 && currentFill >= 0.3) {
                image.color = barColors[1];
                if(textReference)
                textReference.color = textColors[1];
            }
            if(currentFill<0.3) {
               image.color = barColors[2];
               if(textReference)
               textReference.color = textColors[2];
            }
        }

    public int GetPercentage() {
        return currentPercentage;
    }

    public void SetSwitchColor(bool switchColors) {
        this.switchColors = switchColors;
    }
    

    // Update is called once per frame
    void Update()
        {
        if(currentPercentage == target || Math.Abs(currentPercentage-target) < 0.5) {
            if(!reachedTarget)
                reachedTarget=true;
            return;
        }

        updateAmount = step/waitTime * Time.deltaTime;
        if(currentPercentage < target) image.fillAmount += updateAmount;
        if(currentPercentage > target) image.fillAmount -= updateAmount;
        currentPercentage = (int) ((image.fillAmount * 100)*offset);
        if(switchColors) HandleColor();
        
    }
}
