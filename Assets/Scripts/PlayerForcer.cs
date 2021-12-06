using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForcer : MonoBehaviour
{
    public Vector3 initialVelocity;


    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void ApplyForce()
    {
        velocity = initialVelocity;
    }
}
