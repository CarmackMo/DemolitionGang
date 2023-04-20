using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controlPanel;

    private bool isPaused = false;
    private float pauseHoldTime = 2f;
    private float currentPauseHoldTime = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            currentPauseHoldTime += Time.deltaTime;
            if (currentPauseHoldTime >= pauseHoldTime)
            {
                TogglePause();
                currentPauseHoldTime = 0f;
            }
        }
        else
        {
            currentPauseHoldTime = 0f;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        controlPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
