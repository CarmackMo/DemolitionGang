using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    public CountdownTrigger CountdownTriggerScript;
    public EscapeAreaTrigger EscapeAreaScript;
    public Animator endScreenAnimator;

    public float endScreenDisplayTime = 5f;

    private bool level1Completed = false;
    private bool isRestarting = false;



    private void Update()
    {
        if (CountdownTriggerScript != null && CountdownTriggerScript.escaped)
        {
            if (SceneManager.GetActiveScene().name == "MissionLevel_Joe&Allen")
            {
                level1Completed = true;
                SceneManager.LoadScene("MissionLevel_Hang");
            }
            else if (CountdownTriggerScript != null && CountdownTriggerScript.TimerEnded && !CountdownTriggerScript.escaped)
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
        endScreenAnimator.gameObject.SetActive(true); 
        yield return new WaitForSeconds(endScreenDisplayTime); 

        endScreenAnimator.SetTrigger("typlaying"); 
        yield return new WaitForSeconds(endScreenAnimator.GetCurrentAnimatorStateInfo(0).length); 

        endScreenAnimator.gameObject.SetActive(false); 
        SceneManager.LoadScene("Mainmenu"); 
    }

    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartCurrentLevel();
    }

}
