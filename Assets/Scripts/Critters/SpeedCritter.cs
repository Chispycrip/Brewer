using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCritter : Critter
{
    public bool defaultCatchable;//if the critter is catchable by default, true only for tier 1

    public Vector3[] dodgepoints;//the points the speed critter will jump to if it detects the net swinging //DEBUG// public during testing

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

        //set state to idle
        state = "Idle";

        //set catchable to default value
        catchable = defaultCatchable;

        //set tag
        gameObject.tag = "Speed";

        //set previous and initial position
        initialPos = gameObject.transform.position;
        previousPos = initialPos;
    }


    //change behaviour to respond to player's actions
    public override void RespondToPlayer()
    {
        //to be implemented later in Alpha

    }


    //calls required behaviour on updates
    protected override void OnUpdate()
    {
        //check the state of the critter
        if (state == "Idle")
        {
            IdleMovement();
        }
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


    //sets the array of dodgepoints
    public void SetDodgepoints(Vector3[] dodge)
    {
        dodgepoints = dodge;
    }
}
