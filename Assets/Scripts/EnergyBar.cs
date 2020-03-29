using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;
    public Image icon;
    public Material glowMaterial;
    private Slider slider;

    public void glowImage()
    {
        fill.material = glowMaterial;
        if (icon != null)
        {
            icon.material = glowMaterial;
        }
    }

    public void unglowImage()
    {
        fill.material = null;
        if (icon != null)
        {
            icon.material = null;
        }
    }

    public void setGlow(bool glow)
    {
        if (glow)
        {
            fill.material = glowMaterial;
            if (icon != null)
            {
                icon.material = glowMaterial;
            }
        }
        else
        {
            fill.material = null;
            if (icon != null)
            {
                icon.material = null;
            }
        }
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void setMaxEnergy(float energy)
    {
        slider.maxValue = energy;
        slider.value = energy;
        fill.color = gradient.Evaluate(1f);
    }

    public void setEnergy(float energy)
    {
        slider.value = energy;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
