using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameplayManager;

public class PartPrompt : MonoBehaviour
{
    private GameplayManager gameManager;
    private Excavator excavator;
    public GameObject[] Parts;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameplayManager.Instance;
        excavator = Excavator.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        UpdateShowPrompt();
    }

    private void UpdateShowPrompt()
    {
        GameObject part = Parts[Parts.Length - 1];
        part.SetActive(excavator.isBucketRotating);
        for (int i = 0,len =Parts.Length -1;i<len;i++)
        {
            part= Parts[i];
            if(part != null)
            {
                part.SetActive(gameManager.sticks[i].stickState != StickState.IDLE);
            }
        }
}
}
