using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MuteButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button muteButton;
    private TextMeshProUGUI textMesh;
    private SoundManager soundManager;
    void Awake()
    {
        this.muteButton = GameObject.Find("Mute").GetComponent<Button>();
        this.textMesh = this.muteButton.GetComponentInChildren<TextMeshProUGUI>();
        this.muteButton.onClick.AddListener(onMute);
        this.soundManager = GameObject.Find("AudioManager").GetComponent<SoundManager>();

    }
    // Update is called once per frame


    private void onMute()
    {

        bool isMuted = this.soundManager.HandleMute();
        if (!isMuted)
        {
            this.soundManager.PlayButtonClick();
        }
        textMesh.text = isMuted ? "Unmute" : "Mute";
    }


}
