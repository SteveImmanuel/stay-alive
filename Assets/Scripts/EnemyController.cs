using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public const float baseSpeed = 150f;

    public float speed = 100f;
    public float smoothTime = 0.25f;
    public float minDistance = .7f;
    public float lookRadius = 5f;

    private Transform target;
    private Rigidbody2D rb;
    private Animator animator;
    private float movementSpeed;
    private int facingRight = 1;
    private float distance;
    private Vector3 velocity = Vector3.zero;
    private bool isChasing = false;
    private EnemyAttack attackController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackController = GetComponent<EnemyAttack>();
        animator.speed = speed / baseSpeed;
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = null;
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= lookRadius)
            {
                if (target.position.x - transform.position.x > 0)
                {
                    facingRight = 0;
                }
                else
                {
                    facingRight = 1;
                }

                isChasing = Mathf.Abs(distance) > minDistance ? true : false;
                if (attackController.getAttackState() == false)
                {
                    animator.SetBool("isChasing", isChasing);
                    attackController.setAttack(!isChasing);
                }
                animator.SetBool("isTargetReachable", true);
            }
            else
            {
                isChasing = false;
                animator.SetBool("isChasing", false);
                animator.SetBool("isTargetReachable", false);
            }
        }
        else
        {
            isChasing = false;
            animator.SetBool("isChasing", false);
            animator.SetBool("isTargetDead", true);
            attackController.setAttack(false);
        }
    }

    void FixedUpdate()
    {
        Quaternion lookRotation = transform.rotation;
        lookRotation.y = 180 * facingRight;
        transform.rotation = lookRotation;

        if (isChasing)
        {
            Vector3 targetVelocity = transform.right * speed * Time.fixedDeltaTime;
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
