using UnityEngine;

public class ForcerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerForcer>().ApplyForce();
        }
    }
}
