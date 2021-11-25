using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data : ScriptableObject
{
    [Header("Basic Info")]
    public string typeName; //name of the potion type
    public int tier; //tier 1-4, golden is 4
    public string trait; //speed, stealth or golden
    public string description; //brief flavour text

    [Header("Appearance")]
    public Sprite icon; //2D image for UI
    public Color spriteColour = Color.white; //the colour the sprite will appear, defaulted to white
    public GameObject model; //3D model
    public Material modelMaterial; //material used on the model
}
