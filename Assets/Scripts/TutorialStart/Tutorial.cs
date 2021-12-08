using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Tutorial : MonoBehaviour
{
    // changing properties of the canvas (and possibly text)
    [Header("Add Tutorial Canvas here")]
    public Canvas canvas;

    // making the canvas wait to be activated because of the startup text
    private float countdownTimer = 11f;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    void Update()
    {
        if (countdownTimer <= 0)
        {
            countdownTimer = 99999999f;
            StartCoroutine(TutorialKeys());
        }
        countdownTimer -= Time.deltaTime;
    }

    IEnumerator TutorialKeys()
    {
        canvas.enabled = true;

        yield return new WaitForSeconds(11f);

        canvas.enabled = false;
    }
}
