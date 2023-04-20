using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTrigger : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject escapeArea;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI messageText;

    private bool timerEnded = false;

    public bool TimerEnded
    {
        get { return timerEnded; }
    }

    public bool IsCountdownStarted()
    {
        return countdownStarted;
    }

    public void SetEscaped(bool value)
    {
        escaped = value;
    }

    public bool escaped = false;

    private bool countdownStarted = false;
    private float countdownTime = 300f; 
    private float messageDisplayTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Excavator") && !countdownStarted)
        {
            Debug.Log("is triggered");
            countdownStarted = true;
            StartCoroutine(ShowMessage("They called the police! Get back to the start point!", messageDisplayTime));
            StartCoroutine(Countdown());
        }
    }

    private IEnumerator ShowMessage(string message, float displayTime)
    {
        messageText.text = message;
        messageText.CrossFadeAlpha(1f, 0.5f, false);
        yield return new WaitForSeconds(displayTime);
        messageText.CrossFadeAlpha(0f, 0.5f, false);
    }


    private IEnumerator Countdown()
    {
        while (countdownTime > 0)
        {
            countdownText.text = $"Time Remaining: {countdownTime / 60:00}:{countdownTime % 60:00}";
            yield return new WaitForSeconds(1f);
            countdownTime -= 1f;

            if (escaped) 
            {
                if (countdownTime > 0)
                {
                    StartCoroutine(ShowMessage("Mission Sucess！", messageDisplayTime));
                }
                else
                {
                    StartCoroutine(ShowMessage("Mission Failed！", messageDisplayTime));
                    timerEnded = true;
                }
                countdownStarted = false;
                countdownText.text = "";
                break;
            }
        }
    }
}
