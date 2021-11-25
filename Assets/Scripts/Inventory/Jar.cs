using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour
{
    public ItemUI itemUI; //the ItemUI stored in this jar
    public InventoryUI parentInv; //the inventoryUI that holds this jar
    public int index; //the index position of this jar in the InventoryUI
    public Draggable draggable; //the Draggable

    //initialises the jar into it's inventory UI
    public void Init(InventoryUI parent, int jarIndex, Draggable drag)
    {
        index = jarIndex;
        parentInv = parent;
        draggable = drag;
        draggable.jar = this;
    }


    //updates the contents of this jar
    public void UpdateItem(Data item)
    {
        //update the inventory array
        parentInv.inventory.items[index] = item;

        //update the itemUI to display the new data
        itemUI.SetContents(item);
    }
}
