using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public List<CollisionCallback> collisionCallbackList;
    public CollisionCallback enterCallback;
    public CollisionCallback exitCallback;

    public void Start()
    {
        Init(); 
    }


    private void Init()
    {
        foreach(CollisionCallback callback in collisionCallbackList)
        {
            callback.AddCallback(OnOutOfBound, null, "Excavator");
        }

        enterCallback.AddCallback(OnExcavatorEnter, null, "Excavator");
        exitCallback.AddCallback(OnExcavatorExit, null, "Excavator");
    }

    private void OnOutOfBound(GameObject hitObject)
    {
        Debug.LogWarning("Excavator hits the boundary of the track!! ");
    }

    private void OnExcavatorEnter(GameObject hitObject)
    {
        Excavator.Instance.UpdateSpeedBounsTrigger(true);
    }

    private void OnExcavatorExit(GameObject hitObject)
    {
        Excavator.Instance.UpdateSpeedBounsTrigger(false);
    }
}
