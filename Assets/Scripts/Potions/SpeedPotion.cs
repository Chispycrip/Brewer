using UnityEngine;

/// <summary>
/// The SpeedPotion. Speeds the character up
/// </summary>
public class SpeedPotion : Potion
{
    public float speedIncrement; //the increase in rate of movement speed
    
    //called upon instantiation
    public override void Init()
    {
        //potion visual effects to be implemented in Beta
    }

    //consumes the potion and triggers its effects in every speed critter on the map
    public override void Consume()
    {
        //create a list of every speed critter in the game
        GameObject[] speedCritters = GameObject.FindGameObjectsWithTag("Speed");

        //run the SpeedPotion function in every speed critter
        foreach (GameObject c in speedCritters)
        {
            //get the script component and send it the tier number
            c.GetComponent<SpeedCritter>().SpeedPotion(data.tier);
        }

        //create visual effects on the player
        //to be implemented in Beta
    }
}
