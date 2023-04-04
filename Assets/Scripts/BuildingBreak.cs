using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBreak : MonoBehaviour
{
    public float DelayDeleteCollider = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DeleteCollider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("碰到的物体：" + collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Excavator"))
        {
            print("碰到的物体：播放动画");
            Invoke("DeleteCollider", DelayDeleteCollider);
            GetComponent<Animator>().enabled = true;
        }
    }
}
