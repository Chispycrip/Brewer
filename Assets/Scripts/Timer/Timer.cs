using UnityEngine;
using TMPro;

/// <summary>
/// Countdown timer of 3 minutes.
/// </summary>
public class Timer : MonoBehaviour
{
    // timer related stuff
    public float timerLength = 180;
    public bool timerIsRunning = false;
    private float timeRemaining;

    // the actual timer counting down
    public TextMeshProUGUI timerText;

    //starts the timer
    public void StartTimer()
    {
        //reset time remaining and start timer
        //timerLength = 181;
        timeRemaining = timerLength;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // while timer is above 0 seconds
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        DisplayTime(timeRemaining);
    }

    // using a maths thing to make it appear as minutes and seconds
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // format the text in digital time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
