using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float energy = 100;
    public float maxEnergy = 100;
    public float defaultEnergyRate = -0.5f;

    private float energyRate = -0.5f;
    private Animator animator;
    private bool dead = false;
    private Material material;
    private float fade = 0f;

    public static PlayerEnergy instance;

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

        animator = GetComponent<Animator>();
        material = GetComponent<SpriteRenderer>().material;
        StartCoroutine(spawn());
    }

    private void Start()
    {
        UIController.instance.setBarMax(UIController.ENERGY_BAR_TYPE, maxEnergy);
        InvokeRepeating("playAudio", 0, .8f);
    }

    public void takeDamage(float damage)
    {
        energy -= damage;
        energy = Mathf.Clamp(energy, 0, maxEnergy);
        UIController.instance.setBar(UIController.ENERGY_BAR_TYPE, energy);
        
        if (energy <= 0 && !dead)
        {
            die();
        }
    }

    IEnumerator spawn()
    {
        for(float i = 0f; i < 2f; i+=.02f)
        {
            fade = i;
            material.SetFloat("_Fade", fade);
            yield return null;
        }
    }

    public void setEnergyRate(float rate)
    {
        if(rate < 0 && energyRate > 0 && !dead)
        {
            //still charging
            return;
        }
        energyRate = rate;
    }

    public void setDefaultEnergyRate()
    {
        energyRate = defaultEnergyRate;
    }

    private void Update()
    {
        if (dead)
        {
            fade -= Time.deltaTime;
            material.SetFloat("_Fade", fade);
        }
        else
        {
            if (transform.position.y < -23f)
            {
                die();
                return;
            }

            takeDamage(-energyRate * Time.deltaTime);
        }

        UIController.instance.setGlowMaterial(UIController.ENERGY_BAR_TYPE, energyRate > 0);
    }

    private void playAudio()
    {
        if(energy<= .1f * maxEnergy)
        {
            AudioManager.instance.stop("HalfLife");
            AudioManager.instance.play("QuarterLife", false, true);
        }
        else if (energy <= .25f * maxEnergy)
        {
            AudioManager.instance.stop("HalfLife");
            AudioManager.instance.play("QuarterLife");
            
        }else if(energy <= .5f * maxEnergy)
        {
            AudioManager.instance.stop("QuarterLife");
            AudioManager.instance.play("HalfLife");
        }
        else
        {
            AudioManager.instance.stop("QuarterLife");
            AudioManager.instance.stop("HalfLife");
        }
    }

    public bool isDead()
    {
        return dead;
    }

    private void die()
    {
        dead = true;
        AudioManager.instance.stop("QuarterLife");
        AudioManager.instance.stop("HalfLife");
        CancelInvoke("playAudio");
        GetComponent<PlayerController>().enabled = false;
        animator.SetBool("isJumping", false);
        animator.SetBool("isDead", true);
        Destroy(gameObject, 1.5f);
    }

}
