using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Volume_Control : MonoBehaviour
{
    public AudioSource audioSource;
    // Update is called once per frame
    void Start()
    {
        audioSource.volume = Global.VOLUME;
    }
}
