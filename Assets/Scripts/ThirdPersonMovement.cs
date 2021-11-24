using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ThirdPersonMovement is the script necessary for being able to move our character in the world.
/// This is the case of us not using Assets for a ThirdPersonController, and making one ourselves.
/// </summary>
public class ThirdPersonMovement : MonoBehaviour
{
    // used to attach the player controller and main camera
    public CharacterController controller;
    public Transform cam;

    // public speed variable mess around with
    public float speed = 4f;

    // rigidbody for player
    Rigidbody rb;

    // camera / player rotation stuff
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        // grabbing raw axis to feed a normalized direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // if player is moving
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.SimpleMove(moveDir.normalized * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    // start is called once before the first update frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}