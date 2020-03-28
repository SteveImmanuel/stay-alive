using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSequence : MonoBehaviour
{
    public GameObject statusCanvas;
    public GameObject player;

    private WaveSpawner waveSpawner;
    private ScoreCounter scoreCounter;

    private void Awake()
    {
        waveSpawner = GetComponent<WaveSpawner>();
        scoreCounter = GetComponent<ScoreCounter>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
