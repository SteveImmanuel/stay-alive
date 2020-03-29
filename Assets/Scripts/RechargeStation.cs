using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeStation : MonoBehaviour
{
    public float totalEnergy = 10f;
    public float absorbTime = 1f;

    private bool playerInside = false;
    private float timeSpent = 0;

    void Update()
    {
        if (timeSpent >= absorbTime)
        {
            Debug.Log("recharge station destroy");
            PlayerEnergy.instance.setDefaultEnergyRate();
            timeSpent = 0;
            playerInside = false;
            gameObject.SetActive(false);
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
