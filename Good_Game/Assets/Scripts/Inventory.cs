using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake () {
        if  (instance != null){
            Debug.Log("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public List<Item> items = new List<Item>();

    public bool Add (Item item) {
        if (!item.isDefaultItem) {
            items.Add(item);
        }
        return true;
    }

    public void Remove (Item item) {
        items.Remove(item);
    }

    public virtual void Interact(){}
}
