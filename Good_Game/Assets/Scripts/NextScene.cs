using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    public static bool GameIsPaused = false;

    //public GameObject pauseMenuUI;

    public Animator transistion;

    // Update is called once per frame
    void Update()
    {
        if (MeleeController.isAlive == false)
        {

            Debug.Log("pause_menu you died! in");
            //LoadActiveScene();
            LoadActiveScene();
            //LoadNextLevel();

        }

        if(OpponentController.isAlive == false)
        {


           //LoadNextLevel();

        }
        // if (PlayerHealth.isAlive == false)
        // {
        //     PlayerMovement.animator.SetTrigger("Die");
        //     Debug.Log("pause_menu you died! in");
        //     Pause();
        // }
       
        

    }

    
    

    public void LoadNextLevel()
    {
     
         StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }
    
    public void LoadActiveScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        // play animation

        //wait

        yield return new WaitForSeconds(5);


        SceneManager.LoadScene(levelIndex);

        //load scene



    }

    public void Resume()
    {
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("LoadMenu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu_main");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
