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
        source.volume = volume;
        source.Play();
    }

    public void stop()
    {
        source.Stop();
    }

    public IEnumerator playWithFade()
    {
        source.volume = 0;
        source.Play();
        for (float i = 0; i <= volume; i += .05f)
        {
            source.volume = i;
            yield return new WaitForSeconds(.05f);
        }
    }

    public IEnumerator stopWithFade()
    {
        for(float i = source.volume; i > 0; i -= .05f)
        {
            source.volume = i;
            yield return new WaitForSeconds(.1f);
        }
        source.Stop();
        Debug.Log("audio stop");
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

    public void play(string name, bool fade=false, bool forcePlay = false)
    {
        Sound selected = Array.Find(soundList, sound => sound.name == name);
        if (selected != null && (!selected.isPlaying() || forcePlay))
        {
            if (fade)
            {
                StartCoroutine(selected.playWithFade());
            }
            else
            {
                selected.play();
            }
        }
        if (selected == null)
        {
            Debug.LogError("sound with name " + name + " not found");
        }
    }

    public void stop(string name, bool fade = false)
    {
        Sound selected = Array.Find(soundList, sound => sound.name == name);
        if (selected != null && selected.isPlaying())
        {
            if (fade)
            {
                StartCoroutine(selected.stopWithFade());
            }
            else
            {
                selected.stop();
            }
        }
        if (selected == null)
        {
            Debug.LogError("sound with name " + name + " not found");
        }
    }

    public IEnumerator playRandomSound()
    {
        yield return new WaitForSeconds(15f);
        while (true)
        {
            string soundName = "RandomNoise" + UnityEngine.Random.Range(1, 5);
            Debug.Log("playing audio " + soundName);
            play(soundName, true, true);
            yield return new WaitForSeconds(15f);
        }
    }

    public void stopAllSound()
    {
        foreach(Sound sound in soundList)
        {
            stop(sound.name, true);
        }
    }
}
