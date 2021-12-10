using UnityEngine;

//public list of movement patterns
public enum Movements { None, Circle, Oval, Figure8, Waypoint };

/// <summary>
/// CritterData is used for calculated technical details like movementspeeds, and detection distances.
/// </summary>

[CreateAssetMenu(menuName = "DataTemplates/CritterData")]

public class CritterData : Data
{
    [Header("Critter Behaviours")]
    public Movements movementPath; //the movement path this critter follows
    public float movementPathScale = 1.0f; //the scaling factor of the critter's preset path
    public float movementSpeed = 1.0f; //the speed the critter moves at
    public float responseSpeed = 1.0f; //the speed the critter moves when responding to the player
    public float detectionDistance = 0.0f; //the distance the critter can detect the player at, 0 for no detection
}
