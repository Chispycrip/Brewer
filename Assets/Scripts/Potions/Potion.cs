using UnityEngine;

/// <summary>
/// Potion class that is the parent for other potions.
/// </summary>
public class Potion
{
    protected PotionData data; //the object holding all of this potion's data

    //called upon instantiation
    public virtual void Init()
    {
        //overwritten by subclasses
    }

    //consumes the potion and triggers its effects in every critter on the map
    public virtual void Consume()
    { 
        //overwritten by subclass
    }

    // Set potion data
    public void SetData(PotionData pData)
    {
        data = pData;
    }

}
