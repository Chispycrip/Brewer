using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : Draggable
{
    public Image image; //the displayed image of the item
    public Data item; //the data of the stored object
    public Image jar; //the jar image childed to this ItemUI

    public bool acceptsPotions; //if this ItemUI accepts Potions
    public bool jarVisible; //if this ItemUI has a visible jar

    //set the contents of the slot
    public void SetContents(Data newItem)
    {
        item = newItem;

        //if an item exists, set the ItemUI to display its icon
        if (image)
        {
            if (item)
            {
                image.sprite = item.icon;
                image.color = item.spriteColour;
                image.enabled = true;
            }
            else
            {
                image.enabled = false;
            }
        }
    }


    //returns the stored item data
    public ScriptableObject GetItem()
    {
        return item;
    }


    //swaps the contents of the slots
    protected override void Swap(Slot newParent)
    {
        //read in other item
        ItemUI other = newParent.itemUI;

        //if the other slot has an itemUI
        if (other)
        {
            //if the other slot accepts potions or the transfered data is a not a potion
            if (other.acceptsPotions || !(item is PotionData))
            {
                //store both item Data and then swap them into the opposite slot
                Data ours = item;
                Data theirs = other.item;

                slot.UpdateItem(theirs);
                other.slot.UpdateItem(ours);
            }
        }
    }


    //puts the cauldron's contents into the dragged slot
    protected override void TakePotionFromCauldron(CauldronUI cauldron)
    {
        //if this itemUI is empty
        if (item == null)
        {
            //add the potion to the inventory
            slot.UpdateItem(cauldron.GetPotion());

            //clear the potion's contents
            cauldron.ClearPotion();
        }
    }
}
