using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 20;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        if (gameObject.tag == "Player")
        {
            GetComponent<PlayerController>().enabled = false;
        }else if(gameObject.tag == "Enemy")
        {
            GetComponent<EnemyController>().enabled = false;
        }
        animator.SetBool("isDead", true);
        Destroy(gameObject, 2f);
    }
}
