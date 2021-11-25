using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; //the attached inventory
    public ItemUI itemPrefab; //the prefab for ItemUI
    public Jar jarPrefab; //the prefab for Jar
    private Jar[] jars; //array of jars
    
    //Start is called before the first frame update
    void Start()
    {
        //create an array of jars the same size as the inventory
        jars = new Jar[inventory.items.Length];

        //instantiate and initialise all jars in array and create an ItemUI for each jar
        for (int i = 0; i < inventory.items.Length; i++)
        {
            jars[i] = Instantiate(jarPrefab, transform);
            jars[i].itemUI = Instantiate(itemPrefab, jars[i].transform);
            jars[i].itemUI.SetContents(inventory.items[i]);
            jars[i].Init(this, i, jars[i].itemUI);
        }
    }
}
