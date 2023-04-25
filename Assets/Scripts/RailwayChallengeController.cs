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

    public List<CollisionCallback> collisionCallbacks;


    private Vector3 respawnPosition;
    private Quaternion respawnRotation;
    private bool isChallenging = false;
    private bool outOfBounds = false;

    private void Start()
    {
        respawnPosition = excavator.transform.position;
        respawnRotation = excavator.transform.rotation;
        uiText.gameObject.SetActive(false);

        foreach(CollisionCallback callback in collisionCallbacks)
        {
            callback.AddCallback(OnExcavatorHitBoundary, null, "Excavator");
        }
    }

    private void Update()
    {
        float distanceToRail = Vector3.Distance(excavator.transform.position, startPoint.transform.position);

        if (!isChallenging && distanceToRail < 10f) // Adjust the distance according to your needs
        {
            uiText.text = "Press Button to Interact";
            uiText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                excavator.transform.position = startPoint.transform.position;
                excavator.transform.rotation = startPoint.transform.rotation;
                respawnPosition = startPoint.transform.position;
                respawnRotation = startPoint.transform.rotation;
                isChallenging = true;
                outOfBounds = false;
            }
        }
        else
        {
            uiText.gameObject.SetActive(false);
        }

        if (isChallenging)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                excavator.transform.position = respawnPosition;
                excavator.transform.rotation = respawnRotation;
                outOfBounds = false;
            }

            



            //foreach (Collider boundary in leftBoundaries)
            //{
            //    if (Physics.Raycast(excavator.transform.position, boundary.transform.position, 1.0f))
            //    {
            //        Debug.Log("0101");
            //        outOfBounds = true;
            //        break;
            //    }
            //}
            //foreach (Collider boundary in rightBoundaries)
            //{
            //    if (Physics.Raycast(excavator.transform.position, boundary.transform.position, 1.0f))
            //    {
            //        Debug.Log("0202");
            //        outOfBounds = true;
            //        break;
            //    }
            //}

            if (outOfBounds && isChallenging)
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
            if (isChallenging && distanceToEnd < 5.0f)
            {

                uiText.text = "Challenge Complete";
                uiText.gameObject.SetActive(true);
                StartCoroutine(DisableUITextAfterSeconds(5));
                isChallenging = false;
            }
            foreach (CollisionCallback callback in collisionCallbacks)
            {
                callback.AddCallback(OnExcavatorHitEnd, null, "Excavator");
            }
        }
    }

    public void OnChallengeFailed(GameObject hitObject)
    {
        if ( outOfBounds && isChallenging)
        {
            uiText.text = "Challenge Failed";
            uiText.gameObject.SetActive(true);
            isChallenging = false;
            StartCoroutine(DisableUITextAfterSeconds(5));
        }
    }

    public void OnExcavatorHitBoundary(GameObject bound)
    {
        outOfBounds = true;
        Debug.Log($"Excavator hit bounary {bound.name}" );
        //uiText.text = "Challenge Failed";
        //uiText.gameObject.SetActive(true);
        //isChallenging = false;
    }

    public void OnExcavatorHitEnd(GameObject End)
    {
        outOfBounds = true;
        Debug.Log($"Excavator hit End {End.name}");
        //uiText.text = "Challenge Complete";
        //uiText.gameObject.SetActive(true);
        //StartCoroutine(DisableUITextAfterSeconds(5));
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
