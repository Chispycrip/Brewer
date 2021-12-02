using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Inventory
{
    public CauldronUI cauldronUI; //the image of the cauldron that can interact with itemUIs
    public PotionData[] recipes; //the potion recipes
    public CritterData secondCritter; //the critter that matches the critter in the first slot
    public PotionData potentialPotion; //the potion that the critter in the first slot can make

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
                if (r.critter1.name == items[0].name)
                {
                    secondCritter = r.critter2;
                    potentialPotion = r;
                    break;
                }
                //if this potion's second critter matches the critter in the inventory, store the potion's first critter and the potion
                else if (r.critter2.name == items[0].name)
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
            secondCritter = null;
            potentialPotion = null;
        }


        //if the second inventory slot is occupied, and there is stored data, compare them
        if (items[1] != null && secondCritter != null)
        {
            //check if the second critter can make a potion with the first critter
            if (items[1].name == secondCritter.name)
            {
                //possible player interaction or animation//
                
                //send the potion to the cauldronUI
                cauldronUI.SetPotion(potentialPotion);

                //clear the inventory
                items[0] = null;
                items[1] = null;

                //update stored count
                itemsStored = 0;
            }
            else
            { 
                //potion failed effect
            }
        }
    }
}
