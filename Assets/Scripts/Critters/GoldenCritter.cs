using System.Collections;
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


}
