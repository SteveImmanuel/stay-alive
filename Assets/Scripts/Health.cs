using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 20;
    private Animator animator;
    private bool dead = false;
    private Material material;
    private float fade = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        material = GetComponent<SpriteRenderer>().material;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            die();
        }
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
            if (fade < 2f)
            {
                fade += Time.deltaTime;
                material.SetFloat("_Fade", fade);
            }
        }
    }

    public bool isDead()
    {
        return dead;
    }

    private void die()
    {
        dead = true;
        if (gameObject.tag == "Player")
        {
            GetComponent<PlayerController>().enabled = false;
            animator.SetBool("isJumping", false);
        }
        else if(gameObject.tag == "Enemy")
        {
            GetComponent<EnemyController>().enabled = false;
            WaveSpawner.instance.reduceTotalEnemy();
        }
        animator.SetBool("isDead", true);
        Destroy(gameObject, 1.5f);
    }
}
