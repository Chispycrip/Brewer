using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState
{
    None,
    FadeIn,
    FadeOut,
    Wait,
    ShowText,
    HideText
}

// fade action
public struct FadeAction
{
    // fade state
    public FadeState state;
    // time of execution
    public float time;
}

public class FadeController : MonoBehaviour
{

    private float timer = 0.0f;
    private FadeAction currentAction;
    private Image screen;
    private Text textBox;

    // queue of actions
    private List<FadeAction> queue;
    private bool isActive = false;

    private void Start()
    {
        queue = new List<FadeAction>();
        screen = transform.GetChild(0).GetComponent<Image>();
        textBox = transform.GetChild(1).GetComponent<Text>();
        textBox.enabled = false;
        currentAction.state = FadeState.None;
    }

    private void Update()
    {
        if (isActive)
        {
            // update timer and compare agains current actions time limit
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 0.0f;
                // turn off black screen in foud out complete
                if(currentAction.state == FadeState.FadeOut)
                {
                    screen.enabled = false;
                }
                currentAction.state = FadeState.None;
            }

            switch (currentAction.state)
            {
                case FadeState.FadeIn:
                    // increase alpha
                    screen.color = new Color(0,0,0, (currentAction.time - timer) / currentAction.time);
                    break;
                case FadeState.FadeOut:
                    // decrease alpha
                    screen.color = new Color(0, 0, 0, (timer) / currentAction.time);
                    break;
                case FadeState.Wait:
                    break;
                case FadeState.ShowText:
                    // turn on text box
                    textBox.enabled = true;
                    currentAction.state = FadeState.None;
                    break;
                case FadeState.HideText:
                    // turn off text box
                    textBox.enabled = false;
                    currentAction.state = FadeState.None;
                    break;
                case FadeState.None:
                    // check queue size
                    if (queue.Count > 0)
                    {
                        // replace current action with next in queue
                        FadeAction nextAction = queue[0];
                        if(nextAction.time>0)
                        {
                            timer = nextAction.time; // reset timer
                            currentAction.state = nextAction.state;
                            currentAction.time = nextAction.time;
                            switch(currentAction.state)
                            {
                                case FadeState.FadeIn:
                                    // make initially transparrent
                                    screen.color = new Color(0, 0, 0, 0);
                                    screen.enabled = true;
                                    break;
                                case FadeState.FadeOut:
                                    // make initially opaque
                                    screen.color = new Color(0, 0, 0, 1);
                                    screen.enabled = true;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // pop last action from start of queue
                        queue.RemoveAt(0);
                    }
                    else
                    {
                        isActive = false;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    // set text in text UI
    public void SetText(string newText)
    {
        textBox.text = newText;
    }

    // add state to queue
    public void AddState(FadeState state, float time)
    {
        FadeAction newAction = new FadeAction();
        newAction.state = state;
        newAction.time = time;
        queue.Add(newAction);
    }

    // add fade in (specifically for unity events)
    public void AddFadeIn(float time)
    {
        AddState(FadeState.FadeIn, time);
    }
    // add fade out (specifically for unity events)
    public void AddFadeOut(float time)
    {
        AddState(FadeState.FadeOut, time);
    }
    // add wait (specifically for unity events)
    public void AddWait(float time)
    {
        AddState(FadeState.Wait, time);
    }
    // add show text(specifically for unity events)
    public void AddShowText()
    {
        AddState(FadeState.ShowText, 0.1f);
    }
    // add hide text (specifically for unity events)
    public void AddHideText()
    {
        AddState(FadeState.HideText, 0.1f);
    }
    // start executing the queue
    public void StartActions()
    {
        isActive = true;
    }
    // stop executing the queue
    public void StopActions()
    {
        isActive = true;
    }
    // is executing the queue
    public bool IsActive()
    {
        return isActive;
    }
    // screen is dark
    public bool IsDark()
    {
        return screen.color.a == 1;
    }
    // clear queue
    public void Clear()
    {
        queue.Clear();
    }

    public void SetBlack()
    {
        screen.enabled = true;
        screen.color = new Color(0,0,0,1);
    }
}
