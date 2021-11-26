using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // turns on and off player movement
    bool inputsActive = true;

    // Update is called once per frame
    void Update()
    {
        if(!inputsActive)
        {
            return;
        }

        // grabbing raw axis to feed a normalized direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // if player is moving
        if (direction.magnitude >= 0.1f)
        {
            // for camera rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // when the player is facing a direction, input is based on the axis relative to the camera not the playermodel
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // simply... move
            controller.SimpleMove(moveDir.normalized * speed);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            EnableCursorLock();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            DisableCursorLock();
        }
    }

    // start is called once before the first update frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //if the current scene is brewer, lock the cursor
        if (SceneManager.GetActiveScene().name == "Brewer")
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EnableCursorLock()
    {
        // unlocking cursor state to be able to interact with the UI (for now)
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void DisableCursorLock()
    {
        // locking cursor state to be able to play game normally (for now)
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnableMovement()
    {
        inputsActive = true;
    }

    public void DisableMovement()
    {
        inputsActive = false;
    }
}