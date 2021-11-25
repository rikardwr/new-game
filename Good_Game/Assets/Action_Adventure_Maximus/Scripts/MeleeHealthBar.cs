using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image HealthBar;

    public void SetNewMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        // Debug.Log("(meleeHealthBar)SetNewMaxHealth: " + health);
    }

    public void SetNewHealth(int health)
    {
        slider.value = health;
        // Debug.Log("(meleeHealthBar)SetNewHealth: " + health);
    }
}
