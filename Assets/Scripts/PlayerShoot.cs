using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public string bulletTag = "Bullet";
    public string superBulletTag = "SuperBullet";
    public Transform spawnPoint;
    public float energyCost = 2f;

    private Animator animator;
    private CharacterAudio playerAudio;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<CharacterAudio>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerEnergy.instance.takeDamage(energyCost);
            ObjectPooler.instance.instantiateFromPool(bulletTag, spawnPoint.position, spawnPoint.rotation);
            animator.SetBool("isShooting", true);
            playerAudio.playSfxSound(.18f);
            Invoke("stopShooting", .35f);
        }
    }

    private void stopShooting()
    {
        animator.SetBool("isShooting", false);
    }
}
