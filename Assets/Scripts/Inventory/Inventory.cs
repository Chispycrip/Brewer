using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Data[] items; //the array of items stored in this inventory
    public bool isPlayerInv; //if this is the player's inventory
    public int itemsStored = 0; //how many items are in this inventory

    public bool acceptsPotions = true; //if this Inventory accepts Potions
    public bool jarsVisibile = true; //if this Inventory has visible jars


    //adds item to the inventory
    public void AddToInventory(Data item)
    {
        //if there is space, add this item to inventory
        if (itemsStored < items.Length)
        {
            //check the slots until an empty one is found
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    //add item to empty slot
                    items[i] = item;

                    //break once slot is found
                    break;
                }
            }

            //increase items count by one
            itemsStored++;
        }
    }


    //removes an item from the inventory
    public void RemoveFromInventory(int index)
    {
        //remove the item in the given index
        items[index] = null;

        //remove the item from the ItemUI

    }
}
