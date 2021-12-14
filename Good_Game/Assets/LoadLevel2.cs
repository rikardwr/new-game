using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel2 : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //[SerializeField]
    //Material[] skyboxes;
    //public static Material skybox;
    //var otherSkybox  Material;

    //public Material skyOne;

    // }

    //public static void UpdateEnvironment()
    //{
    //    RenderSettings.skybox = mat2;
    //}

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            SceneManager.UnloadSceneAsync("PettersMapV2");
            RenderSettings.skybox = (null);
            Debug.Log("unloadScene intro_scene");

            SceneManager.LoadScene("Inside_Colosseum_Night", LoadSceneMode.Additive);
            //RenderSettings.skybox = skyboxes[1];
            //UpdateEnvironment();
            //RenderSettings.skybox = skyOne;
            //RenderSettings.fog = true;
            //RenderSettings.fogDensity = 0.015f;
            Debug.Log("loadScene pettersMap");
        }
    }
}
