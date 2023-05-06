using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(.5f, 3f)]
    public float pitch = 1;

    [HideInInspector]
    public AudioSource source;

}
