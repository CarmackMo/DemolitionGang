using UnityEngine;
using System.Collections.Generic;
public class ChallengeTrigger : MonoBehaviour
{
    [SerializeField]
    private RailwayChallengeController targetChallenge;

    [SerializeField]
    private List<RailwayChallengeController> otherChallenges;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Excavator"))
        {
            ActivateTargetChallenge();
            DeactivateOtherChallenges();
        }
    }

    private void ActivateTargetChallenge()
    {
        targetChallenge.SetActiveChallenge(true);
    }

    private void DeactivateOtherChallenges()
    {
        foreach (RailwayChallengeController challenge in otherChallenges)
        {
            challenge.SetActiveChallenge(false);
        }
    }
}
