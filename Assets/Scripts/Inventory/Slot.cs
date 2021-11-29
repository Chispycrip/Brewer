using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public ItemUI itemUI; //the ItemUI stored in this slot
    public InventoryUI parentInv; //the inventoryUI that holds this slot
    public int index; //the index position of this slot in the InventoryUI

    //initialises the slot into it's inventory UI
    public void Init(InventoryUI parent, int slotIndex, ItemUI drag, bool jars, bool potions)
    {
        index = slotIndex;
        parentInv = parent;
        itemUI = drag;
        itemUI.slot = this;
        itemUI.jarVisible = jars;
        itemUI.acceptsPotions = potions;

        //if jars are visible, display the jar
        if (jars)
        {
            itemUI.jar.enabled = true;
        }
        else
        {
            itemUI.jar.enabled = false;
        }
    }


    //updates the contents of this slot
    public void UpdateItem(Data item)
    {
        //update the inventory array
        parentInv.inventory.items[index] = item;

        //update the itemUI to display the new data
        itemUI.SetContents(item);
    }
}
