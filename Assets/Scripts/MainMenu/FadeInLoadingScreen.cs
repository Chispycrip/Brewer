using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInLoadingScreen : MonoBehaviour
{
    public float fadeInSpeed;
    Image image;
    float alpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        alpha += (1f / fadeInSpeed) * Time.deltaTime;
        image.color = new Color(0, 0, 0, alpha);
    }
}
