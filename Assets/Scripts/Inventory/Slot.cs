using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public ItemUI itemUI; //the ItemUI stored in this slot
    public InventoryUI parentInv; //the inventoryUI that holds this slot
    public int index; //the index position of this slot in the InventoryUI
    public Draggable draggable; //the Draggable

    //initialises the slot into it's inventory UI
    public void Init(InventoryUI parent, int slotIndex, Draggable drag)
    {
        index = slotIndex;
        parentInv = parent;
        draggable = drag;
        draggable.slot = this;
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
