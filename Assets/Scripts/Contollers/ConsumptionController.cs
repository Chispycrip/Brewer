﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumptionController : MonoBehaviour
{
    public InventoryUI playerInventory;

    [Header("Potion Visual Effects")]
    public ParticleSystem stealthParticles;
    public ParticleSystem speedParticles;
    public Material playerMaterial;

    // Update is called once per frame
    void Update()
    {
        // check alpha numeric keys and consume potion slot
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ConsumePotion(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ConsumePotion(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ConsumePotion(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ConsumePotion(3);
        }

    }

    private void ConsumePotion(int slot)
    {
        // check inventory capacity is <= slot size
        if (slot < playerInventory.inventory.items.Length)
        {
            // get inventory data item
            Data item = playerInventory.inventory.items[slot];
            if (item && item is PotionData)
            {
                // if speed
                if(item.trait == Traits.Speed)
                {
                    // construct potion from potion data
                    SpeedPotion potion = new SpeedPotion();
                    potion.SetData((PotionData)item);

                    //set speed particles active
                    speedParticles.gameObject.SetActive(true);

                    // consume potion
                    potion.Consume();
                }
                else if (item.trait == Traits.Stealth) // if stealth
                {
                    // construct potion from potion data
                    StealthPotion potion = new StealthPotion();
                    potion.SetData((PotionData)item);

                    //set stealth particles active
                    stealthParticles.gameObject.SetActive(true);

                    // consume potion
                    potion.Consume();
                }

                // clear slot
                playerInventory.RemoveFromInventory(slot);
            }
        }
    }
}
