using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : Draggable
{
    public Image image; //the displayed image of the item
    public Data item; //the data of the stored object

    //set the contents of the jar
    public void SetContents(Data newItem)
    {
        item = newItem;

        //if an item exists, set the ItemUI to display its icon
        if (image)
        {
            if (item)
            {
                image.sprite = item.icon;
                image.color = item.colour;
            }
            gameObject.SetActive(item != null);
        }
    }


    //returns the stored item data
    public ScriptableObject GetItem()
    {
        return item;
    }


    //swaps the contents of the jars
    protected override void Swap(Jar newParent)
    {
        //read in other item
        ItemUI other = newParent.draggable as ItemUI;

        //if the other jar has an itemUI
        if (other)
        {
            //store both item Data and then swap them into the opposite jar
            Data ours = item;
            Data theirs = other.item;

            jar.UpdateItem(theirs);
            other.jar.UpdateItem(ours);
        }
    }
}
