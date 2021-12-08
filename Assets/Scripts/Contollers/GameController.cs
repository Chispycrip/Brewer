using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public enum GameState
{
    StartingGame,
    Catching,
    Brewing,
    TransitionEndDay,
    TransitionStartDay
}

public class GameController : MonoBehaviour
{
    public Timer timer;

    [Header("Game Components")]
    public GameObject player;
    public InventoryUI playerInventory;
    public GameObject inventorySlots;
    public PlayerNet net;
    public Cauldron cauldron;
    public Journal journal;
    public Tutorial tutorialUI;

    [Header("Controllers")]
    public CritterController critterControl;
    public BrewingController brewingController;
    public ConsumptionController consumptionController;
    public FadeController fadeToBlackScreen;

    [Header("Visual Effects")]
    public CinemachineVirtualCamera cinVCam;
    public GameObject cauldronVCam;
    public CinemachineFreeLook cinTPCam;
    public GameObject postProcessingVolume;

    [Header("Audio")]
    public AudioSource musicBrewing;
    public AudioSource musicCatching;
    public AudioSource cauldronBubble;

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;

    private GameState state = GameState.StartingGame;

    //time variables for music fades
    private float inFade;
    private float outFade;

    //variable to make music start without delay on the first day
    bool firstDay = true;

    //Start is called before the first frame update
    void Start()
    {
        //set player start position
        playerStartPos = player.transform.position;
        playerStartRot = player.transform.rotation;

        //initialise the other controllers
        critterControl.Init(player);
        consumptionController.Init(journal);

        //add the journal to the net and cauldron
        net.journal = journal;
        cauldron.journal = journal;

        // hide player inventory
        playerInventory.gameObject.SetActive(false);
        inventorySlots.SetActive(false);

        // hide timer text
        timer.timerText.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        switch(state)
        {
            case GameState.StartingGame:
                break;
            case GameState.Catching:
                // check if timer running (i.e. if 3 mins is up)
                if (!timer.timerIsRunning)
                {
                    FadeEndDayText();
                }
                break;
            case GameState.TransitionEndDay:
                if(fadeToBlackScreen.IsDark())
                {
                    // set camera to over cauldron
                    cinVCam.MoveToTopOfPrioritySubqueue();
                    cauldronVCam.SetActive(true);

                    //set depth of field to close range
                    UpdateDepthOfField(1.75f, 2.4f);

                    // hide player
                    player.gameObject.SetActive(false);

                    // end the day
                    EndOfDay();
                }
                if(!fadeToBlackScreen.IsActive())
                {
                    state = GameState.Brewing;

                }
                break;
            case GameState.TransitionStartDay:
                if(fadeToBlackScreen.IsDark())
                {
                    // set camera to track player
                    cinTPCam.MoveToTopOfPrioritySubqueue();

                    //set depth of field to far range
                    UpdateDepthOfField(12.5f, 1.1f);

                    //set player to start position
                    player.transform.position = playerStartPos;
                    player.transform.rotation = playerStartRot;

                    // clear bugs from player inventory
                    playerInventory.RemoveBugsFromInventory();

                    // clear bugs from cauldron inventory
                    cauldron.cauldronInventoryUI.RemoveBugsFromInventory();

                    // show player
                    player.gameObject.SetActive(true);

                    // start day
                    StartNewDay();
                }
                if(!fadeToBlackScreen.IsActive())
                {
                    state = GameState.Catching;

                }
                break;
            case GameState.Brewing:
                break;
            default:
                break;
        }
    }

    //sets up all objects in their initial positions for a new day cycle
    public void StartNewDay()
    {
        //set up critters
        critterControl.StartNewDay();

        // update brewing controller
        brewingController.StartNewDay();

        //start timer
        timer.StartTimer();

        // show timer text
        timer.timerText.enabled = true;

        //show tutorial UI
        tutorialUI.EnableTutorial();
    }

    public void FadeEndDayText()
    {
        //fade in brew music
        FadeIn(musicBrewing);

        //fade out catch music
        FadeOut(musicCatching);

        // stop cauldron bubble
        cauldronBubble.Play();

        // stop player footsteps
        player.GetComponent<AudioSource>().enabled = false;


        // fade to black and show text, then fade out
        fadeToBlackScreen.AddState(FadeState.FadeIn, 1.0f);
        fadeToBlackScreen.SetText("You have grown tired and have returned to camp...");
        fadeToBlackScreen.AddShowText();
        fadeToBlackScreen.AddState(FadeState.Wait, 3.0f);
        fadeToBlackScreen.AddHideText();
        fadeToBlackScreen.AddState(FadeState.FadeOut, 1.0f);
        fadeToBlackScreen.StartActions();
       

        // disable player movement and cursor lock
        player.GetComponent<ThirdPersonMovement>().EnableCursorLock();
        player.GetComponent<ThirdPersonMovement>().DisableMovement();

        state = GameState.TransitionEndDay;

    }

