using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; //the attached inventory
    public ItemUI itemPrefab; //the prefab for ItemUI
    public Slot slotPrefab; //the prefab for Slot
    private Slot[] slots; //array of slots
    
    //Start is called before the first frame update
    void Start()
    {
        //create an array of slots the same size as the inventory
        slots = new Slot[inventory.items.Length];

        //instantiate and initialise all slots in array and create an ItemUI for each slot
        for (int i = 0; i < inventory.items.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab, transform);
            slots[i].itemUI = Instantiate(itemPrefab, slots[i].transform);
            slots[i].itemUI.SetContents(inventory.items[i]);
            slots[i].Init(this, i, slots[i].itemUI, inventory.jarsVisibile, inventory.acceptsPotions);
        }
    }


    //every update, clear Data from itemUIs that are empty in the Inventory
    private void Update()
    {
        //check through all the slots in the inventory
        for (int i = 0; i < inventory.items.Length; i++)
        {
            //if the inventory slot is empty, clear the matching ItemUI and disable its image
            if (inventory.items[i] == null && slots[i] != null)
            {
                slots[i].itemUI.item = null;
                slots[i].itemUI.image.enabled = false;
            }
        }
    }


    //adds item to the inventory
    public void AddToInventory(Data item)
    {
        //if there is space, add this item to inventory
        if (inventory.itemsStored < inventory.items.Length)
        {
            //check the slots until an empty one is found
            for (int i = 0; i < inventory.items.Length; i++)
            {
                if (inventory.items[i] == null)
                {
                    //add item to empty slot
                    inventory.items[i] = item;

                    //add item to ItemUI
                    slots[i].UpdateItem(item);

                    //break once slot is found
                    break;
                }
            }

            //increase items count by one
            inventory.itemsStored++;
        }
    }


    //removes an item from the inventory
    public void RemoveFromInventory(int index)
    {
        //remove the item from the ItemUI and the inventory
        slots[index].UpdateItem(null);
    }
}
