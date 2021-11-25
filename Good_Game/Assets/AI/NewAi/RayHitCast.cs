using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHitCast : MonoBehaviour
{
    public static int currentHealth;
    public MeleeHealthBar meleeHealthBar;
    private static Animator meleeAnimator;
    public static bool isAlive;
    public int maxHealth = 100;
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        meleeHealthBar.SetNewHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("(smeleeController) player died");
            isAlive = false;
            // pause_menu.Pause();
            meleeAnimator.SetTrigger("Die");
            meleeAnimator.Play("PlayerDeath");
        }
        // Debug.Log("(meleeController) TakeDamage() " + currentHealth);
        meleeAnimator.Play("TakeDamage");
        // meleeAnimator.SetTrigger("takeDamage");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        // Debug.Log("collision with untagged");
        if (collision.gameObject.tag == "OpponentSword")
        // if (collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Damage_10")
        {
            int damage = 26;
            TakeDamage(damage);
            Debug.Log("collision with OpponentSword");
            // Debug.Log(damage);
        }
        if (collision.gameObject.tag == "mySword")
        {
            Debug.Log("collision with mySword");
            OpponentHealth.OpponentTakeDamage(34);
        }
        if (collision.gameObject.tag == "getHealth")
        {
            Debug.Log("if(true) => getting max health");
            currentHealth = maxHealth;
            meleeHealthBar.SetNewHealth(currentHealth);
        }
    }
}
