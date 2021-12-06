﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenCritter : Critter
{
    public bool willHide; //if the critter will hide when it sees the player, default true
    public GameObject playerObject; //the player object, used to track its proximity to the critter

    public Vector3[] dodgepoints; //the points the speed critter will jump to if it detects the net swinging //DEBUG// public during testing
    private int dodgeIndex = 0; //the dodgepoint the critter is currently moving towards if it is dodging

    public Vector3[] hidepoints; //the points the stealth critter will follow to hide if it detects the player nearby //DEBUG// public during testing
    public int hidepointIndex = 0; //the current hidepoint the critter is moving towards if it is hiding


    //Init is called upon instantiation by the spawnpoint 
    public override void Init()
    {
        //set state to idle
        state = States.Idle;

        //set catchable to false and willHide to true
        catchable = false;
        willHide = true;

        //set tag
        gameObject.tag = "Golden";

        //set previous and initial position
        initialPos = gameObject.transform.position;
        previousPos = initialPos;
    }

    //makes changes to critter from potion consumption
    public void GoldenPotion()
    {
        //set catchable to true
        catchable = true;
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (!catchable)
            {
                // throw player off ledge
                other.GetComponent<PlayerForcer>().ApplyForce();
            }
        }
        else if (other.tag == "Net")
        {
            if (catchable == true && inventoryFull == false)
            {
                //get script from collider
                PlayerNet net = other.transform.gameObject.GetComponent<PlayerNet>();
                net.CatchCritter(this);

                //set critter state to caught
                state = States.Caught;

                //destroy critter
                Destroy(gameObject);

                //the critter object no longer exists, set state to inactive
                state = States.Inactive;
            }
            else
            {
                //call net event false
            }
        }
    }

}
