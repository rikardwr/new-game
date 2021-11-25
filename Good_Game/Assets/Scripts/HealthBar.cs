using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // private static Animator animator;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // public static void SetNewMaxHealth(int health)
    // {
    //     Debug.Log("(healthBar)SetNewMaxHealth: " + health);
    //     slider.maxValue = health;
    //     slider.value = health;
    // }

    // public static void SetNewHealth(int health)
    // {
    //     Debug.Log("(healthBar)SetNewHealth: " + health);
    //     slider.value = health;
    //     // animator.SetTrigger("Die");
    // }
    public void SetMaxHealth(int health)
    {
        Debug.Log("(healthBar)SetMaxHealth: " + health);
        slider.maxValue = health;
        slider.value = health;
    }


    public void SetHealth(int health)
    {
        Debug.Log("(healthBar)SetHealth" + health);
        slider.value = health;
        // animator.SetTrigger("Die");
    }
}
