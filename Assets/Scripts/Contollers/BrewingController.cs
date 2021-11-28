using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingController : MonoBehaviour
{
    public GameObject cauldronUI;
    public GameObject cauldronInventoryUI;
    public GameObject endDayUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void StartNewDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // deactivate caudron UI
        cauldronInventoryUI.SetActive(false);
        cauldronUI.SetActive(false);
    }

    //swaps to the brewing UI
    public void EndOfDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // activate caudron UI
        cauldronInventoryUI.SetActive(true);
        cauldronUI.SetActive(true);
    }

    public void ContinueDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // deactivate caudron UI
        cauldronInventoryUI.SetActive(false);
        cauldronUI.SetActive(false);
    }

    // collision trigger for day end collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endDayUI.SetActive(true);
        }
    }
}
