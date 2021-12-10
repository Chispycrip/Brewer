using UnityEngine;

/// <summary>
/// SpawnPoints is used to Spawn the Waypoints, and the Critters.
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    public CritterData data; //all of the data needed to create a critter
    [Header("Spawn Rotation")]
    public float x; //the rotation around the x axis
    public float y; //the rotation around the y axis
    public float z; //the rotation around the z axis

    public Vector3[] waypoints; //array of all attached waypoint locations //DEBUG// public during testing
    public Vector3[] dodgepoints; //array of all attached dodgepoint locations - only for Speed/Golden
    public Vector3[] hidepoints; //array of all attached hidepoint locations - only for Stealth/Golden

    //spawns a critter based on the stored data and rotation
    public void SpawnCritter()
    {
        //if this spawnpoint has data
        if (data)
        {
            //clone critter model at the spawnpoint's position and stored rotation
            GameObject critter = Instantiate(data.model, gameObject.transform.position, Quaternion.Euler(x, y, z));

            //create bool to hold the script
            Critter script;

            //set up waypoints
            SetupWaypoints();

            //create critter script based on trait
            if (data.trait == Traits.Speed)
            {
                //add SpeedCritter script
                script = critter.AddComponent<SpeedCritter>();

                //add dodgepoints array to critter
                script.GetComponent<SpeedCritter>().SetDodgepoints(dodgepoints);
            }
            else if (data.trait == Traits.Stealth)
            {
                //add StealthCritter script
                script = critter.AddComponent<StealthCritter>();

                //add hidepoints array to critter
                script.GetComponent<StealthCritter>().SetHidepoints(hidepoints);
            }
            else
            {
                //add GoldenCritter script
                script = critter.AddComponent<GoldenCritter>();

                //add hidepoints array to critter
                script.GetComponent<GoldenCritter>().SetHidepoints(hidepoints);
            }

            //add data file
            script.SetData(data);

            //initialise the critter
            script.Init();

            //add waypoints
            script.SetWaypoints(waypoints);

            //set the spawnpoint inactive
            gameObject.SetActive(false);
        }
    }

    //sets up the waypoint system
    private void SetupWaypoints()
    {
        //set transform to variable
        Transform transform = gameObject.transform;

        //check if this spawnpoint has any children
        if (transform.childCount > 0)
        {
            //iterate through each child, adding all object positions to the waypoints, dodgepoints and hidepoints arrays
            for (int i = 0; i < transform.childCount; i++)
            {
                //set child to variable
                Transform child = transform.GetChild(i);

                //if there is a child object named waypoints, set the position of every child it has to the waypoints array
                if (child.name == "Waypoints")
                {
                    //set size of waypoints array
                    waypoints = new Vector3[child.childCount];

                    //add all children into waypoints
                    for (int j = 0; j < child.childCount; j++)
                    {
                        waypoints[j] = child.GetChild(j).position;
                    }
                }
                //if there is a child object named dodgepoints, set the position of every child it has to the dodgepoints array
                else if (child.name == "Dodgepoints")
                {
                    //set size of dodgepoints array
                    dodgepoints = new Vector3[child.childCount];

                    //add all children into dodgepoints
                    for (int j = 0; j < child.childCount; j++)
                    {
                        dodgepoints[j] = child.GetChild(j).position;
                    }
                }
                //if there is a child object named hidepoints, set the position of every child it has to the hidepoints array
                else if (child.name == "Hidepoints")
                {
                    //set size of hidepoints array
                    hidepoints = new Vector3[child.childCount];

                    //add all children into hidepoints
                    for (int j = 0; j < child.childCount; j++)
                    {
                        hidepoints[j] = child.GetChild(j).position;
                    }
                }
            }
        }
    }
}
