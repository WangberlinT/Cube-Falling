using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;

    private AudioSource source;

    public void SetSource(AudioSource s)
    {
        source = s;
    }

    public AudioSource GetSource()
    {
        return source;
    }

}
