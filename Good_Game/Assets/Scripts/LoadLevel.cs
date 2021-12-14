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

    // }[SerializeField]
    //Material[] skyboxes;

    //void Awake()
    //{
    //    RenderSettings.skybox = skyboxes[yourSkyboxIndex];
    //}

    void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == "Player")
        {
                SceneManager.UnloadSceneAsync("intro_scene");
                Debug.Log("unloadScene intro_scene");

                SceneManager.LoadScene("PettersMapV2", LoadSceneMode.Additive);
                //RenderSettings.skybox = skyboxes[0];
                Debug.Log("loadScene pettersMap");
            }
    }
}
