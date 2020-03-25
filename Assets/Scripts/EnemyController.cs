using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 100f;
    public float smoothTime = 0.25f;
    public float minDistance = .7f;

    private Transform target;
    private Rigidbody2D rb;
    private Animator animator;
    private float movementSpeed;
    private int facingRight = 1;
    private float distance;
    private Vector3 velocity = Vector3.zero;
    private bool isChasing = true;
    private EnemyAttack attackController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackController = GetComponent<EnemyAttack>();
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (target != null)
        {
            distance = target.position.x - transform.position.x;
            if (distance > 0)
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
            rb.velocity = Vector3.zero;
        }
    }

}
