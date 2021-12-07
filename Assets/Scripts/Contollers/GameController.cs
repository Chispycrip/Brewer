using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public enum GameState
{
    Catching,
    Brewing,
    TransitionEndDay,
    TransitionStartDay
}

public class GameController : MonoBehaviour
{
    public Timer timer;
    public GameObject player;
    public CritterController critterControl;
    public BrewingController brewingController;
    public ConsumptionController consumptionController;
    public FadeController fadeToBlackScreen;
    public CinemachineVirtualCamera cinVCam;
    public GameObject cauldronVCam;
    public CinemachineFreeLook cinTPCam;
    public GameObject postProcessingVolume;

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;

    private GameState state = GameState.Catching;
    
    //Start is called before the first frame update
    void Start()
    {
        //set player start position
        playerStartPos = player.transform.position;
        playerStartRot = player.transform.rotation;

        //start will be used to display any opening menu/animation desired//

        //initialise the critter controller
        critterControl.Init(player);

        //initialise the consumption controller
        consumptionController.Init();

        //Begin the first day (fade in)
        fadeToBlackScreen.AddState(FadeState.FadeOut, 1.0f);
        fadeToBlackScreen.StartActions();
        StartNewDay();

    }

    // Update is called once per frame
    private void Update()
    {
        switch(state)
        {
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
                }
                if(!fadeToBlackScreen.IsActive())
                {
                    // end the day
                    EndOfDay();

                }
                break;
            case GameState.TransitionStartDay:
                if(fadeToBlackScreen.IsDark())
                {
                    // set camera to track player
                    cinTPCam.MoveToTopOfPrioritySubqueue();

                    //set depth of field to far range
                    UpdateDepthOfField(11.03f, 1.1f);

                    //set player to start position
                    player.transform.position = playerStartPos;
                    player.transform.rotation = playerStartRot;

                    // show player
                    player.gameObject.SetActive(true);
                }
                if(!fadeToBlackScreen.IsActive())
                {
                    // start day
                    StartNewDay();
                }
                break;
            case GameState.Brewing:
                break;
        }
    }

    //sets up all objects in their initial positions for a new day cycle
    public void StartNewDay()
    {
        //set up critters
        critterControl.StartNewDay();

        //start timer
        timer.StartTimer();

        // show timer text
        timer.timerText.enabled = true;

        // update brewing controller
        brewingController.StartNewDay();

        state = GameState.Catching;
    }

    public void FadeEndDayText()
    {
       
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

        //play any day ending animations/transistions//

        //clear potion effects
        consumptionController.EndOfDay();
        
        // update brewing controller
        brewingController.EndOfDay();

        // hide timer text
        timer.timerText.enabled = false;

        state = GameState.Brewing;
    }

    public void ContinueDay()
    {
        brewingController.ContinueDay();
        //set depth of field to far range
        UpdateDepthOfField(11.03f, 1.1f);
    }
}
