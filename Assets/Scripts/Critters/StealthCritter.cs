using UnityEngine;

/// <summary>
/// The StealthCritter derives from the Critter class and has its own behaviours.
/// If the player does not have a stealthpotion active, and it goes within a certain detection radius, the creature will flee.
/// </summary>
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
        state = States.Idle;

        //set catchable to inverse of hide state
        catchable = !willHide;

        //set tag
        gameObject.tag = "Stealth";

        //set previous and initial position
        initialPos = gameObject.transform.position;
        previousPos = initialPos;
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
            state = States.Idle;
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
        if (state != States.Inactive)
        {
            //if the player is nearby and the critter is not already hiding, respond to the player
            if (PlayerWithin() && state != States.Hiding)
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
            //if the critter is idle, continue its idle movement
            else if (state == States.Idle)
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
            catchable = true;
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
        transform.position = Vector3.MoveTowards(transform.position, hidepoints[hidepointIndex], Time.deltaTime * data.responseSpeed);

        //if the critter is at a hidepoint, increase hidepoint index
        if (transform.position == hidepoints[hidepointIndex])
        {
            hidepointIndex++;
        }

        //if the critter is at the last hidepoint, set it to inactive
        if (hidepointIndex == hidepoints.Length)
        {
            state = States.Inactive;
            gameObject.SetActive(false);
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
    }
}
