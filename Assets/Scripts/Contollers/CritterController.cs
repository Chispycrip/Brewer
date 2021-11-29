using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterController : MonoBehaviour
{
    public GameObject player;
    private GameObject[] spawnpoints;

    //initial setup
    public void Init(GameObject playerObject)
    {
        //attach the player object
        player = playerObject;
        
        //find all spawners and store them
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    

    //spawn in all the critters to start a new day
    public void StartNewDay()
    {
       //set spawnpoints active then spawn critters
        foreach (GameObject s in spawnpoints)
        {
            s.SetActive(true);
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


    //destroy all the critters to end the day
    public void EndOfDay()
    {
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
