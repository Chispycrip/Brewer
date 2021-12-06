using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingController : MonoBehaviour
{
    [Header("UI's")]
    public GameObject brewUI;
    public GameObject endDayUI;
    public GameObject journalUI;

    [Header("Player")]
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
        brewUI.SetActive(false);

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
        brewUI.SetActive(true);
    }

    public void ContinueDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // deactivate caudron UI
        brewUI.SetActive(false);

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

    public void EnableJournalUI()
    {
        journalUI.SetActive(true);
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
