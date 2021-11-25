using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMang : MonoBehaviour
{
	#region Singleton

	public static GameMang instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	public GameObject Player;

	public void KillPlayer()
    {

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex
			);
    }
}
