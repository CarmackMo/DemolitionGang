using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RailwayChallengeController : MonoBehaviour
{
    public GameObject excavator;
    public GameObject startPoint;
    public GameObject endPoint;
    public List<Collider> leftBoundaries;
    public List<Collider> rightBoundaries;
    public TextMeshProUGUI uiText;

    private Vector3 respawnPosition;
    private Quaternion respawnRotation;
    private bool isChallenging = false;

    private void Start()
    {
        respawnPosition = excavator.transform.position;
        respawnRotation = excavator.transform.rotation;
        uiText.gameObject.SetActive(false);
    }

    private void Update()
    {
        float distanceToRail = Vector3.Distance(excavator.transform.position, startPoint.transform.position);

        if (!isChallenging && distanceToRail < 10f) // Adjust the distance according to your needs
        {
            uiText.text = "Press Button to Interact";
            uiText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                excavator.transform.position = startPoint.transform.position;
                excavator.transform.rotation = startPoint.transform.rotation;
                respawnPosition = startPoint.transform.position;
                isChallenging = true;
            }
        }
        else
        {
            uiText.gameObject.SetActive(false);
        }

        if (isChallenging)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                excavator.transform.position = respawnPosition;
                excavator.transform.rotation = respawnRotation;
            }

            bool outOfBounds = false;
            foreach (Collider boundary in leftBoundaries)
            {
                if (Physics.Raycast(excavator.transform.position, boundary.transform.position, 1.0f))
                {
                    outOfBounds = true;
                    break;
                }
            }
            foreach (Collider boundary in rightBoundaries)
            {
                if (Physics.Raycast(excavator.transform.position, boundary.transform.position, 1.0f))
                {
                    outOfBounds = true;
                    break;
                }
            }

            if (outOfBounds)
            {
                uiText.text = "Challenge Failed";
                uiText.gameObject.SetActive(true);
                isChallenging = false;
            }
            else
            {
                uiText.gameObject.SetActive(false);
            }

            float distanceToEnd = Vector3.Distance(excavator.transform.position, endPoint.transform.position);
            if (distanceToEnd < 1.0f)
            {
                uiText.text = "Challenge Complete";
                uiText.gameObject.SetActive(true);
                StartCoroutine(DisableUITextAfterSeconds(5));
                isChallenging = false;
            }
        }
    }

    public void OnChallengeFailed(GameObject hitObject)
    {
        if (isChallenging)
        {
            uiText.text = "Challenge Failed";
            uiText.gameObject.SetActive(true);
            isChallenging = false;
        }
    }

    public void SetActiveChallenge(bool active)
    {
        isChallenging = active;
    }


    IEnumerator DisableUITextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        uiText.gameObject.SetActive(false);
    }
}
