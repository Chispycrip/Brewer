﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthCritter : Critter
{
    //if the critter will hide when it sees the player, default false only for tier 1
    public bool willHide;
    //the player object, used to track its proximity to the critter
    public GameObject playerObject;

    //Init is called upon instantiation by the spawnpoint 
    public override void Init()
    {
        //set default hiding state based on tier
        if (data.tier == 1)
        {
            willHide = false;
        }
        else
        {
            willHide = true;
        }

        //set state to idle
        state = "Idle";

        //set tag
        gameObject.tag = "Stealth";
    }


    //change behaviour to respond to player's actions
    protected override void RespondToPlayer()
    {
        //to be implemented later in Alpha
    }


    //checks if the player is within the detection distance
    private bool PlayerWithin()
    {
        //get player and critter positions
        Vector3 playerPos = playerObject.transform.position;
        Vector3 critterPos = gameObject.transform.position;

        //create vector between the two positions
        Vector3 difference = playerPos - critterPos;

        //return if distance to player is less than detection distance
        if (difference.magnitude <= data.detectionDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //calls required behaviour on updates
    protected override void OnUpdate(float deltaTime)
    {
        //stealth critters check player proximity on updates to decide behaviour
        if (PlayerWithin())
        {
            RespondToPlayer();
        }
        else
        {
            IdleMovement(deltaTime);
        }
    }


    //makes changes to critter based on potion tier level
    public void StealthPotion(int potionTier)
    {
        //if this potion is powerful enough for this critter's tier, set willHide to false
        if (potionTier + 1 >= data.tier)
        {
            willHide = false;
        }
    }
}