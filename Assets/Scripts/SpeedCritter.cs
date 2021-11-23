using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCritter : Critter
{
    //if the critter is catchable by default, true only for tier 1
    public bool defaultCatchable;

    //Init is called upon instantiation by the spawnpoint 
    public override void Init()
    {
        //set default catchable state based on tier
        if (data.tier == 1)
        {
            defaultCatchable = true;
        }
        else 
        {
            defaultCatchable = false;
        }
    }


    //change behaviour to respond to player's actions
    protected override void RespondToPlayer()
    {
        //to be implemented later in Alpha
    }


    //calls required behaviour on updates
    protected override void OnUpdate(float deltaTime)
    {
        //speed critters follow only idle movement on updates
        IdleMovement(deltaTime);
    }


    //makes changes to critter based on potion tier level
    public void SpeedPotion(int potionTier)
    {
        //if this potion is powerful enough for this critter's tier, set catchable to true
        if (potionTier + 1 >= data.tier)
        {
            catchable = true;
        }
    }
}
