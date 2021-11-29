using System.Collections;
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
        //movement path 1 is a figure-8
        if (data.movementPath == 1)
        {
            //the scale curves in the figure-8 for a better looking shape
            float scale = 2.0f / (3.0f - (float)Math.Cos((2.0f * Time.time)));

            //the x and y positions are set and then assigned to a vector
            float x = scale * (float)Math.Cos(Time.time);
            float z = scale * (float)Math.Sin(2.0f * Time.time) / 2.0f;
            Vector3 translation = new Vector3(x, 0.0f, z);

            //the movement is applied relative to the starting position of the critter
            gameObject.transform.position = (initialPos + translation);
        }

        //get the direction the critter is moving
        Vector3 currentDirection = gameObject.transform.position - previousPos;

        //set the critter to face the direction it is moving
        gameObject.transform.rotation = Quaternion.LookRotation(currentDirection);

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
