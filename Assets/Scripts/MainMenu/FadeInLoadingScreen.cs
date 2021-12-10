using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simply Fading in a LoadingScreen on load.
/// </summary>
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
