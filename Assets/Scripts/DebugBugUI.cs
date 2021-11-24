using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBugUI : MonoBehaviour
{
    public Inventory playerInventory = null;

    // Update is called once per frame
    void Update()
    {
        if (playerInventory)
        {
            GetComponent<Text>().text = "Bugs Caught: "+playerInventory.itemsStored;
        }
    }
}
