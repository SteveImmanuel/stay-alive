using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject rechargeStationPrefab;
    public float timeBetweenWave = 5f;
    public Text waveNowText;
    public GameObject waveCounter;

    private List<Transform> enemyPoint = new List<Transform>();
    private List<Transform> energyPoint = new List<Transform>();
    private int waveIndex = 0;
    private int enemyAlive = 0;
    private float timeUntilNextWave;

    public static WaveSpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "EnemyPoint")
            {
                enemyPoint.Add(child);
            }else if(child.tag == "EnergyPoint")
            {
                energyPoint.Add(child);
            }
        }
        timeUntilNextWave = 0;
    }

    void Update()
    {
        if (enemyAlive <= 0)
        {
            if (timeUntilNextWave <= 0)
            {
                hideCountdown();
                waveIndex++;
                spawnEnemy();
                spawnEnergy();
                timeUntilNextWave = timeBetweenWave;
                waveNowText.text = "WAVE-" + waveIndex;
            }
            else
            {
                timeUntilNextWave -= Time.deltaTime;
                showCountdown();
            }

        }
    }

    private void showCountdown()
    {
        waveCounter.SetActive(true);
        waveCounter.GetComponent<Animator>().SetBool("countdownOver", false);
        waveCounter.GetComponentInChildren<Text>().text = "Next wave begins in\n" + Mathf.RoundToInt(timeUntilNextWave);
    }

    private void hideCountdown()
    {
        waveCounter.GetComponent<Animator>().SetBool("countdownOver", true);
        Invoke("disableCountdown", .5f);
    }

    private void disableCountdown()
    {
        waveCounter.SetActive(false);
    }

    private void spawnEnemy()
    {
        enemyAlive += getTotalEnemy();
        for (int i = 0; i < getTotalEnemy(); i++)
        {
            int enemyPrefabIdx = Random.Range(0, enemyPrefab.Length);
            int locationIdx = Random.Range(0, enemyPoint.Count);
            Instantiate(enemyPrefab[enemyPrefabIdx], enemyPoint[locationIdx].position, Quaternion.identity);
        }
    }

    private void spawnEnergy()
    {
        for(int i=0; i < getTotalEnergy(); i++)
        {
            int locationIdx = Random.Range(0, energyPoint.Count);
            Instantiate(rechargeStationPrefab, energyPoint[locationIdx].position, Quaternion.identity);
        }
    }

    private int getTotalEnemy()
    {
        int totalEnemy = Mathf.RoundToInt(waveIndex * waveIndex * .5f + 3);
        return Mathf.Clamp(totalEnemy, 3, 13);

    }

    private int getTotalEnergy()
    {
        int totalEnergy = 3 * waveIndex;
        return Mathf.Clamp(totalEnergy, 3, 7);
    }

    public void reduceTotalEnemy()
    {
        enemyAlive--;
        Debug.Log("enemy alive:" + enemyAlive);
    }
}
