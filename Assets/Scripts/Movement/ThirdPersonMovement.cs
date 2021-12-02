using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Controller & Camera")]
    public CharacterController controller;
    public Transform cam;

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
            // for camera rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // when the player is facing a direction, input is based on the axis relative to the camera not the playermodel
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // simply... move
            controller.SimpleMove(moveDir * speed);
        }
        else
        {
            if (!controller.isGrounded)
            {
                // for camera rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // when the player is facing a direction, input is based on the axis relative to the camera not the playermodel
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                moveDir += Physics.gravity;

                controller.SimpleMove(moveDir * speed);
            }
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

    // start is called once before the first update frame
    void Start()
    {
        //controller = GetComponent<CharacterController>();

        //if the current scene is brewer, lock the cursor
        if (SceneManager.GetActiveScene().name == "Brewer")
        {
            DisableCursorLock();
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