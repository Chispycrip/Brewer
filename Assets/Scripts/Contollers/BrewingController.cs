using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingController : MonoBehaviour
{
    public GameObject cauldronUI;
    public GameObject cauldronInventoryUI;
    public GameObject workbenchInventoryUI;
    public GameObject endDayUI;

    public ThirdPersonMovement player;

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
        workbenchInventoryUI.SetActive(false);

        // disable player movement and cursor lock
        player.DisableCursorLock();
        player.EnableMovement();
    }

    //swaps to the brewing UI
    public void EndOfDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // activate caudron UI
        cauldronInventoryUI.SetActive(true);
        cauldronUI.SetActive(true);
        workbenchInventoryUI.SetActive(true);
    }

    public void ContinueDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // deactivate caudron UI
        cauldronInventoryUI.SetActive(false);
        cauldronUI.SetActive(false);
        workbenchInventoryUI.SetActive(false);

        // enable player movement and cursor lock
        player.DisableCursorLock();
        player.EnableMovement();
    }

    private void EnableEndDayUI()
    {
        endDayUI.SetActive(true);

        // disable player movement and cursor lock
        player.EnableCursorLock();
        player.DisableMovement();
    }

    // collision trigger for day end collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnableEndDayUI();
        }
    }
}
