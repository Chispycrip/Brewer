using UnityEngine;

/// <summary>
/// This script deals with the character movement, as well as camera movement within the world.
/// </summary>
public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Controller & Camera")]
    public CharacterController controller;
    public Transform cam;
    public Animator animator = null;

    [Header("Player Settings")]
    public float speed = 4f;

    [Header("Camera Settings")]
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // turns on and off player movement
    bool inputsActive = true;


    // Update is called once per frame
    void Update()
    {
        if (!inputsActive)
        {
            return;
        }

        // grabbing raw axis to feed a normalized direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // if player is moving
        if (direction.magnitude >= 0.1f && controller.isGrounded)
        {
            // start walking animation
            animator.SetBool("Walking", true);

            // for camera rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // when the player is facing a direction, input is based on the axis relative to the camera not the playermodel
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // simply... move
            controller.SimpleMove(moveDir * speed);

            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        // if the player is in the air for some reason
        // TODO - fix this, make it so I'm not duplicating the code
        else if(!controller.isGrounded)
        {
                // for camera rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // when the player is facing a direction, input is based on the axis relative to the camera not the playermodel
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // adding gravity so that player can fall
                moveDir += Physics.gravity;
                
                // falling, basically
                controller.SimpleMove(moveDir * speed);

            GetComponent<AudioSource>().Stop();
        }
        // when the player is stopped
        else
        {
            // stop walking animation
            animator.SetBool("Walking", false);

            GetComponent<AudioSource>().Stop();
        }

        // if Middle mouse button is pressed, enable the cursor for UI.
        if (Input.GetMouseButtonDown(2))
        {
            switch (Cursor.lockState)
            {
                case CursorLockMode.Confined:
                    DisableCursorLock();
                    break;
                case CursorLockMode.Locked:
                    EnableCursorLock();
                    break;
                case CursorLockMode.None:
                    DisableCursorLock();
                    break;
                default:
                    DisableCursorLock();
                    break;
            }
        }
    }

    // function names for CursorLock might actually be reversed by accident lol
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

    // player can move
    public void EnableMovement()
    {
        inputsActive = true;
    }

    // player can do the opposite of move
    public void DisableMovement()
    {
        inputsActive = false;

        // stop walking animation
        animator.SetBool("Walking", false);
    }
}