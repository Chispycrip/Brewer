using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCritter : Critter
{
    public bool defaultCatchable; //if the critter is catchable by default, true only for tier 1

    public Vector3[] dodgepoints; //the points the speed critter will jump to if it detects the net swinging //DEBUG// public during testing
    private int dodgeIndex = 0; //the dodgepoint the critter is currently moving towards if it is dodging

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
        state = States.Idle;

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
        //set state to responding to player
        state = States.RespondingToPlayer;

        //set variables to find nearest dodgepoint
        int minIndex = 0;
        float minDist = 10000.0f;

        //check each dodgepoint against the minimum distance found
        for (int i = 0; i < dodgepoints.Length; i++)
        {
            //create vector between dodgepoint and critter
            Vector3 difference = gameObject.transform.position - dodgepoints[i];
            float magnitude = difference.magnitude;

            //check if it is closer than the current minimum distance, and the critter is not already very close to it
            if (magnitude < minDist && magnitude > 3.0f)
            {
                //set this dodgepoint to the new closest
                minIndex = i;
                minDist = magnitude;
            }
        }

        //set the closest dodgepoint
        dodgeIndex = minIndex;

        //set state to dodging
        state = States.Dodging;
    }


    //calls required behaviour on updates
    protected override void OnUpdate()
    {
        //check the state of the critter
        if (state == States.Dodging)
        {
            Dodge();
        }
        else if (state == States.Idle)
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


    //move towards the closest dodgepoint
    private void Dodge()
    {
        //move the critter towards the closest dodgepoint
        transform.position = Vector3.MoveTowards(transform.position, dodgepoints[dodgeIndex], Time.deltaTime * data.responseSpeed);

        //if the critter is at a dodgepoint
        if (transform.position == dodgepoints[dodgeIndex])
        {
            //reset state to idle
            state = States.Idle;
            
            //check if critter is using waypoints or not
            if (data.movementPath == Movements.Waypoint)
            {
                //find the nearest waypoint and set its index as the next waypoint
                //set variables to find nearest waypoint
                int minIndex = 0;
                float minDist = 10000.0f;

                //check each waypoint against the minimum distance found
                for (int i = 0; i < waypoints.Length; i++)
                {
                    //create vector between dodgepoint and critter
                    Vector3 difference = gameObject.transform.position - waypoints[i];
                    float magnitude = difference.magnitude;

                    //check if it is closer than the current minimum distance
                    if (magnitude < minDist)
                    {
                        //set this waypoint to the new closest
                        minIndex = i;
                        minDist = magnitude;
                    }
                }

                //set closest waypoint to the next waypoint
                waypointIndex = minIndex;
            }
            //if not following waypoints, move idle path to new position
            else
            {
                initialPos = transform.position;
            }
        }

        //get the direction the critter is moving
        Vector3 currentDirection = gameObject.transform.position - previousPos;

        //remove error message from unity console
        if (currentDirection != Vector3.zero)
        {
            //set the critter to face the direction it is moving
            gameObject.transform.rotation = Quaternion.LookRotation(currentDirection);
        }

        //end movement by updating previous position to current position
        previousPos = gameObject.transform.position;
    }

    //called when the critter detects a collision with the net
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Net"))
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
        else if(other.CompareTag("Player"))
        {
            if (!catchable)
            {
                RespondToPlayer();
            }
        }
    }
}
