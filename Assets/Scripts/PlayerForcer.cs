using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForcer : MonoBehaviour
{
    public Vector3 initialVelocity;
    public Vector3 gravity;

    CharacterController controller;

    float yVelocity;
    bool inFlight = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ThirdPersonMovement>().controller;
    }

    // Update is called once per frame
    void Update()
    {
        if(inFlight)
        {
            UpdatePosition();
        }
        else
        {
            yVelocity = 0.0f;
        }
    }

    void UpdatePosition()
    {
        yVelocity += gravity.y * Time.deltaTime;
        transform.position = new Vector3(
            transform.position.x+initialVelocity.x*Time.deltaTime,
            transform.position.y + yVelocity * Time.deltaTime,
            transform.position.z
        );
    }

    public void ApplyForce()
    {
        inFlight = true;
        yVelocity = initialVelocity.y;
        UpdatePosition();
    }

    public void StopPlayer()
    {
        inFlight = false;
    }
}
