using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoTrigger : MonoBehaviour
{
    public GameObject gameObj3;

    private void OnTrigger(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            gameObj3.SetActive(false);
        }
    }
}
