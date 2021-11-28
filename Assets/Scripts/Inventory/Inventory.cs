using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Data[] items; //the array of items stored in this inventory
    public bool isPlayerInv; //if this is the player's inventory
    public int itemsStored = 0; //how many items are in this inventory

    public bool acceptsPotions = true; //if this Inventory accepts Potions
    public bool jarsVisibile = true; //if this Inventory has visible jars
}
