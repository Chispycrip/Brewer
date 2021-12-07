using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    public Data[] data; //all unique critter and potion data files 
    public bool[] dataMask; //stores which data entries have been unlocked


    //takes in a data file and unlocks it
    public void AddToJournal(Data newData)
    {
        //iterate through the array of data until the name matches the given data
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].typeName == newData.typeName)
            {
                //the names match, set data to unlocked and break
                dataMask[i] = true;
                break;
            }
        }
    }
}
