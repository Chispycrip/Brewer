using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Timer timer;
    public GameObject player;
    public CritterController critterControl;
    public GameObject cauldronUI;
    public GameObject cauldronInventoryUI;
    public GameObject endDayUI;

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    
    //Start is called before the first frame update
    void Start()
    {
        //set player start position
        playerStartPos = player.transform.position;
        playerStartRot = player.transform.rotation;

        //start will be used to display any opening menu/animation desired//

        //attach the player to the critter controller
        critterControl.player = player;

        //Begin the first day
        StartNewDay();
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
    }


    //swaps to the brewing UI
    public void EndOfDay()
    {
        //clear critters
        critterControl.EndOfDay();

        //play any day ending animations/transistions//

        // deactivate endDayUI
        endDayUI.SetActive(false);

        // activate caudron UI
        cauldronInventoryUI.SetActive(true);
        cauldronUI.SetActive(true);
    }

    public void ContinueDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // deactivate caudron UI
        cauldronInventoryUI.SetActive(false);
        cauldronUI.SetActive(false);
    }

    // collision trigger for day end collider
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            endDayUI.SetActive(true);
        }
    }

}
