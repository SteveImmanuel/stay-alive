using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IPooledObject
{
    public int maxHealth = 20;
    public int score = 50;

    private int health;
    private Animator animator;
    private bool dead = false;
    private Material material;
    private float fade = 0f;
    private CharacterAudio enemyAudio;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        material = GetComponent<SpriteRenderer>().material;
        enemyAudio = GetComponent<CharacterAudio>();
        health = maxHealth;
        StartCoroutine(spawn());
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        enemyAudio.playSfxSound();
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
        StartCoroutine(dieSequence());
    }

    IEnumerator dieSequence()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    public void onInstantiate()
    {
        dead = false;
        GetComponent<EnemyController>().enabled = true;
        animator.SetBool("isDead", false);
        StartCoroutine(spawn());
        health = maxHealth;
    }
}
