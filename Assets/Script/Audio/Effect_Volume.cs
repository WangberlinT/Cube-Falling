using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Volume : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource.volume = Global.EFFECT_VOLUME;
    }
}
