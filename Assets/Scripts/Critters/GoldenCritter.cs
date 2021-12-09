using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenCritter : Critter
{
    public bool willHide; //if the critter will hide when it sees the player, default true
    public GameObject playerObject; //the player object, used to track its proximity to the critter

    public Vector3[] hidepoints; //the points the stealth critter will follow to hide if it detects the player nearby //DEBUG// public during testing
    public int hidepointIndex = 0; //the current hidepoint the critter is moving towards if it is hiding

    private Quaternion initialRot; //the rotation the critter has at spawn

    private bool hasMoved; //if the critter has moved today


    //Init is called upon instantiation by the spawnpoint 
    public override void Init()
    {
        //set state to idle
        state = States.WaitingForPlayer;

        //set catchable and willHide to false initially
        catchable = false;
        willHide = false;

        //set hasMoved to false
        hasMoved = true;

        //set tag
        gameObject.tag = "Golden";

        //set previous and initial position
        initialPos = gameObject.transform.position;
        previousPos = initialPos;

        //set initial rotation
        initialRot = gameObject.transform.rotation;

        //set waypointIndex to 1;
        waypointIndex = 1;
    }


    //makes changes to critter from potion consumption
    public void GoldenPotion()
    {
        //set catchable to true
        catchable = true;

        //set hasMoved to false
        hasMoved = false;
    }


    //sets the array of hidepoints
    public void SetHidepoints(Vector3[] hide)
    {
        hidepoints = hide;
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


    //change behaviour to respond to player's actions
    public override void RespondToPlayer()
    {
        //if this critter will hide, set to hiding
        if (willHide)
        {
            state = States.Hiding;
        }
        else
        {
            //if this critter is not hiding, make it wait for a player action
            state = States.WaitingForPlayer;
        }
    }


    //calls required behaviour on updates
    protected override void OnUpdate()
    {
        //check if critter is heading towards waypoint 0
        if (waypointIndex == 0 && state == States.Idle)
        {
            //the golden critter will not idle path towards the first waypoint, set it to waiting for player state
            state = States.WaitingForPlayer;

            //set waypoint index to 1
            waypointIndex = 1;
        }

        //if the player is nearby and the critter is not already hiding, respond to the player
        if (PlayerWithin() && state == States.WaitingForPlayer)
        {
            //update state and respond to player
            state = States.RespondingToPlayer;
            RespondToPlayer();
        }

        //if the critter is hiding, continue to update its position as it moves
        if (state == States.Hiding)
        {
            Hide();
        }
        //if the critter is waiting for a player action and not at its initial position, look at the player on the z axis
        else if (state == States.WaitingForPlayer)
        {
            //get position of player
            Vector3 playerPos = playerObject.transform.position;

            //set playerPos y value to match the critters
            playerPos.y = gameObject.transform.position.y;

            //set critter to look at the new vector
            gameObject.transform.LookAt(playerPos);

            //if the critter is at its initial position, also set it to initial rotation
            if (gameObject.transform.position == initialPos)
            {
                gameObject.transform.rotation = initialRot;
            }

        }
        //if the critter is idle, continue its idle movement
        else if (state == States.Idle)
        {
            IdleMovement();
        }
    }


    //move the critter to the next hidepoint hiding location via a waypoint system using the hidepoints
    private void Hide()
    {
        //move the critter towards the next hidepoint
        transform.position = Vector3.MoveTowards(transform.position, hidepoints[hidepointIndex], Time.deltaTime * data.responseSpeed);

        //if the critter is at a hidepoint, increase hidepoint index and set back to waiting for player
        if (transform.position == hidepoints[hidepointIndex])
        {
            hidepointIndex++;
            state = States.WaitingForPlayer;
        }

        //if the critter is at the last hidepoint, disable willHide, reset hidepoint index and set state to waiting for player
        if (hidepointIndex == hidepoints.Length)
        {
            willHide = false;
            hidepointIndex = 0;
            state = States.WaitingForPlayer;
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


    //starts the critter's movement down and away from the tree
    public void PlayerLeftCamp()
    {
        if (gameObject.transform.position == initialPos && (!catchable || !hasMoved))
        {
            //set critter state to idle
            state = States.Idle;

            //set critter to willHide
            willHide = true;

            //set hasMoved to true
            hasMoved = true;
        }
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
            if (catchable && !inventoryFull)
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
