using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public list of trait options
public enum Traits { Speed, Stealth, Golden };

public class Data : ScriptableObject
{
    public enum Names { None, Speed1A, Speed1B, Speed2A, Speed2B, Speed3, Stealth1A, Stealth1B, Stealth2A, Stealth2B, Stealth3, Golden, SpeedPotion1, SpeedPotion2, StealthPotion1, StealthPotion2, GoldenPotion}
    
    [Header("Basic Info")]
    public Names typeName; //name of this item
    public int tier; //tier 1-4, golden is 4
    public Traits trait; //speed, stealth or golden
    public string description; //brief flavour text

    [Header("Appearance")]
    public Sprite icon; //2D image for UI
    public Color spriteColour = Color.white; //the colour the sprite will appear, defaulted to white
    public GameObject model; //3D model
}
