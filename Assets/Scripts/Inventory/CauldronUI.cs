using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronUI : MonoBehaviour
{
    public PotionData data = null;


    //takes in the potion data
    public void SetPotion(PotionData pData)
    {
        data = pData;
    }


    //returns potion data
    public PotionData GetPotion()
    {
        return data;
    }


    //clears potion data
    public void ClearPotion()
    {
        data = null;
    }
}
