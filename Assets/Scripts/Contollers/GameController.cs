using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Timer timer;
    public GameObject player;
    public CritterController critterControl;
    public BrewingController brewingController;
    public ConsumptionController consumptionController;

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    
    //Start is called before the first frame update
    void Start()
    {
        //set player start position
        playerStartPos = player.transform.position;
        playerStartRot = player.transform.rotation;

        //start will be used to display any opening menu/animation desired//

        //initialise the critter controller
        critterControl.Init(player);

        //Begin the first day
        StartNewDay();
    }

    // Update is called once per frame
    private void Update()
    {
        // check if timer running (i.e. if 3 mins is up)
        if(!timer.timerIsRunning)
        {
            // end the day
            EndOfDay();
        }
    }

    //sets up all objects in their initial positions for a new day cycle
    public void StartNewDay()
    {
        //set up critters
        critterControl.StartNewDay();
        
        //set player to start position
        player.transform.position = playerStartPos;
        player.transform.rotation = playerStartRot;

        //start timer
        timer.StartTimer();

        // show timer text
        timer.timerText.enabled = true;

        // update brewing controller
        brewingController.StartNewDay();
    }


    //swaps to the brewing UI
    public void EndOfDay()
    {
        //clear critters
        critterControl.EndOfDay();

        //play any day ending animations/transistions//

        //clear potion effects
        consumptionController.EndOfDay();
        
        // update brewing controller
        brewingController.EndOfDay();

        // hide timer text
        timer.timerText.enabled = false;
    }
}
