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

    [Header("Controllers")]
    public EndGameController endGameController;

    [Header("Audio")]
    public AudioSource musicBrewing;
    public AudioSource musicCatching;
    public AudioSource cauldronBubble;

    //time variables for music fades
    private float inFade; 
    private float outFade;

    //variable to make music start without delay on the first day
    bool firstDay = true;

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


    public void StartNewDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // deactivate caudron UI
        brewUI.SetActive(false);

        // disable player movement and cursor lock
        player.DisableCursorLock();
        player.EnableMovement();

        //fade out brew music
        FadeOut(musicBrewing);
        //musicBrewing.Stop();

        //fade in catch music
        FadeIn(musicCatching);
        //musicCatching.Play();

        // stop cauldron bubble
        cauldronBubble.Stop();
    }

    //swaps to the brewing UI
    public void EndOfDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // activate caudron UI
        brewUI.SetActive(true);

        // start cauldron bubble
        cauldronBubble.Play();

        //fade in brew music
        FadeIn(musicBrewing);

        //fade out catch music
        FadeOut(musicCatching);

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

        // show player
        player.gameObject.SetActive(true);
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
            // if hasn't got golden critter start brew menu
            if (!endGameController.PlayerHasGoldenCritter())
            {
                EnableEndDayUI();
            }
            
        }
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
