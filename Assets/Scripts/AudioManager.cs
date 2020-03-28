using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop = false;
    [Range(0,1)]
    public float volume = 1;
    [Range(-3, 3)]
    public float pitch = 1;
    private AudioSource source;

    public void setSource(AudioSource targetSource)
    {
        source = targetSource;
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
        source.pitch = pitch;
    }

    public void play()
    {
        source.Play();
    }

    public void stop()
    {
        source.Stop();
    }

    public bool isPlaying()
    {
        return source.isPlaying;
    }
}

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] soundList;

    public static AudioManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in soundList)
        {
            GameObject temp = new GameObject("AudioSource_" + sound.name);
            temp.transform.SetParent(transform);
            sound.setSource(temp.AddComponent<AudioSource>());
        }
    }

    public void play(string name)
    {
        Sound selected = Array.Find(soundList, sound => sound.name == name);
        if (selected != null)
        {
            selected.play();
        }
        else
        {
            Debug.LogError("sound with name " + name + " not found");
        }
    }

    public void stopMainTheme()
    {
        Sound selected = Array.Find(soundList, sound => sound.name == "MainTheme");
        selected.stop();
    }

    public void playMainTheme()
    {
        Sound selected = Array.Find(soundList, sound => sound.name == "MainTheme");
        if (!selected.isPlaying())
        {
            selected.play();
        }
    }
}
