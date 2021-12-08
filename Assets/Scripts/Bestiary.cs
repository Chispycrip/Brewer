using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    public CritterData[] critters; //all unique critter data files 
    public PotionData[] potions; //all the potion data files
    public bool[] critterMask; //stores which critter entries have been unlocked
    public bool[] potionMask; //store which potion entries have been unlocked
    public bool[] recipeMask; //stores which recipes have been unlocked


    //sets up the journal to be blank by default
    public void Start()
    {
        //set size of critter mask and set everything to false
        critterMask = new bool[critters.Length];
        for (int i = 0; i < critters.Length; i++)
        {
            critterMask[i] = false;
        }

        //set size of potion and recipe masks and set everything to false
        potionMask = new bool[potions.Length];
        recipeMask = new bool[potions.Length];
        for (int i = 0; i < potions.Length; i++)
        {
            potionMask[i] = false;
            recipeMask[i] = false;
        }
    }


    //takes in a critter data file and unlocks its entry
    public void AddCritterToJournal(CritterData newCritter)
    {
        //iterate through the array of critters until the name matches the given data
        for (int i = 0; i < critters.Length; i++)
        {
            if (critters[i].typeName == newCritter.typeName)
            {
                //the names match, set data to unlocked and break loop
                critterMask[i] = true;
                break;
            }
        }
    }


    //takes in a potion data file and unlocks its entry
    public void AddPotionToJournal(PotionData newPotion)
    {
        //iterate through the array of potions until the name matches the given data
        for (int i = 0; i < potions.Length; i++)
        {
            if (potions[i].typeName == newPotion.typeName)
            {
                //the names match, set data to unlocked and break loop
                potionMask[i] = true;
                break;
            }
        }
    }


    //takes in a potion data file and unlocks its recipe
    public void AddRecipeToJournal(PotionData newRecipe)
    {
        //iterate through the array of potions until the name matches the given data
        for (int i = 0; i < potions.Length; i++)
        {
            if (potions[i].typeName == newRecipe.typeName)
            {
                //the names match, set data to unlocked and break loop
                recipeMask[i] = true;
                break;
            }
        }
    }
}
