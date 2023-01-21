using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    //what to name audio clip (will be referenced in play method in AudioManager)
    public string name;

    //Section in inspector where audio clip gets dragged
    public AudioClip clip;

    //Range of volume
    [Range(0f, 1f)]
    public float volume;

    //Variance of volume
    [Range(0f, 1f)]
    public float volumeVariance = .1f;

    //Range of pitch
    [Range(0.1f, 3f)]
    public float pitch = 1;

    //Variance of pitch.
    [Range(0f, 1f)]
    public float pitchVariance = .1f;

    //Loop sound
    public bool loop;

    [HideInInspector]
    public AudioSource source;

    //Whether to play whole clip uninterrupted
    public bool uninterruptable;

    //Whether the clip can be play more than once at the same time.
    public bool canPlayOverSelf;

    public bool backgroundMusic;
}