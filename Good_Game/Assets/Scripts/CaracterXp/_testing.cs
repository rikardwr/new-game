using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _testing : MonoBehaviour
{
    //[SerializeField] private _LevelWindow _levelWindow;

    [SerializeField] private LevelWindow levelWindow;
    [SerializeField] private MeleeController player;
    

    LevelSystem levelSystem = new LevelSystem();

    public int killXP = 1;


    private void Update()
    {
        levelWindow.SetLevelSystem(levelSystem);
        
        /*
        if (Input.GetKeyDown(KeyCode.O) == true)
        {
            LevelSystem levelSystem = new LevelSystem();
            Debug.Log(levelSystem.GetLevelNumber());
            levelSystem.AddExperience(50);
            Debug.Log(levelSystem.GetLevelNumber());
            levelSystem.AddExperience(500);
            Debug.Log(levelSystem.GetLevelNumber());
            levelWindow.SetLevelSystem(levelSystem);

            levelSystem.AddExperience(500);
            player.SetLevelSystem(levelSystem);
        }
        */
        if (Input.GetKeyDown(KeyCode.B) == true)
        {
            levelSystem.AddExperience(killXP);
            Debug.Log(levelSystem.GetLevelNumber());

            player.SetLevelSystem(levelSystem);

        }

    }

    private void Awake()
    {
        /*
        LevelSystem levelSystem = new LevelSystem();
        Debug.Log(levelSystem.GetLevelNumber());
        levelSystem.AddExperience(50);
        Debug.Log(levelSystem.GetLevelNumber());
        levelSystem.AddExperience(500);
        Debug.Log(levelSystem.GetLevelNumber());
        levelWindow.SetLevelSystem(levelSystem);

        levelSystem.AddExperience(500);
        player.SetLevelSystem(levelSystem);  
        */
        /*
        if(MeleeEnemyController.isAlive == false)
        {
            LevelSystem levelSystem = new LevelSystem();
            Debug.Log(levelSystem.GetLevelNumber());
            levelSystem.AddExperience(50);
            Debug.Log(levelSystem.GetLevelNumber());
            levelSystem.AddExperience(500);
            Debug.Log(levelSystem.GetLevelNumber());
            levelWindow.SetLevelSystem(levelSystem);

            levelSystem.AddExperience(500);
            player.SetLevelSystem(levelSystem);
        }

        /*

        if(Input.GetKeyDown(KeyCode.B) == true)
        {
            levelSystem.AddExperience(killXP);
            Debug.Log(levelSystem.GetLevelNumber());

        }
        */

    }
}
