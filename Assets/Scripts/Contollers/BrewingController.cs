using UnityEngine;

/// <summary>
/// The BrewingController is used to activate the brewing UI once the player collides with the gameObject.
/// There is a gameObject in the world that the player bumps into, and when confirmed, these methods will run.
/// </summary>
public class BrewingController : MonoBehaviour
{
    [Header("UI's")]
    /*!< The BrewingUI used for crafting/brewing */
    public GameObject brewUI;
    /*!< The endDayUI used for bringing up the end of day screen */
    public GameObject endDayUI;
    /*!< The JournalUI canvas brought up when the player requests it */
    public GameObject journalUI; 

    // being able to reference the player component from ThirdPersonMovement script
    [Header("Player")]
    public ThirdPersonMovement player;

    // endGameController used for the endGame screen
    [Header("Controllers")]
    public EndGameController endGameController;

    // Start is called before the first frame update
    void Start()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // deactivate caudron UI
        brewUI.SetActive(false);

        // disable player movement and cursor lock
        player.EnableCursorLock();
        player.DisableMovement();
    }

    // used to begin a new day
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

    // if the player does not wish to brew yet
    public void ContinueDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // deactivate caudron UI
        brewUI.SetActive(false);

        // enable player movement and cursor lock
        player.DisableCursorLock();
        player.EnableMovement();

        // show player
        player.transform.Find("Brewer_Character").gameObject.SetActive(true);
    }

    // if the player does choose to end the day
    private void EnableEndDayUI()
    {
        endDayUI.SetActive(true);

        // disable player movement and cursor lock
        player.EnableCursorLock();
        player.DisableMovement();

        // stop player footsteps
        player.gameObject.GetComponent<AudioSource>().enabled = false;
    }

    // simply enabling the JournalUI
    public void EnableJournalUI()
    {
        journalUI.SetActive(true);
    }

    // collision trigger for day end collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // if hasn't got golden critter start brew menu
            if (!endGameController.PlayerHasGoldenCritter())
            {
                EnableEndDayUI();
            }
            
        }
    }
}
