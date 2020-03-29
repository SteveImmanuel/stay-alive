using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public string bulletTag = "Bullet";
    public string superBulletTag = "SuperBullet";
    public Transform spawnPoint;
    public float energyCost = 2f;
    public float holdTime = 1f;

    private Animator animator;
    private CharacterAudio playerAudio;
    private float currentHoldTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<CharacterAudio>();
    }

    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    PlayerEnergy.instance.takeDamage(energyCost);
        //    ObjectPooler.instance.instantiateFromPool(bulletTag, spawnPoint.position, spawnPoint.rotation);
        //    animator.SetBool("isShooting", true);
        //    playerAudio.playSfxSound(.18f);
        //    Invoke("stopShooting", .35f);
        //}

        if (Input.GetButton("Fire1"))
        {
            currentHoldTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (currentHoldTime >= holdTime)
            {
                //deploy superbullet
                ObjectPooler.instance.instantiateFromPool(superBulletTag, spawnPoint.position, spawnPoint.rotation);
                playerAudio.playSfxSound(.4f);
            }
            else
            {
                //deploy normal bullet
                ObjectPooler.instance.instantiateFromPool(bulletTag, spawnPoint.position, spawnPoint.rotation);
                playerAudio.playSfxSound(.18f);
            }
            PlayerEnergy.instance.takeDamage(energyCost);
            animator.SetBool("isShooting", true);
            Invoke("stopShooting", .35f);
            currentHoldTime = 0;
        }
    }

    private void stopShooting()
    {
        animator.SetBool("isShooting", false);
    }
}
