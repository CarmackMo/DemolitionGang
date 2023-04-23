using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public Image tutorialImage;
    public Sprite[] tutorialImages;
    public float enterHoldTime = 1f;

    private int currentImageIndex = 0;
    private bool isTutorialActive = false;
    private float enterHoldTimer = 0f;

    private void OnTriggerEnter (Collider collision)
    {
        if (collision.CompareTag("Excavator"))
        {
            isTutorialActive = true;
            tutorialCanvas.SetActive(true);
            tutorialImage.sprite = tutorialImages[currentImageIndex];
            Time.timeScale = 0f;
        }
    }

    private void Update()
    {
        if (isTutorialActive)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                enterHoldTimer += Time.unscaledDeltaTime;
                if (enterHoldTimer >= enterHoldTime)
                {
                    enterHoldTimer = 0f;
                    currentImageIndex++;
                    if (currentImageIndex >= tutorialImages.Length)
                    {
                        isTutorialActive = false;
                        tutorialCanvas.SetActive(false);
                        Time.timeScale = 1f;
                        Destroy(gameObject);
                    }
                    else
                    {
                        tutorialImage.sprite = tutorialImages[currentImageIndex];
                    }
                }
            }
            else
            {
                enterHoldTimer = 0f;
            }
        }
    }
}
