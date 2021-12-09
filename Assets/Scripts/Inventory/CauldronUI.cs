using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronUI : MonoBehaviour
{
    public Cauldron cauldron; //the cauldron attached to this UI
    public PotionData data = null;
    public Material cauldronWater; //the material of the water in the cauldron
    public ParticleSystem bubbles; //the bubble particles in the cauldron
    public Color clearWater; //the default water colour

    public GameObject brewTotorial; // UI for brew tutorial

    //start is called before the first frame update
    public void Start()
    {
        //set the water material to the default colour
        cauldronWater.color = clearWater;
        
        //set the bubble colour to the default color with half the alpha
        var main = bubbles.main;
        Color halfAlpha = clearWater;
        halfAlpha.a /= 2;
        main.startColor = halfAlpha;
    }

    //takes in the potion data and changes the cauldron's water colour
    public void SetPotion(PotionData pData)
    {
        data = pData;

        //change the water material colour to match the potion colour
        cauldronWater.color = data.spriteColour;

        //set the bubble colour to the potion color with half the alpha
        var main = bubbles.main;
        Color halfAlpha = data.spriteColour;
        halfAlpha.a /= 2;
        main.startColor = halfAlpha;

        //clear all spawned bubbles
        bubbles.Clear();
    }


    //returns potion data
    public PotionData GetPotion()
    {
        return data;
    }


    //clears potion data
    public void ClearPotion()
    {
        brewTotorial.SetActive(false);

        data = null;

        //reset cauldron colour
        cauldronWater.color = clearWater;

        //set the bubble colour to the default color with half the alpha
        var main = bubbles.main;
        Color halfAlpha = clearWater;
        halfAlpha.a /= 2;
        main.startColor = halfAlpha;

        //clear all spawned bubbles
        bubbles.Clear();
    }
}
