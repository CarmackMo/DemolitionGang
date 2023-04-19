using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBuilding : MonoBehaviour
{
    public List<CollisionCallback> collisionCallbacks = new List<CollisionCallback>();
    public List<Animator> animators = new List<Animator>();

    private int fallenWalls = 0;


    public void Start()
    {
        Init();
    }

    private void OnExcavatorEnter(GameObject hitObject)
    {
        foreach(Animator animator in animators)
        {
            if (animator.gameObject == hitObject)
            {
                animator.SetBool("IsHit", true);
                fallenWalls++;
            }
        }

        if (fallenWalls > 5)
        {
            foreach(Animator animator in animators)
            {
                if (animator.GetBool("IsHit") == false)
                {
                    animator.SetBool("IsHit", true);
                }
            }
        }
    }

    private void Init()
    {
       foreach(CollisionCallback callback in collisionCallbacks)
        {
            callback.AddCallback(OnExcavatorEnter, null, "Excavator");
        }
    }
}
