using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 20;
    public int score = 50;

    private Animator animator;
    private bool dead = false;
    private Material material;
    private float fade = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        material = GetComponent<SpriteRenderer>().material;
        StartCoroutine(spawn());
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            die();
        }
    }

    IEnumerator spawn()
    {
        for (float i = 0f; i < 2f; i += .01f)
        {
            fade = i;
            material.SetFloat("_Fade", fade);
            yield return null;
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
            if (transform.position.y < -23f)
            {
                die();
                return;
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

        GetComponent<EnemyController>().enabled = false;
        WaveSpawner.instance.reduceTotalEnemy();
        ScoreCounter.instance.addScore(score);

        animator.SetBool("isDead", true);
        Destroy(gameObject, 1.5f);
    }
}
