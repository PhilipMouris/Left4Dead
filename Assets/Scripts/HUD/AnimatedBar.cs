using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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



    public void Initialize(TextMeshProUGUI textReference, Color[] barColors, Color[] textColors, float waitTime, float step, int startingPercentage) {
        this.switchColors = true;
        this.textReference = textReference;
        this.barColors = barColors;
        this.textColors = textColors;
        image = this.gameObject.GetComponent<Image>();
        this.currentPercentage = startingPercentage;
        this.target = currentPercentage;
        this.waitTime = waitTime;
        this.step = step;
    }

    public void Initialize( float waitTime, float step, int startingPercentage) {
        this.waitTime = waitTime;
        this.step = step;
        this.currentPercentage = startingPercentage;
        this.target = currentPercentage;
        image = this.gameObject.GetComponent<Image>();
    }

    public void Change(int amount) {
        target += amount;
        if(target<0) target = 0;
        if(target>100) target = 100;
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
                textReference.color = textColors[0];
            }
            if(currentFill<=0.6 && currentFill >= 0.3) {
                image.color = barColors[1];
                textReference.color = textColors[1];
            }
            if(currentFill<0.3) {
               image.color = barColors[2];
               textReference.color = textColors[2];
            }
        }

    public int GetPercentage() {
        return currentPercentage;
    }

    

    // Update is called once per frame
    void Update()
        {
        if(currentPercentage == target || currentPercentage-target < 0.5) return;
        updateAmount = step/waitTime * Time.deltaTime;
        if(currentPercentage < target) image.fillAmount += updateAmount;
        if(currentPercentage > target) image.fillAmount -= updateAmount;
        currentPercentage = (int) (image.fillAmount * 100);
        if(switchColors) HandleColor();
        
    }
}
