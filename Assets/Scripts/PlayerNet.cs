﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNet : MonoBehaviour
{
    private Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("SwingNet",true);
        }
    }

    void StopSwing()
    {
        animator.SetBool("SwingNet", false);
        animator.SetBool("ResetNet", true);
    }


}
