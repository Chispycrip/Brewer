using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BrewingController : MonoBehaviour
{
    [Header("UI's")]
    public GameObject brewUI;
    public GameObject endDayUI;
    public GameObject journalUI;

    public GameObject postProcessingVolume;

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

        //set depth of field to far range
        UpdateDepthOfField(11.03f, 1.1f);

        // show player
        player.gameObject.SetActive(true);
    }

    //swaps to the brewing UI
    public void EndOfDay()
    {
        // deactivate endDayUI
        endDayUI.SetActive(false);

        // activate caudron UI
        brewUI.SetActive(true);

        //set depth of field to close range
        UpdateDepthOfField(1.75f, 2.4f);

        // hide player
        player.gameObject.SetActive(false);
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

        //set depth of field to far range
        UpdateDepthOfField(11.03f, 1.1f);

        // show player
        player.gameObject.SetActive(true);
    }

    // update the depth of field setting
    void UpdateDepthOfField(float focusDistance,float aperture)
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
