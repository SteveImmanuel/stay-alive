using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject rechargeStationPrefab;
    public float timeBetweenWave = 5f;

    private List<Transform> enemyPoint = new List<Transform>();
    private List<Transform> energyPoint = new List<Transform>();
    private int waveIndex = 0;
    private int enemyAlive = 0;
    private float elapsedTime = 0f;

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
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeBetweenWave || enemyAlive <= 0)
        {
            waveIndex++;
            Debug.Log("wave-" + waveIndex + " begins");
            spawnEnemy();
            spawnEnergy();
            elapsedTime = 0f;
        }
    }

    private void spawnEnemy()
    {
        enemyAlive += getTotalEnemy();
        Debug.Log("enemy alive:" + enemyAlive);
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
        int totalEnemy = (int)Mathf.Round(waveIndex * waveIndex * .5f + 5);
        return Mathf.Clamp(totalEnemy, 6, 13);

    }

    private int getTotalEnergy()
    {
        return 3 * waveIndex;
    }

    public void reduceTotalEnemy()
    {
        enemyAlive--;
        Debug.Log("enemy alive:" + enemyAlive);
    }
}
