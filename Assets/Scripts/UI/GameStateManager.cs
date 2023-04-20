using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    public CountdownTrigger CountdownTriggerScript;
    public EscapeAreaTrigger EscapeAreaScript;
    public GameObject endScreen;
    public float endScreenDisplayTime = 5f;

    private bool level1Completed = false;
    private bool isRestarting = false;



    private void Update()
    {
        if (EscapeAreaScript != null && EscapeAreaScript.escaped)
        {
            if (SceneManager.GetActiveScene().name == "MissionLevel_Joe&Allen")
            {
                level1Completed = true;
                SceneManager.LoadScene("MissionLevel_Hang");
            }
            else if (CountdownTriggerScript != null && CountdownTriggerScript.TimerEnded && !EscapeAreaScript.escaped)
            {
                if (!isRestarting)
                {
                    isRestarting = true;
                    StartCoroutine(RestartLevelAfterDelay(5));
                }
            }
            else if (SceneManager.GetActiveScene().name == "MissionLevel_Hang")
            {
                StartCoroutine(ShowEndScreenAndReturnToMainMenu());
            }
        }
        
    }

    private void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ShowEndScreenAndReturnToMainMenu()
    {
        endScreen.SetActive(true);
        yield return new WaitForSeconds(endScreenDisplayTime);
        endScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartCurrentLevel();
    }

}
