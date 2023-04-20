using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private float exitHoldTime = 3f;
    private float currentExitHoldTime = 0f;
    private bool exitKeyPressed = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        if (Input.GetKey(KeyCode.Return))
        {
            //Debug.Log("start");
            currentExitHoldTime += Time.deltaTime;
            if (currentExitHoldTime >= exitHoldTime)
            {
                ExitGame();
            }
        }
        else
        {
            currentExitHoldTime = 0f;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MissionLevel_Joe&Allen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
