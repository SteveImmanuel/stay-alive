using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 10f;
    public float attackDelay = 1f;
    public float forceImpact = 4f;
    
    private bool isAttacking = false;
    private Animator animator;
    private bool isAttackingInProgress = false;
    private float currentDelay = 0f;


    public void setAttack(bool attack)
    {
        isAttacking = attack;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        if (isAttacking)
        {
            if (currentDelay >= attackDelay)
            {
                animator.SetBool("isAttacking", true);
                isAttackingInProgress = true;
                Invoke("resetDelay", .7f);
            }
            else
            {
                animator.SetBool("isAttacking", false);
                currentDelay += Time.fixedDeltaTime;
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public bool getAttackState()
    {
        return isAttackingInProgress;
    }

    private void resetDelay()
    {
        currentDelay = 0f;
        isAttackingInProgress = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerEnergy player = collision.transform.GetComponent<PlayerEnergy>();
            player.takeDamage(damage);

            if (!player.isDead())
            {
                Vector2 force = new Vector2(forceImpact, 5f);
                if(collision.transform.position.x < transform.position.x)
                {
                    force.x *= -1;
                }
                collision.transform.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
