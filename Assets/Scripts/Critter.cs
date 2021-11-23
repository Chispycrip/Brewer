﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour
{
    protected CritterData data; //the object holding all of this critter's data
    protected bool catchable; //if this critter is currently catchable, false if inactive
    protected string state; //the current behavioural state of the critter


    //Init is called upon instantiation by the spawnpoint 
    public virtual void Init()
    {
        //overwritten by subclasses
    }


    //Update is called once per frame
    public void Update(float deltaTime)
    {
        //call update behaviour
        OnUpdate(deltaTime);
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
        //if the bug is caught, send it to the journal
        if (catchable)
        {
            AddToJournal();
        }
        
        return catchable;
    }


    //returns the critterData
    public CritterData GetData()
    {
        return data;
    }


    //adds critter to the journal
    private void AddToJournal()
    { 
        //to be implemented in Beta
    }
}