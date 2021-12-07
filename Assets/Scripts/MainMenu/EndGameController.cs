using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [Header("UI's")]
    public GameObject endGameUI;


    [Header("Player Inventory")]
    public InventoryUI playerInventory;

    [Header("Controllers")]
    public GameController gameController;

    [Header("Player")]
    public ThirdPersonMovement player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Continue()
    {
        // deactivate endGameUI
        endGameUI.SetActive(false);

        // fade out and start day
        //gameController.fadeToBlackScreen.AddState(FadeState.FadeIn, 1.0f);
        //gameController.fadeToBlackScreen.AddState(FadeState.FadeOut, 1.0f);
        //gameController.fadeToBlackScreen.StartActions();
        gameController.FadeStartDay();
    }
    public void Quit()
    {
        Application.Quit();
    }

    void EndGame()
    {
        // enable end game UI
        endGameUI.SetActive(true);
        // disable player movement and cursor lock
        player.EnableCursorLock();
        player.DisableMovement();
    }

    public bool PlayerHasGoldenCritter()
    {
        // check inventory for golden critter
        foreach(Data item in playerInventory.inventory.items)
        {
            // has golden bug
            if(item && item.tier == 4)
            {
                EndGame();
                return true;
            }
        }
        return false;

    }
}
