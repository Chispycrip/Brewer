using UnityEngine;

/// <summary>
/// The CritterController is used for when spawning critters is needed.
/// When the day started, they are initialized at their spawnpoints.
/// When the day ended, they are destroyed.
/// </summary>
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

        //find the golden critter and attach the player
        GameObject golden = GameObject.FindGameObjectWithTag("Golden");
        golden.GetComponent<GoldenCritter>().playerObject = player;
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
