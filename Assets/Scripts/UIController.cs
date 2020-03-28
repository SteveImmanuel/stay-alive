using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text waveNowText;
    public Text zombieAliveText;
    public Text scoreText;
    public GameObject waveCounter;
    public GameObject statusCanvas;
    public GameObject messageCanvas;
    public GameObject player;
    public EnergyBar energyBar;
    public float openingSequenceDuration = 2f;

    private WaveSpawner waveSpawner;
    private ScoreCounter scoreCounter;
    private bool isGameOver = false;

    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        waveSpawner = GetComponent<WaveSpawner>();
        scoreCounter = GetComponent<ScoreCounter>();
    }

    void Start()
    {
        StartCoroutine(startSequence());
    }

    private void Update()
    {
        if (!isGameOver && player.GetComponent<PlayerEnergy>().isDead())
        {
            isGameOver = true;
            StartCoroutine(endSequence());
        }
    }

    IEnumerator startSequence()
    {
        yield return new WaitForSeconds(1f);
        messageCanvas.GetComponent<MessageCanvas>().displayOpening();
        messageCanvas.SetActive(true);
        yield return new WaitForSeconds(openingSequenceDuration);
        messageCanvas.GetComponent<MessageCanvas>().hideCanvas();
        yield return new WaitForSeconds(.5f);
        messageCanvas.SetActive(false);
        statusCanvas.SetActive(true);
        player.SetActive(true);
        scoreCounter.enabled = true;
        waveSpawner.enabled = true;
    }

    IEnumerator endSequence()
    {
        waveSpawner.enabled = false;
        scoreCounter.enabled = false;
        statusCanvas.GetComponent<Animator>().SetTrigger("Change");
        yield return new WaitForSeconds(.5f);
        statusCanvas.SetActive(false);
        messageCanvas.GetComponent<MessageCanvas>().displayGameOver();
        messageCanvas.GetComponent<MessageCanvas>().scoreFinalText.text = "Wave Survived: " + scoreCounter.getScore();
        messageCanvas.GetComponent<MessageCanvas>().waveSurvivedText.text = "Total Score: " + waveSpawner.getWaveIdx();
        messageCanvas.SetActive(true);
    }

    public void setWaveNowText(int waveIdx)
    {
        waveNowText.text = "WAVE-" + waveIdx;
    }

    public void setZombieAliveText(int zombieCount)
    {
        zombieAliveText.text = "Zombies Alive: " + zombieCount;
    }

    public void setScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void showCountdown(int timeUntilNextWave)
    {
        waveCounter.SetActive(true);
        waveCounter.GetComponent<Animator>().SetBool("countdownOver", false);
        waveCounter.GetComponentInChildren<Text>().text = "Next wave begins in\n" + timeUntilNextWave;
    }

    public void hideCountdown()
    {
        if (waveCounter.activeSelf == true)
        {
            waveCounter.GetComponent<Animator>().SetBool("countdownOver", true);
            Invoke("disableCountdown", .5f);
        }
    }

    private void disableCountdown()
    {
        waveCounter.SetActive(false);
    }

    public void setEnergyBarMax(float max)
    {
        energyBar.setMaxEnergy(max);
    }

    public void setEnergyBar(float energy)
    {
        energyBar.setEnergy(energy);
    }

    public void glowEnergyBar()
    {
        energyBar.glowImage();
    }

    public void unglowEnergyBar()
    {
        energyBar.unglowImage();
    }

}
