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
    public float movementPathScale = 1.0f; //the scaling factor of the critter's preset path
    public float movementSpeed = 1.0f; //the speed the critter moves at
    public float detectionDistance = 0.0f; //the distance the critter can detect the player at, 0 for no detection
}
