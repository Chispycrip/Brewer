using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNet : MonoBehaviour
{
    public InventoryUI playerInventory = null;
    public ThirdPersonMovement player = null;
    public Animator animator = null;

    private BoxCollider netCollider = null;

    bool swinging = false;


    // Start is called before the first frame update
    void Start()
    {
        netCollider = GetComponent<BoxCollider>();
    }   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.Locked)
        {
            StartSwing();
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Swing") && animator.GetBool("Swinging"))
        {
            // flags swing as active
            swinging = true;
            // flags swing to exit at end
            animator.SetBool("Swinging", false);
        }
        else if (swinging && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            StopSwing();
        }
    }

    void StartSwing()
    {

        animator.SetBool("Walking", false);

        // start unity swing animation
        animator.SetBool("Swinging", true);
        // turn on net collider
        netCollider.enabled = true;
        if(player)
        {
            // disable player movement while swinging
            player.DisableMovement();
            // enable collider for speed critter response
            player.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void StopSwing()
    {
        swinging = false;

        // turn off net collider
        netCollider.enabled = false;
        if (player)
        {
            // disable player movement while swinging
            player.EnableMovement();
            // disable collider for speed critter response
            player.GetComponent<BoxCollider>().enabled = false;
        }
    }
    // Add critter to inventory
    public void CatchCritter(Critter critter)
    {
        playerInventory.AddToInventory(critter.GetData());
    }

}
