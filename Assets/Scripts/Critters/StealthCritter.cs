using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthCritter : Critter
{
    public bool willHide; //if the critter will hide when it sees the player, default false only for tier 1
    public GameObject playerObject; //the player object, used to track its proximity to the critter

    public Vector3[] hidepoints; //the points the stealth critter will follow to hide if it detects the player nearby //DEBUG// public during testing
    public int hidepointIndex = 0; //the current hidepoint the critter is moving towards if it is hiding

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

        //set previous and initial position
        initialPos = gameObject.transform.position;
        previousPos = initialPos;
    }


    //change behaviour to respond to player's actions
    protected override void RespondToPlayer()
    {
        //if this critter will hide, set to hiding
        if (willHide)
        {
            state = "Hiding";
        }
        else
        {
            state = "Idle";
        }
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
    protected override void OnUpdate()
    {
        //if the critter is inactive, there is no need to update this frame
        if (state != "Inactive")
        {
            //if the player is nearby and the critter is not already hiding, respond to the player
            if (PlayerWithin() && state != "Hiding")
            {
                //update state and respond to player
                state = "RespondingToPlayer";
                RespondToPlayer();
            }

            //if the critter is hiding, continue to update its position as it moves
            if (state == "Hiding")
            {
                Hide();
            }
            //if the critter is idle, continue its idle movement
            else if (state == "Idle")
            {
                IdleMovement();
            }
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


    //sets the array of hidepoints
    public void SetHidepoints(Vector3[] hide)
    {
        hidepoints = hide;
    }


    //move the critter into a hiding location via a waypoint system using the hidepoints
    private void Hide()
    {
        //move the critter towards the next hidepoint
        transform.position = Vector3.MoveTowards(transform.position, hidepoints[hidepointIndex], Time.deltaTime * data.movementSpeed);

        //if the critter is at a hidepoint, increase hidepoint index
        if (transform.position == hidepoints[hidepointIndex])
        {
            hidepointIndex++;
        }

        //if the critter is at the last hidepoint, set it to inactive
        if (hidepointIndex == hidepoints.Length)
        {
            state = "Inactive";
            gameObject.SetActive(false);
        }
    }
}
