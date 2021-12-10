using UnityEngine;

public class PlayerCatcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerForcer>().StopPlayer();
        }
    }


    //triggered when the player leaves the camp
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //find the golden critter and start its movement cycle
            GameObject golden = GameObject.FindGameObjectWithTag("Golden");
            golden.GetComponent<GoldenCritter>().PlayerLeftCamp();
        }
    }
}
