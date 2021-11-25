using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "DataTemplates/PotionData")]
public class PotionData : Data
{
    [Header("Potion Visual Effects")]
    public float effectsIntensity; //how intense the visual effects are
}
