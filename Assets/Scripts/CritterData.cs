using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "DataTemplates/CritterData")]
public class CritterData : ScriptableObject
{
    public string typeName; //name of the critter type
    public int tier; //tier 1-4, golden is 4
    public string trait; //speed, stealth or golden
    public string description; //brief flavour text

    public Image icon; //2D image for UI
    public GameObject model; //3D model
    public Material modelMaterial; //material used on the model

    public int movementPath; //the id of the movement path this bug follows
    public float wanderDistance; //the distance the critter can wander from spawn, 0 for immobile
    public float detectionDistance; //the distance the critter can detect the player at, 0 for no detection
}
