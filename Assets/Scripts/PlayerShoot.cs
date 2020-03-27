﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float energyCost = 2f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerEnergy.instance.takeDamage(energyCost);
            Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            animator.SetBool("isShooting", true);
            Invoke("stopShooting", .35f);
        }
    }

    private void stopShooting()
    {
        animator.SetBool("isShooting", false);
    }
}
