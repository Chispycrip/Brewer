using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "DataTemplates/CritterData")]
public class CritterData : Data
{
    [Header("Critter Behaviours")]
    public int movementPath; //the id of the movement path this bug follows
    public float wanderDistance; //the distance the critter can wander from spawn, 0 for immobile
    public float detectionDistance; //the distance the critter can detect the player at, 0 for no detection
}
