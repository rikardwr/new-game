using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayerManager : MonoBehaviour
{
    #region Singleton
        public static MeleePlayerManager instance;

        void Awake () {
            instance = this;
        }
    #endregion

    public GameObject player;

}
