using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeStation : MonoBehaviour
{
    public float totalEnergy = 10f;
    public float absorbTime = 1f;

    private ParticleSystem particle;
    private ParticleSystem.MainModule particleModule;
    private bool playerInside = false;
    private float timeSpent = 0;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particleModule = particle.main;
    }

    void Update()
    {
        if (timeSpent >= absorbTime)
        {
            Debug.Log("recharge station destroy");
            PlayerEnergy.instance.setDefaultEnergyRate();
            Destroy(gameObject);
            particleModule.loop = false;
        }

        if (playerInside)
        {
            timeSpent += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Player enter");
            playerInside = true;
            PlayerEnergy.instance.setEnergyRate(totalEnergy / absorbTime);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInside = false;
            PlayerEnergy.instance.setDefaultEnergyRate();
        }
    }
}
