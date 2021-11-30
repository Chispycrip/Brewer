using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleController : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        //find all spawners and store them
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        //spawn critters
        foreach (GameObject s in spawnpoints)
        {
            s.GetComponent<SpawnPoint>().SpawnCritter();
        }

        //find all stealth critters
        GameObject[] stealths = GameObject.FindGameObjectsWithTag("Stealth");

        //attach the player to each stealth critter
        foreach (GameObject s in stealths)
        {
            s.GetComponent<StealthCritter>().playerObject = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //trigger RespondToPlayer in every critter in scene
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //find all critters
            GameObject[] speeds = GameObject.FindGameObjectsWithTag("Speed");
            GameObject[] stealths = GameObject.FindGameObjectsWithTag("Stealth");
            GameObject golden = GameObject.FindGameObjectWithTag("Golden");

            //trigger all speed critters
            foreach (GameObject s in speeds)
                s.GetComponent<Critter>().RespondToPlayer();

            //trigger all stealth critters
            foreach (GameObject s in stealths)
                s.GetComponent<Critter>().RespondToPlayer();

            //trigger golden critter
            //golden.GetComponent<Critter>().RespondToPlayer();
        }
    }
}
