using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Inventory
{
    public CauldronUI cauldronUI; //the image of the cauldron that can interact with itemUIs
    public InventoryUI cauldronInventoryUI; //the attached UI component
    public PotionData[] recipes; //the potion recipes
    public Journal journal; //the journal

    private Data.Names secondCritter; //the critter that matches the critter in the first slot
    private PotionData potentialPotion; //the potion that the critter in the first slot can make

    //Update is called once per frame
    void Update()
    {
        //check if the first inventory slot is occupied
        if (items[0] != null)
        {
            //check the recipes to see which critter matches this one to make a potion
            foreach (PotionData r in recipes)
            {
                //if this potion's first critter matches the critter in the inventory, store the potion's second critter and the potion
                if (r.critter1 == items[0].typeName)
                {
                    secondCritter = r.critter2;
                    potentialPotion = r;
                    break;
                }
                //if this potion's second critter matches the critter in the inventory, store the potion's first critter and the potion
                else if (r.critter2 == items[0].typeName)
                {
                    secondCritter = r.critter1;
                    potentialPotion = r;
                    break;
                }
            }
        }
        //if the first inventory slot is empty, erase any stored critter data
        else
        {
            secondCritter = Data.Names.None;
            potentialPotion = null;
        }


        //if the second inventory slot is occupied, and there is stored data, compare them
        if (items[1] != null && secondCritter != Data.Names.None)
        {
            //check if the second critter can make a potion with the first critter
            if (items[1].typeName == secondCritter)
            {
                //possible player interaction or animation//
                
                //send the potion to the cauldronUI
                cauldronUI.SetPotion(potentialPotion);

                //clear the inventory
                items[0] = null;
                items[1] = null;

                //update stored count
                itemsStored = 0;

                //add recipe to journal
                journal.AddRecipeToJournal(potentialPotion);
            }
            else
            { 
                //potion failed effect
            }
        }
    }


    //Adds items to the cauldron inventory if dropped in the cauldron UI, returns if a slot is empty
    public bool DroppedInCauldron(Data droppedItem)
    {
        //if the item is a critter
        if (droppedItem is CritterData)
        {
            //if there is an empty slot, add item
            if (itemsStored == 0 || itemsStored == 1)
            {
                //get the first slot
                cauldronInventoryUI.AddToInventory(droppedItem);

                //return that a slot is empty
                return true;
            }
            //if not return inventory full
            else
            {
                return false;
            }
        }
        //item was not a critter, return false
        else
        {
            return false;
        }
    }
}
