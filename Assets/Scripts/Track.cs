using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public List<CollisionCallback> collisionCallbackList;

    public List<RailwayChallengeController> railwayChallengeControllers;


    public void Start()
    {
        Init(); 
    }


    private void Init()
    {
        foreach (RailwayChallengeController controller in railwayChallengeControllers)
        {
            foreach (CollisionCallback callback in collisionCallbackList)
            {
                callback.AddCallback(controller.OnChallengeFailed, null, "Excavator");
            }
        }
    }


    private void OnChallengeFailed(GameObject hitObject)
    {
        Debug.LogWarning("Excavator hits the boundary of the track!! ");
    }


}
