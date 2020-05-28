using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_Sound : MonoBehaviour
{
    private Slider slider;//Slider 对象
    private Text text;//Text 对象
    public AudioSource audioSource;
    void Start()
    {
        slider = GameObject.Find("Volume Slider").GetComponent<Slider>();
        text = slider.transform.Find("Volume_Value").GetComponent<Text>();
        slider.value = 0.5f;
        text.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ((int)(slider.value/1*100)).ToString();
        audioSource.volume = slider.value;
        Global.VOLUME = slider.value;
    }
}

public class Global
{
    public static float  VOLUME = 0.5f;
}
