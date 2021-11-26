using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNet : MonoBehaviour
{
    public Inventory playerInventory = null;

    private Animator animator = null;
    private ThirdPersonMovement player = null;
    private BoxCollider netCollider = null;
  

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = transform.parent.gameObject.GetComponent<ThirdPersonMovement>();
        netCollider = transform.Find("NetCollider").GetComponent<BoxCollider>();
    }   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartSwing();
        }
    }

    void StartSwing()
    {
        // start unity swing animation
        animator.SetBool("SwingNet", true);
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
        // end unity swing animation
        animator.SetBool("SwingNet", false);
        // bring net into idle state
        animator.SetBool("ResetNet", true);
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
