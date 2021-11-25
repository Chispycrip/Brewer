using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Timer timer;
    public GameObject player;

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    
    //Start is called before the first frame update
    void Start()
    {
        //set player start position
        playerStartPos = player.transform.position;
        playerStartRot = player.transform.rotation;

        //start will be used to display any opening menu/animation desired//

        //Begin the first day
        StartNewDay();
    }


    //sets up all objects in their initial positions for a new day cycle
    public void StartNewDay()
    {
        //set player to start position
        player.transform.position = playerStartPos;
        player.transform.rotation = playerStartRot;
        
        //find all spawners
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("SpawnPoint");

        //spawn critters at every spawnpoint
        foreach (GameObject s in spawners)
        {
            s.GetComponent<SpawnPoint>().SpawnCritter();
        }

        //start timer
        timer.StartTimer();
    }


    //swaps to the brewing UI
    public void EndOfDay()
    {
        //play any day ending animations/transistions//

        //find all critters
        GameObject[] speeds = GameObject.FindGameObjectsWithTag("Speed");
        GameObject[] stealths = GameObject.FindGameObjectsWithTag("Stealth");
        GameObject golden = GameObject.FindGameObjectWithTag("Golden");

        //destroy all speed critters
        foreach (GameObject s in speeds)
            Destroy(s.gameObject);

        //destroy all stealth critters
        foreach (GameObject s in stealths)
            Destroy(s.gameObject);

        //destroy golden critter
        Destroy(golden);
    }

}
