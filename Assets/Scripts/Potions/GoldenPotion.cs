using UnityEngine;

/// <summary>
/// Final Golden Potion.
/// </summary>
public class GoldenPotion : Potion
{
    //consumes the potion and triggers its effects in every stealth critter on the map
    public override void Consume()
    {
        //create a list of every gold critter in the game
        GameObject[] goldenCritters = GameObject.FindGameObjectsWithTag("Golden");

        //run the stealthPotion function in every gold critter
        foreach (GameObject c in goldenCritters)
        {
            //get the script component and send it the tier number
            c.GetComponent<GoldenCritter>().GoldenPotion();
        }

        //create a list of every speed critter in the game
        GameObject[] speedCritters = GameObject.FindGameObjectsWithTag("Speed");

        //run the stealthPotion function in every speed critter
        foreach (GameObject c in speedCritters)
        {
            //get the script component and send it the tier number
            c.GetComponent<SpeedCritter>().SpeedPotion(2);
        }

        //create a list of every stealth critter in the game
        GameObject[] stealthCritters = GameObject.FindGameObjectsWithTag("Stealth");

        //run the stealthPotion function in every stealth critter
        foreach (GameObject c in stealthCritters)
        {
            //get the script component and send it the tier number
            c.GetComponent<StealthCritter>().StealthPotion(2);
        }
    }
}
