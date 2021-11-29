﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Critter : MonoBehaviour
{
    protected CritterData data; //the object holding all of this critter's data
    protected bool catchable = true; //if this critter is currently catchable, false if inactive
    protected string state; //the current behavioural state of the critter
    public bool inventoryFull = false; //if the inventory is full

    protected Vector3 initialPos; //the starting position of the critter
    protected Vector3 previousPos; //the position of the critter last frame it moved


    //Init is called upon instantiation by the spawnpoint 
    public virtual void Init()
    {
        //overwritten by subclasses
    }


    //Update is called once per frame
    public void Update()
    {
        OnUpdate();
    }


    //calls required behaviour on updates
    protected virtual void OnUpdate()
    { 
        //overwritten by subclasses
    }


    //change behaviour to respond to player's actions
    protected virtual void RespondToPlayer()
    { 
        //overwritten by subclasses
    }


    //performs idle movement behaviour
    protected void IdleMovement()
    {
        //create offset vector that determines how far from initialPos the critter is
        Vector3 offset;
        
        //make movement based on stored data
        switch (data.movementPath)
        {
            //movement path 1 is a figure-8
            case 1:
                {
                    //the scale changes the curve of the figure-8 for a better looking shape
                    float scale = 2.0f / (3.0f - (float)Math.Cos((2.0f * Time.time)));

                    //the x and y positions are set and then assigned to a vector
                    float x = scale * (float)Math.Cos(Time.time);
                    float z = scale * (float)Math.Sin(2.0f * Time.time) / 2.0f;
                    offset = new Vector3(x, 0.0f, z);

                    break;
                }
            //movement path 2 is a circle
            case 2:
                {
                    //the x and y positions are set and then assigned to a vector
                    float x = (float)Math.Cos(Time.time);
                    float z = (float)Math.Sin(Time.time);
                    offset = new Vector3(x, 0.0f, z);

                    break;
                }
            //no movement path
            default:
                {
                    //set offset to zero
                    offset = Vector3.zero;

                    break;
                }
        }

        //apply the correct position relative to the starting position of the critter
        gameObject.transform.position = (initialPos + offset);

        //get the direction the critter is moving
        Vector3 currentDirection = gameObject.transform.position - previousPos;

        //remove error message from unity console
        if (currentDirection != Vector3.zero)
        { 
            //set the critter to face the direction it is moving
            gameObject.transform.rotation = Quaternion.LookRotation(currentDirection);
        }


            //end movement by updating previous position to current position
            previousPos = gameObject.transform.position;
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
                PlayerNet net = other.transform.parent.gameObject.GetComponent<PlayerNet>();
                net.CatchCritter(this);

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
