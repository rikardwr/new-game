using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

	public static bool isAlive = true;
	public int maxHealth = 100;
	public static int currentHealth;
	public HealthBar healthBar;

	float fallTime = 0;
	bool hasFallen = false;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		isAlive = true;
		healthBar.SetMaxHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	TakeDamage(20);
		//}
        if (GetComponent<Rigidbody>().velocity.y < 1)
        {
            fallTime += Time.deltaTime;
//            Debug.Log(fallTime);
            hasFallen = true;
        }
        else if (hasFallen)
        {
            if (fallTime > 1)
            {
            Debug.Log("has fallen");
            Debug.Log(fallTime);
            TakeDamage(30);
            }
            // Reset fall measurements
            hasFallen = false;
            fallTime = 0;
        }
	}

	void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if(currentHealth <= 0 ) {
			isAlive = false;
		}
		healthBar.SetHealth(currentHealth);
	}
}
