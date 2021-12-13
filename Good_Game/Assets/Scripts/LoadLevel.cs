using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == "Player")
        {
                SceneManager.UnloadSceneAsync("intro_scene");
                Debug.Log("unloadScene intro_scene");

                SceneManager.LoadScene("PettersMapV2", LoadSceneMode.Additive);
                Debug.Log("loadScene pettersMap");
            }
    }
}
