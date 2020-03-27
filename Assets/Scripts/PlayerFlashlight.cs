﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerFlashlight : MonoBehaviour
{
    public float energy = 100f;
    public float minRadius = 4f;
    public float maxRadius = 15f;
    public float speedFactor = 3;

    private UnityEngine.Experimental.Rendering.Universal.Light2D flashlight;
    private bool isPowerOn = false;

    void Start()
    {
        flashlight = GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isPowerOn = !isPowerOn;
            if (isPowerOn)
            {
                PlayerEnergy.instance.setEnergyRate(-2f);
            }
            else
            {
                PlayerEnergy.instance.setEnergyRate(-1f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPowerOn)
        {
            flashlight.pointLightOuterRadius += Time.fixedDeltaTime * speedFactor;
        }
        else
        {
            flashlight.pointLightOuterRadius -= Time.fixedDeltaTime * speedFactor;
        }
        flashlight.pointLightOuterRadius = Mathf.Clamp(flashlight.pointLightOuterRadius, minRadius, maxRadius);
    }
}