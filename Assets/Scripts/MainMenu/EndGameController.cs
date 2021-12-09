using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    [Header("UI's")]
    public GameObject endGameUI;
    public GameObject brewingUI;

    [Header("Player Inventory")]
    public InventoryUI playerInventory;

    [Header("Controllers")]
    public GameController gameController;
    public FadeController fadeController;

    [Header("Player")]
    public ThirdPersonMovement player;

    [Header("Golden Critter")]
    public CritterData goldenCritter;

    private float t = 0.0f; //tracks the time the critter is moving
    private Color endGameUIAlpha; //exists to set the alpha of the end game UI
    private RectTransform Jar3Transform; //the transform of jar 3
    private Vector2 initialAnchor; //the initial position of jar 3
    private Vector3 initialScale; //the initial scale of jar 3


    // Start is called before the first frame update
    void Start()
    {
        //set jar3 transform
        Jar3Transform = (RectTransform)playerInventory.GetItemTransform(2);

        //store initial scale and position
        initialAnchor = Jar3Transform.anchoredPosition;
        initialScale = Jar3Transform.localScale;

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
        //start end of day fade
        gameController.FadeEndDayNoText();

        //disable the brewingUI and player
        Invoke("DisableBrewingUI", 1.1f);

        // stop player footsteps and music
        player.GetComponent<AudioSource>().enabled = false;

        //set jar3 transform
        Jar3Transform = (RectTransform)playerInventory.GetItemTransform(2);

        //raise the golden critter up
        StartCoroutine(RaiseGolden());

        // enable end game UI
        //endGameUI.SetActive(true);

        //gameController.EndOfDay();
    }


    //if the player has the golden critter, start end game sequence
    public bool PlayerHasGoldenCritter()
    {
        // check inventory for golden critter
        foreach(Data item in playerInventory.inventory.items)
        {
            // has golden bug
            if(item && item.tier == 4 && item is CritterData)
            {
                EndGame();
                return true;
            }
        }
        return false;

    }


    //disables brewingUI and moves golden critter to the middle of the inventory
    private void DisableBrewingUI()
    {
        //empty player inventory
        for (int i = 0; i < 5; i++)
        {
            //remove items slot by slot
            playerInventory.RemoveFromInventory(i);
        }

        //add the golden critter to slot 3
        playerInventory.AddToInventorySlot(2, goldenCritter);

        //stop music and sound effects
        gameController.musicBrewing.Stop();
        gameController.cauldronBubble.Stop();

        //disable UI
        brewingUI.SetActive(false);
    }


    //raises the golden critter up to the middle of the screen
    IEnumerator RaiseGolden()
    {
        //until time is complete
        while (t < 1.0f)
        {
            //set the time variable
            t += 0.2f * Time.deltaTime;

            //raise the golden critter image
            Jar3Transform.anchoredPosition += new Vector2(0.0f, 40.0f * Time.deltaTime);

            //increase the size of the golden critter image
            Jar3Transform.localScale += new Vector3(0.3f * Time.deltaTime, 0.3f * Time.deltaTime, 0.0f);

            //yield the coroutine until next frame
            yield return null;
        }

        //reset timer
        t = 0.0f;

        //fade in and out of black screen
        fadeController.AddState(FadeState.FadeIn, 2.0f);
        fadeController.AddState(FadeState.Wait, 2.0f);
        fadeController.AddState(FadeState.FadeOut, 2.0f);
        fadeController.StartActions();

        //wait 2 seconds
        while (t < 1.0f)
        {
            //set the time variable
            t += 0.4f * Time.deltaTime;

            //yield the coroutine until next frame
            yield return null;
        }

        //reset timer
        t = 0.0f;

        //set the endGameUI active
        endGameUI.SetActive(true);

        //reset the jar to initial conditions
        Jar3Transform.anchoredPosition = initialAnchor;
        Jar3Transform.localScale = initialScale;

        //end the coroutine
        yield break;
    }
}
