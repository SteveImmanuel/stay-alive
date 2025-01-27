﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 250f;
    public float smoothTime = 0.25f;
    public float jumpForce = 200f;

    private Rigidbody2D rb;
    private Animator animator;
    private float horizontalMovement;
    private Vector3 velocity = Vector3.zero;
    private bool isJumping = false;
    private bool isGrounded = false;
    private bool facingRight = true;
    private CharacterAudio playerAudio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<CharacterAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * speed;

        if(facingRight && horizontalMovement < 0)
        {
            facingRight = false;
        }else if (!facingRight && horizontalMovement > 0)
        {
            facingRight = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isJumping = true;
            }
        }

        animate();
    }

    void FixedUpdate()
    {
        orient();
        Vector3 targetVelocity = new Vector2(horizontalMovement * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    private void orient()
    {
        Quaternion lookRotation = transform.rotation;
        
        if (facingRight)
        {
            lookRotation.y = 0;
        }
        else
        {
            lookRotation.y = 180;
        }

        transform.rotation = lookRotation;
    }

    private void animate()
    {
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
        animator.SetBool("isJumping", !isGrounded);
        if (Mathf.Abs(horizontalMovement) > 0.01 && isGrounded)
        {
            playerAudio.setRunning(true);
        }
        else
        {
            playerAudio.setRunning(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            isGrounded = false;
        }
    }

}
