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
        foreach(CollisionCallback collisionCallback in collisionCallbackList)
        {
            collisionCallback.AddCallback(OnOutOfBound, null, "Excavator");
        }

        enterCallback.AddCallback(OnExcavatorEnter, null, "Excavator");
        exitCallback.AddCallback(OnExcavatorExit, null, "Excavator");
    }

    private void OnOutOfBound()
    {
        Debug.LogWarning("Excavator hits the boundary of the track!! ");
    }

    private void OnExcavatorEnter()
    {
        Excavator.Instance.UpdateSpeedBounsTrigger(true);
    }

    private void OnExcavatorExit()
    {
        Excavator.Instance.UpdateSpeedBounsTrigger(false);
    }
}
