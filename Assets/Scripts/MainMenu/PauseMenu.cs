using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas; //the canvas with the pause menu

    private GameController gameController; //the game controller

    CursorLockMode cursorState; //if the mouse was locked or not when the game was paused

    
    //initialise variables
    public void Init(GameController gameControl)
    {
        //set the game controller
        gameController = gameControl;
    }


    //pauses the game and enables the menu
    public void OpenPauseMenu()
    {
        //stop time
        Time.timeScale = 0;

        //set canvas active
        pauseMenuCanvas.SetActive(true);

        //save original state of cursor
        cursorState = Cursor.lockState;

        //set cursor to unlocked
        Cursor.lockState = CursorLockMode.Confined;
    }


    //unpauses the game and disables the menu
    public void ClosePauseMenu()
    {
        //restart time
        Time.timeScale = 1;

        //set canvas inactive
        pauseMenuCanvas.SetActive(false);

        //reset cursor lock to state before pausing
        Cursor.lockState = cursorState;
    }


    //disables the pause menu then fades to end of day
    public void EndDay()
    {
        //close the pause menu
        ClosePauseMenu();

        //start the end of day fade out
        gameController.FadeEndDayNoText();
    }


    //quits program
    public void ExitGame()
    {
        Application.Quit();
    }
}
