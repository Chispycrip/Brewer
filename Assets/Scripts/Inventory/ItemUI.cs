using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : Draggable
{
    public Image image; //the displayed image of the item
    private ScriptableObject item; //

    //set the contents of the jar
    public void SetContents(ScriptableObject newItem)
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
    { 
    
    }
}
