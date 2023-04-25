using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBuilding : MonoBehaviour
{
    public float despawnInterval = 10f;
    public GameObject victimPrefab;
    public ParticleSystem smoke;
    public List<CollisionCallback> collisionCallbacks = new List<CollisionCallback>();
    public List<Animator> animators = new List<Animator>();
    public List<Transform> victimPosList = new List<Transform>();

    public int fallThreshold = 0;
    private int fallenWalls = 0;
    private bool isDestroyed = false;
    private GameplayManager gameplayManager;

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
                {
                    gameplayManager.UpdateGameScore(gameplayManager.wallDestroyAward);
                    fallenWalls++;
                }

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

            if (isDestroyed == false)
            {
                gameplayManager.UpdateGameScore(gameplayManager.buildingDestroyAward);
                StartCoroutine(DespawnCoroutine());
                isDestroyed = true;
            }
        }
    }

    private void Init()
    {
        gameplayManager = GameplayManager.Instance;

        AlertManager.Instance.alertAction += SpanwVictims;

        foreach(CollisionCallback callback in collisionCallbacks)
        {
            callback.AddCallback(OnExcavatorEnter, null, "Excavator");
        }
    }

    private void SpanwVictims()
    {
        for (int i = 0; i < victimPosList.Count; i++)
        {
            GameObject victim = ObjectPoolManager.Instance.Spawn(victimPrefab, victimPosList[i].position, Quaternion.identity);
        }
    }

    private IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSecondsRealtime(despawnInterval);
        ObjectPoolManager.Instance.Despawn(gameObject);
    }
}
