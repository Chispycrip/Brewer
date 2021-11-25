using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : Draggable
{
    public Image image; //the displayed image of the item
    public Data item; //the data of the stored object

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
                gameObject.GetComponent<Image>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Image>().enabled = false;
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
        ItemUI other = newParent.draggable as ItemUI;

        //if the other slot has an itemUI
        if (other)
        {
            //store both item Data and then swap them into the opposite slot
            Data ours = item;
            Data theirs = other.item;

            slot.UpdateItem(theirs);
            other.slot.UpdateItem(ours);
        }
    }


    //puts the cauldron's contents into the dragged slot
    protected override void TakePotionFromCauldron(Cauldron cauldron)
    {
        //if this itemUI is empty, fill it with the contents of the cauldron and empty the cauldron
        if (item == null)
        {
            SetContents(cauldron.GetPotion());
            cauldron.ClearPotion();
        }
    }
}
