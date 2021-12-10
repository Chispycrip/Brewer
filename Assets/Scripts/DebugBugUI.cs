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
            if (playerInventory.itemsStored < 4)
            {
                GetComponent<Text>().text = "Bugs Caught: " + playerInventory.itemsStored;
            }
            else
            {
                GetComponent<Text>().text = "Inventory full with 4 bugs";
            }
        }
    }
}
