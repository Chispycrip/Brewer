using UnityEngine;

/// <summary>
/// The Data needed for each potion type.
/// </summary>

[CreateAssetMenu(menuName = "DataTemplates/PotionData")]
public class PotionData : Data
{
    [Header("Potion Visual Effects")]
    public float effectsIntensity; //how intense the visual effects are
    [Header("Recipe")]
    public Names critter1; //the first bug that makes this potion
    public Names critter2; //the second bug that makes this potion
}
