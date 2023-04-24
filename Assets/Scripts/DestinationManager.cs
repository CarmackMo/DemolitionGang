using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class DestinationManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> destinationPoints;

    [SerializeField]
    private GameObject destinationPrefab;

    [SerializeField]
    private TextMeshProUGUI destinationText;

    private int currentDestinationIndex;
    private GameObject currentDestinationObject;

    private void Start()
    {
        currentDestinationIndex = 0;
        SpawnDestination();
    }

    public void DestinationReached()
    {
        Destroy(currentDestinationObject);

        currentDestinationIndex++;

        if (currentDestinationIndex < destinationPoints.Count)
        {
            SpawnDestination();
        }
        else
        {
            LevelCleared();
        }
    }

    private void SpawnDestination()
    {
        currentDestinationObject = Instantiate(destinationPrefab, destinationPoints[currentDestinationIndex].position, Quaternion.identity);
        destinationText.text = $"Next Destination: {destinationPoints[currentDestinationIndex].position}";
    }

    private void LevelCleared()
    {
        destinationText.text = "Level Cleared!";
        // Pause the game and wait for the player to hold the Enter key for 3 seconds
        Time.timeScale = 0f;
        StartCoroutine(CheckForNextLevelInput());
    }

    private IEnumerator CheckForNextLevelInput()
    {
        float heldTime = 0f;
        while (heldTime < 3f)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                heldTime += Time.unscaledDeltaTime;
            }
            else
            {
                heldTime = 0f;
            }

            yield return null;
        }

        // Load the next level here
        Time.timeScale = 1f;
        SceneManager.LoadScene("MissionLevel_Hang");
    }
}
