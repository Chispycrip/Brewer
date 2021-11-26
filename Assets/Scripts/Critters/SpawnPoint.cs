using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public CritterData data; //all of the data needed to create a critter
    [Header("Spawn Rotation")]
    public float x; //the rotation around the x axis
    public float y; //the rotation around the y axis
    public float z; //the rotation around the z axis


    //Temporary method for triggering spawns, will need to be handled another way for daily resets post alpha
    //Start is called before the first frame update
    void Start()
    {
        //SpawnCritter();
    }


    //spawns a critter based on the stored data and rotation
    public void SpawnCritter()
    {
        //set the spawnpoint inactive
        gameObject.SetActive(false);
        
        //clone critter model at the spawnpoint's position and stored rotation
        GameObject critter = Instantiate(data.model, gameObject.transform.position, Quaternion.Euler(x,y,z));

        //set the critter's material
        critter.GetComponent<Renderer>().material = data.modelMaterial;

        //create bool to hold the script
        Critter script;

        //create critter script based on trait
        if (data.trait == "Speed")
        {
            //add SpeedCritter script
            script = critter.AddComponent<SpeedCritter>();
        }
        else if (data.trait == "Stealth")
        {
            //add StealthCritter script
            script = critter.AddComponent<StealthCritter>();
        }
        else 
        {
            //add GoldenCritter script
            //to be added
            script = critter.AddComponent<SpeedCritter>();
        }

        //add data file and run initial setup
        script.SetData(data);
        script.Init();
    }
}
