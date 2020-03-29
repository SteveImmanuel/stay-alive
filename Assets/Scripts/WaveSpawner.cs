using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public string zombieMaleTag = "ZombieMale";
    public string zombieFemaleTag = "ZombieFemale";
    public string rechargeStationTag = "RechargeStation";
    public float timeBetweenWave = 5f;

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
                UIController.instance.hideCountdown();
                AudioManager.instance.play("NewWave", false, true);
                waveIndex++;
                spawnEnemy();
                spawnEnergy();
                timeUntilNextWave = timeBetweenWave;
                UIController.instance.setWaveNowText(waveIndex);
            }
            else
            {
                timeUntilNextWave -= Time.deltaTime;
                UIController.instance.showCountdown(Mathf.RoundToInt(timeUntilNextWave));
            }

        }
    }

    private void spawnEnemy()
    {
        enemyAlive += getTotalEnemy();
        UIController.instance.setZombieAliveText(enemyAlive);
        for (int i = 0; i < getTotalEnemy(); i++)
        {
            int enemyType = Random.Range(0, 2);
            int locationIdx = Random.Range(0, enemyPoint.Count);
            string enemyTag;
            if (enemyType == 0)
            {
                enemyTag = zombieMaleTag;
            }
            else
            {
                enemyTag = zombieFemaleTag;
            }
            ObjectPooler.instance.instantiateFromPool(enemyTag, enemyPoint[locationIdx].position, Quaternion.identity);
        }
    }

    private void spawnEnergy()
    {
        for(int i=0; i < getTotalEnergy(); i++)
        {
            int locationIdx = Random.Range(0, energyPoint.Count);
            ObjectPooler.instance.instantiateFromPool(rechargeStationTag, energyPoint[locationIdx].position, Quaternion.identity);
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
        UIController.instance.setZombieAliveText(enemyAlive);
    }

    public int getWaveIdx()
    {
        return waveIndex;
    }
}
