using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    private AudioSource source;

    public void setSource(AudioSource targetSource)
    {
        source = targetSource;
        source.clip = clip;
    }

    public void play()
    {
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] soundsList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
