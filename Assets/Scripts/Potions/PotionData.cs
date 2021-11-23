using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "DataTemplates/PotionData")]
public class PotionData : ScriptableObject
{
    public string typeName; //name of the potion type
    public int tier; //tier 1-2, golden is 3
    public string trait; //speed, stealth or golden
    public string description; //brief flavour text

    public Image icon; //2D image for UI
    public GameObject model; //3D model
    public Material modelMaterial; //material used on the model
}