    public void FadeEndDayNoText()
    {
        //fade in brew music
        FadeIn(musicBrewing);

        //fade out catch music
        FadeOut(musicCatching);

        // stop cauldron bubble
        cauldronBubble.Play();

        // stop player footsteps
        player.GetComponent<AudioSource>().enabled = false;

        // fade to black then fade out
        fadeToBlackScreen.AddState(FadeState.FadeIn, 1.0f);
        fadeToBlackScreen.AddState(FadeState.FadeOut, 1.0f);
        fadeToBlackScreen.StartActions();


        // disable player movement and cursor lock
        player.GetComponent<ThirdPersonMovement>().EnableCursorLock();
        player.GetComponent<ThirdPersonMovement>().DisableMovement();

        state = GameState.TransitionEndDay;

    }

    public void FadeStartDay()
    {
        //fade out brew music
        FadeOut(musicBrewing);

        //fade in catch music
        FadeIn(musicCatching);

        // stop cauldron bubble
        cauldronBubble.Stop();

        // start player footsteps
        player.GetComponent<AudioSource>().enabled = true;

        // fade to black and show text, then fade out
        fadeToBlackScreen.AddState(FadeState.FadeIn, 1.0f);
        fadeToBlackScreen.SetText("All of your critters have escaped in the night!");
        fadeToBlackScreen.AddShowText();
        fadeToBlackScreen.AddState(FadeState.Wait, 2.0f);
        fadeToBlackScreen.AddHideText();
        fadeToBlackScreen.AddState(FadeState.FadeOut, 1.0f);
        fadeToBlackScreen.StartActions();

        state = GameState.TransitionStartDay;

    }

    public void StartFirstDay()
    {

        FadeIn(musicCatching);

        // turn on player inventory
        playerInventory.gameObject.SetActive(true);
        inventorySlots.SetActive(true);

        // fade from black
        fadeToBlackScreen.SetBlack();
        fadeToBlackScreen.AddState(FadeState.FadeOut, 1.0f);
        fadeToBlackScreen.StartActions();

        // Is HAX: add the outline component to the brewer character mesh
        // because for some reason it is messed up by start functions
        var outline = player.transform.Find("Brewer_Character").gameObject.GetComponent<Outline>();
        outline.enabled = true;

        // show timer text
        timer.timerText.enabled = true;

        // Start day
        StartNewDay();
        state = GameState.Catching;
    }

    // update the depth of field setting
    void UpdateDepthOfField(float focusDistance, float aperture)
    {
        // get Post processing volume
        PostProcessVolume volume = postProcessingVolume.GetComponent<PostProcessVolume>();
        // get depth of field setting
        DepthOfField depthOfField;
        volume.profile.TryGetSettings<DepthOfField>(out depthOfField);
        // set focus distance
        depthOfField.focusDistance.value = focusDistance;
        // set aperture
        depthOfField.aperture.value = aperture;
    }

    //swaps to the brewing UI
    public void EndOfDay()
    {
        //clear critters
        critterControl.EndOfDay();

        //clear potion effects
        consumptionController.EndOfDay();
        
        // update brewing controller
        brewingController.EndOfDay();

        // hide timer text
        timer.timerText.enabled = false;
    }

    public void ContinueDay()
    {
        brewingController.ContinueDay();
        //set depth of field to far range
        UpdateDepthOfField(11.03f, 1.1f);

        state = GameState.Catching;
    }

    //fades music in slowly after 2 second delay
    public void FadeIn(AudioSource music)
    {
        //start playing the music if first day
        if (firstDay)
        {
            music.Play();
        }
        else
        {
            //play music with 2 second delay
            music.PlayDelayed(2.0f);
        }

        //fades the music in over the next 7 seconds
        StartCoroutine(FadeInCR(music));
    }


    //5 second coroutine that fades the music in
    IEnumerator FadeInCR(AudioSource music)
    {
        //until volume is max
        while (inFade < 1.0f)
        {
            //if first day, turn volume up over 5 seconds
            if (firstDay)
            {
                //set the time variable
                inFade += 0.2f * Time.deltaTime;

                //set the music volume
                music.volume = Mathf.Lerp(0.0f, 1.0f, inFade);
            }
            //if not first day, wait 2 seconds then turn the volume up
            else
            {
                //set the time variable
                inFade += 0.14f * Time.deltaTime;

                //set the music volume
                music.volume = Mathf.Lerp(-0.28f, 1.0f, inFade);
            }


            //yield the coroutine until next frame
            yield return null;
        }
        //set first day to false
        firstDay = false;

        //reset timer and break
        inFade = 0.0f;
        yield break;
    }


    //fades music out slowly
    public void FadeOut(AudioSource music)
    {
        //fades the music out over the next 5 seconds
        StartCoroutine(FadeOutCR(music));
    }


    //5 second coroutine that fades the music out
    IEnumerator FadeOutCR(AudioSource music)
    {
        //until volume is 0
        while (outFade < 1.0f)
        {
            //set the time variable
            outFade += 0.2f * Time.deltaTime;

            //set the music volume
            music.volume = Mathf.Lerp(1.0f, 0.0f, outFade);

            //yield the coroutine until next frame
            yield return null;
        }

        //reset timer and break
        outFade = 0.0f;
        yield return null;
    }
}
