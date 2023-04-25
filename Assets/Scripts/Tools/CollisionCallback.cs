using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCallback : MonoBehaviour
{
    private BoxCollider boxCollider;
    private CapsuleCollider capsuleCollider;
    private SphereCollider sphereCollider;
    private MeshCollider meshCollider;

    /* Will pass the gameobject of this script back to the callback function */
    private List<Action<GameObject>> callbackList = new List<Action<GameObject>>();
    private List<string> targetTags = new List<string>();
    private List<Type> targetTypes = new List<Type>();


    public delegate void OnCollision(GameObject hitObject);

    private List<OnCollision> callbacks = new List<OnCollision>();

    private void Awake()
    {
        CheckValidation();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogWarning("OnTriggerEnter!!");
        InvokeCallback(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.LogWarning("OnCollisionEnter");
        InvokeCallback(collision.gameObject);
    }

    /// <summary>
    /// User must have 'BoxCollider' / 'CapsuleCollider' / 'SphereCollider' in the same
    /// gameobject to use this script.
    /// </summary>
    private void CheckValidation()
    {
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        meshCollider = GetComponent<MeshCollider>();

        if (boxCollider == null && capsuleCollider == null && 
            sphereCollider == null && meshCollider == null)
        {
            Debug.LogError("CollisionCallback: collider component is required to use this script!");
        }
    }

    /// <summary>
    /// When a collision enter, check if the incoming object having one of the target types.
    /// Then check if the object having one of the target tags. If so, invoke all registered
    /// callback.
    /// </summary>
    private void InvokeCallback(GameObject other)
    {
        bool isTarget = false;

        foreach(Type type in targetTypes)
        {
            if (other.TryGetComponent(type, out _) == true)
                isTarget = true;
        }

        foreach(string tag in targetTags)
        {
            if (other.CompareTag(tag) == true)
                isTarget = true;
        }

        if (isTarget == true)
        {
            foreach(Action<GameObject> callback in callbackList)
            {
                callback.Invoke(gameObject);
            }
        }
    }

    /// <summary>
    /// Must provide target type or target tag to successfully register the callback function
    /// </summary>
    public void AddCallback(Action<GameObject> callback, Type targetType = null, string targetTag = null)
    {
        if (targetType != null)
            targetTypes.Add(targetType);

        if (targetTag != null)
            targetTags.Add(targetTag);

        if (targetType != null || targetTag != null)
            callbackList.Add(callback);
    }

}
