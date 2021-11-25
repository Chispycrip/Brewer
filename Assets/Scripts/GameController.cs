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

        //start will be used to display any opening menu/animation desired
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
}
