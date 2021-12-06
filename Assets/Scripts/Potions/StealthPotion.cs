using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthPotion : Potion
{
    public float transparencyIncrement; //the change in transparency value
    
    //called upon instantiation
    public override void Init()
    {
        //potion visual effects to be implemented in Beta
    }

    //consumes the potion and triggers its effects in every stealth critter on the map
    public override void Consume()
    {
        //create a list of every stealth critter in the game
        GameObject[] stealthCritters = GameObject.FindGameObjectsWithTag("Stealth");

        //run the stealthPotion function in every stealth critter
        foreach (GameObject c in stealthCritters)
        {
            //get the script component and send it the tier number
            c.GetComponent<StealthCritter>().StealthPotion(data.tier);
        }
    }
}
