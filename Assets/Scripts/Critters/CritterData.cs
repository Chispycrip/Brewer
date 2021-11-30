using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//public list of movement patterns
public enum Movements { None, Circle, Figure8, Waypoint };

[CreateAssetMenu(menuName = "DataTemplates/CritterData")]

public class CritterData : Data
{
    [Header("Critter Behaviours")]
    public Movements movementPath; //the movement path this critter follows
    public float wanderDistance; //the distance the critter can wander from spawn, 0 for immobile
    public float detectionDistance; //the distance the critter can detect the player at, 0 for no detection
    public float movementSpeed; //the speed the critter moves at
}
