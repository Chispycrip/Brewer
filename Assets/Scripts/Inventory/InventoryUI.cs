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
            if (inventory.items[i] == null)
            {
                slots[i].itemUI.item = null;
                slots[i].itemUI.image.enabled = false;
            }
        }
    }
}
