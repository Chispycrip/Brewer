using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : Draggable
{
    public Image image; //the displayed image of the item
    private Data item; //the data of the stored object

    //set the contents of the jar
    public void SetContents(Data newItem)
    {
        item = newItem;
    }


    //returns the stored item data
    public ScriptableObject GetItem()
    {
        return item;
    }


    //swaps the contents of the jars
    protected override void Swap(Jar newParent)
    {}
}
