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
        animator.SetBool("SwingNet", true);
        netCollider.enabled = true;
        if(player)
        {
            player.DisableMovement();
            player.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void StopSwing()
    {
        animator.SetBool("SwingNet", false);
        animator.SetBool("ResetNet", true);
        netCollider.enabled = false;
        if (player)
        {
            player.EnableMovement();
            player.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void CatchCritter(Critter critter)
    {
        playerInventory.AddToInventory(critter.GetData());
    }

}
