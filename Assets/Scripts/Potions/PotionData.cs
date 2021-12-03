using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "DataTemplates/PotionData")]
public class PotionData : Data
{
    [Header("Potion Visual Effects")]
    public float effectsIntensity; //how intense the visual effects are
    [Header("Recipe")]
    public Names critter1; //the first bug that makes this potion
    public Names critter2; //the second bug that makes this potion
}
