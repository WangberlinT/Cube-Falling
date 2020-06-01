using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
            AudioSource source = s.GetSource();
            source.clip = s.clip;
            source.pitch = s.pitch;
            source.volume = s.volume;
        }
    }

    public void Play(string name)
    {
        Sound playSound = null;
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                playSound = s;
                break;
            }
        }
        if (playSound != null)
            playSound.GetSource().Play();
        else
            Debug.LogError("Not this sound");

    }
}
