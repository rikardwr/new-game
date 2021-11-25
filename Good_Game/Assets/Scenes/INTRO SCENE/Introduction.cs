using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("PettersMap", LoadSceneMode.Single);
    }
}
