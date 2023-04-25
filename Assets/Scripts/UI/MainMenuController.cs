using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Animator animationController;

    private float exitHoldTime = 3f;
    private float currentExitHoldTime = 0f;
    private bool exitKeyPressed = false;
    private bool isAnimationPlaying = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isAnimationPlaying = true;    
            StartCoroutine(PlayAnimationAndLoadNextScene());
        }

        if (Input.GetKey(KeyCode.Return))
        {
            
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

    private IEnumerator PlayAnimationAndLoadNextScene()
    {
        animationController.SetTrigger("Titles");
        float animationLength = GetAnimationLength("TitleScreen");
        yield return new WaitForSeconds(animationLength);
        animationController.gameObject.SetActive(false);
        SceneManager.LoadScene("MissionLevel_in PROGRESS");
    }

    private float GetAnimationLength(string animationName)
    {
        RuntimeAnimatorController ac = animationController.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == animationName)
            {
                return ac.animationClips[i].length;
            }
        }
        return 0;
    }



    public void ExitGame()
    {
        Application.Quit();
    }
}
