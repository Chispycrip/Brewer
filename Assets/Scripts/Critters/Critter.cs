﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour
{
    protected CritterData data; //the object holding all of this critter's data
    protected bool catchable = true; //if this critter is currently catchable, false if inactive
    protected string state; //the current behavioural state of the critter
    public bool inventoryFull = false; //if the inventory is full


    //Init is called upon instantiation by the spawnpoint 
    public virtual void Init()
    {
        //overwritten by subclasses
    }


    //calls required behaviour on updates
    protected virtual void OnUpdate(float deltaTime)
    { 
        //overwritten by subclasses
    }


    //change behaviour to respond to player's actions
    protected virtual void RespondToPlayer()
    { 
        //overwritten by subclasses
    }


    //performs idle movement behaviour
    protected void IdleMovement(float deltaTime)
    { 
        //to be implemented in Beta
    }


    //returns if critter is catchable
    public bool IsCaught()
    {
        return catchable;
    }


    //sets the critterData
    public void SetData(CritterData cData)
    {
        data = cData;
    }


    //returns the critterData
    public CritterData GetData()
    {
        return data;
    }


    //called when the critter detects a collision with the net
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Net"))
        {
            //get script from collider

            if (catchable == true && inventoryFull == false)
            {
                //call net event true

                //destroy critter
                Destroy(gameObject);
            }
            else
            { 
                //call net event false
            }
        }
    }

}
