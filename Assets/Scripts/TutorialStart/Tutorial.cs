using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI tutText;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = true;
        StartCoroutine(TutorialKeys());
    }

    IEnumerator TutorialKeys()
    {
        yield return new WaitForSeconds(12f);

        canvas.enabled = false;
    }
}
