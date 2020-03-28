using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    public AudioSource runningSource;
    public AudioClip sfxSound;
    [Range(0,1)]
    public float volume = 1;
    [Range(-3, 3)]
    public float pitch;
    [Range(0, 1)]
    public float spatialBlend;

    private void Start()
    {
        runningSource.volume = volume;
        runningSource.pitch = pitch;
        runningSource.spatialBlend = spatialBlend;
    }

    public void setRunning(bool run)
    {
        if (run && !runningSource.isPlaying)
        {
            runningSource.Play();
        }
        else if (!run && runningSource.isPlaying)
        {
            runningSource.Stop();
        }
    }

    public void playSfxSound()
    {
        GameObject temp = new GameObject("temp_shootSound");
        temp.transform.SetParent(transform);
        AudioSource tempAudioSource = temp.AddComponent<AudioSource>();
        tempAudioSource.clip = sfxSound;
        tempAudioSource.volume = volume;
        tempAudioSource.pitch = pitch;
        tempAudioSource.spatialBlend = spatialBlend;
        tempAudioSource.Play();
        Destroy(temp, 3f);
    }

    public void playSfxSound(float customVolume)
    {
        GameObject temp = new GameObject("temp_shootSound");
        temp.transform.SetParent(transform);
        AudioSource tempAudioSource = temp.AddComponent<AudioSource>();
        tempAudioSource.clip = sfxSound;
        tempAudioSource.volume = customVolume;
        tempAudioSource.pitch = pitch;
        tempAudioSource.spatialBlend = spatialBlend;
        tempAudioSource.Play();
        Destroy(temp, 3f);
    }
}
