using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBuilding : MonoBehaviour
{
    public ParticleSystem smoke;
    public List<CollisionCallback> collisionCallbacks = new List<CollisionCallback>();
    public List<Animator> animators = new List<Animator>();

    public int fallThreshold = 0;
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
                if (animator.GetBool("IsHit") == false)
                    fallenWalls++;

                animator.SetBool("IsHit", true); 
            }
        }

        if (fallenWalls > fallThreshold)
        {
            foreach(Animator animator in animators)
            {
                if (animator.GetBool("IsHit") == false)
                {
                    animator.SetBool("IsHit", true);
                }
            }

            if (smoke != null)
                smoke.Play();
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
