using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class OpponentHealth : MonoBehaviour
{

    private static Animator animatorOpponent;
    public static bool isAlive;
    public int maxHealth = 100;
    private static int currentOpponentHealth;
    public static HealthBar healthBar;

    float fallTime = 0;
    bool hasFallen = false;
    int damage = 55;

    // Start is called before the first frame update
    void Start()
    {
        currentOpponentHealth = 100;
        isAlive = true;
        // healthBar.SetMaxHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpponentTakeDamage(damage);
            animatorOpponent.SetTrigger("opponentDead");
        }
        if (GetComponent<Rigidbody>().velocity.y < 1)
        {
            fallTime += Time.deltaTime;
            hasFallen = true;
            //            Debug.Log(fallTime);
        }
        else if (hasFallen)
        {
            if (fallTime > 1)
            {
                Debug.Log("has fallen");
                Debug.Log(fallTime);
                // TakeDamage(30);
            }
            // Reset fall measurements
            hasFallen = false;
            fallTime = 0;
        }
    }

    public static void OpponentTakeDamage(int damage)
    {
        Debug.Log(currentOpponentHealth);
        currentOpponentHealth -= damage;
        Debug.Log("opponentTakeDamage()" + currentOpponentHealth);
        // animator.SetTrigger("takeDamage");
        if (currentOpponentHealth <= 0)
        {
            Debug.Log("opponent died");
            isAlive = false;
            animatorOpponent.SetTrigger("Die");
        }
        animatorOpponent.SetTrigger("opponentDamage");
        animatorOpponent.SetTrigger("opponentDamage");
        // healthBar.SetHealth(currentHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
            Debug.Log("your opponent is detecting collision " + damage);
        if (collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Damage_10")
        {

            OpponentTakeDamage(damage);
            Debug.Log("your opponent took a hit of: " + damage);
            //Debug.Log("collision caused by an Untagged game object");
            //Debug.Log(currentHealth);
            //currentHealth -= 20;
            //playerHealth.TakeDamage(damage);
        }
        //Debug.Log("collision has occured");
    }
}
