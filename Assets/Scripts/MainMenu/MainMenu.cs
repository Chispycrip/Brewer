using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameScene;
    public GameObject loadingScreen;
    public AudioSource menuMusic;

    //time variables for music fades
    private float outFade;

    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        loadingScreen.SetActive(true);
        Invoke("OpenGameScene", 1.5f);
        FadeOut(menuMusic);
        
    }

    void OpenGameScene()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void Quit()
    {
        Application.Quit();
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
            outFade += 0.5f * Time.deltaTime;

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
