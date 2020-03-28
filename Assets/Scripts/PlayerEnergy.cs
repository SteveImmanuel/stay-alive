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
        UIController.instance.setEnergyBarMax(maxEnergy);
    }

    public void takeDamage(float damage)
    {
        energy -= damage;
        energy = Mathf.Clamp(energy, 0, maxEnergy);
        UIController.instance.setEnergyBar(energy);
        
        if (energy <= 0 && !dead)
        {
            die();
        }
    }

    IEnumerator spawn()
    {
        for(float i = 0f; i < 2f; i+=.01f)
        {
            fade = i;
            material.SetFloat("_Fade", fade);
            yield return null;
        }
    }

    public void setEnergyRate(float rate)
    {
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

        if (energyRate > 0)
        {
            UIController.instance.glowEnergyBar();
        }
        else
        {
            UIController.instance.unglowEnergyBar();
        }
    }

    public bool isDead()
    {
        return dead;
    }

    private void die()
    {
        dead = true;
        GetComponent<PlayerController>().enabled = false;
        animator.SetBool("isJumping", false);
        animator.SetBool("isDead", true);
        Destroy(gameObject, 1.5f);
    }

}
