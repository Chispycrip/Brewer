using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalUI : MonoBehaviour
{
    public Journal journal; //the attached journal
    public GameObject journalCanvas; //the canvas holding the journal
    public GameObject[] lockedSprites; //the sprites for locked recipes
    public GameObject[] unlockedSprites; //the sprites for unlocked recipes
    
    // Start is called before the first frame update
    void Start()
    {
        //set all locked sprites active
        foreach (GameObject go in lockedSprites)
        {
            go.SetActive(true);
        }

        //set all unlocked sprites inactive
        foreach (GameObject go in unlockedSprites)
        {
            go.SetActive(false);
        }

        //set golden critter sprite active from the start
        lockedSprites[5].SetActive(false);
        unlockedSprites[5].SetActive(true);
    }


    //sets the journal canvas active and updates recipes
    public void OpenJournal()
    {
        for (int i=0;i<journal.recipeMask.Length;i++)
        {
            if (journal.recipeMask[i] == true)
            {
                lockedSprites[i].SetActive(false);
                unlockedSprites[i].SetActive(true);
            }
            else
            {
                lockedSprites[i].SetActive(true);
                unlockedSprites[i].SetActive(false);
            }
        }

        //set journal canvas active
        journalCanvas.SetActive(true);
    }


    //sets the journal canvas inactive
    public void CloseJournal()
    {
        journalCanvas.SetActive(false);
    }
}
