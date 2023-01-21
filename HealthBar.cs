using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Script attached to Action Canvas > Health bar
    public Slider slider;

    public void SetMaxHealth(float health)
    {
        //The health variable is updated by the Health Manager script
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
