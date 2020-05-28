using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Control_Sound : MonoBehaviour
{
    private Slider BGM_slider;
    private Slider Effect_slider;
    public AudioSource audioSource;
    public TextMeshProUGUI BGM_text;
    public TextMeshProUGUI Effect_text;
    void Start()
    {
        BGM_slider = GameObject.Find("BGM Volume Slider").GetComponent<Slider>();
        BGM_slider.value = 0.5f;
        Effect_slider = GameObject.Find("Effect Volume Slider").GetComponent<Slider>();
        Effect_slider.value = 0.5f;
        BGM_text.text = BGM_slider.value.ToString();
        Effect_text.text = Effect_slider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        BGM_text.text = ((int)(BGM_slider.value/1*100)).ToString();
        audioSource.volume = BGM_slider.value;
        Global.BGM_VOLUME = BGM_slider.value;

        Effect_text.text = ((int)(Effect_slider.value/1*100)).ToString();
        Global.EFFECT_VOLUME = Effect_slider.value;
    }
}

public class Global
{
    public static float  BGM_VOLUME = 0.5f;//全局背景音量
    public static float  EFFECT_VOLUME = 0.5f;//全局效果音量
}
