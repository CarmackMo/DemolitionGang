using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public List<CollisionCallback> collisionCallbackList;


    public void Start()
    {
        Init(); 
    }


    private void Init()
    {
        foreach(CollisionCallback collisionCallback in collisionCallbackList)
        {
            collisionCallback.AddCallback(OnExcavatorCollide, null, "Excavator");
        }
    }

    private void OnExcavatorCollide()
    {
        Debug.LogWarning("Excavator hits the boundary of the track!! ");
    }
}
