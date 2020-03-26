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
            Destroy(gameObject, 2f);
            particleModule.loop = false;
        }
    }

    private void FixedUpdate()
    {
        if (playerInside)
        {
            timeSpent += Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("player enter");
            playerInside = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInside = false;
        }
    }
}
