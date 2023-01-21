using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    private string backgroundMusic;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //To get your sound to play call it like this  FindObjectOfType<AudioManager>().Play("sound string name");
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        
        if(s.uninterruptable == false)
        {
            s.source.Play();        
        }

        if (s.uninterruptable == true && s.canPlayOverSelf == true)
        {
            s.source.PlayOneShot(s.clip, s.source.volume);
        }

        if (s.source.isPlaying) 
        {
            return;
        }

        else
        {
            s.source.PlayOneShot(s.clip, s.source.volume);
        }
    }


    //To stop music: just call it like this FindObjectOfType<AudioManager>().StopPlaying("sound string name");
    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop();
    }

    public void ButtonClick()
    {
        Play("ButtonHighlighted");
    }

    public void ButtonSelected()
    {
        Play("ButtonSelected");
    }

    //Used to reduce background music when pausing game/whilst on menus
    public void reduceMusicVolume()
    {
        foreach (Sound s in sounds)
        {
            if (s.backgroundMusic == true)
            {
                s.source.volume = 0.2f;
            }
        }
    }

    //Used to increase background music when unpausing game/coming out of menus
    public void increaseMusicVolume()
    {
        foreach (Sound s in sounds)
        {
            if (s.backgroundMusic == true)
            {
                s.source.volume = 0.5f;
            }
        }
    }

    //stop playing all background music
    public void stopPlayingAllMusic()
    {
        foreach (Sound s in sounds)
        {
            if (s.backgroundMusic == true)
            {
                StopPlaying(s.name);
            }
        }
    }
}
