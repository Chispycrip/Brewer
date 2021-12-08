using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Tutorial : MonoBehaviour
{
    public float countdownTimer = 11f; //how long the UI is visible

    // changing properties of the canvas (and possibly text)
    [Header("Add Tutorial Canvas here")]
    public Canvas canvas;

    private bool hasBeenEnabled; //tracks if the tutorial has been enabled yet

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        hasBeenEnabled = false;
    }


    //turns on tutorial UI
    public void EnableTutorial()
    {
        //if tutorial has not been enabled yet, enable it
        if (!hasBeenEnabled)
        {
            hasBeenEnabled = true;
            StartCoroutine(TutorialKeys());
        }
    }

    IEnumerator TutorialKeys()
    {
        canvas.enabled = true;

        yield return new WaitForSeconds(countdownTimer);

        canvas.enabled = false;
    }
}
