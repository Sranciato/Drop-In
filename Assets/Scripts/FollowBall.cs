using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public DropInBegin dropInBeginScript;
    public GameObject sphereRef, ballPhysics, skateboard;
    bool hasGottenOffset;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dropInBeginScript.followBall)
        {
            if (!hasGottenOffset)
            {
                offset = (transform.position - sphereRef.transform.position);
                sphereRef.transform.SetParent(transform, true);
                hasGottenOffset = false;
            }
            offset = (transform.position - sphereRef.transform.position);
            transform.position = new Vector3(ballPhysics.transform.position.x, ballPhysics.transform.position.y -.1f, ballPhysics.transform.position.z) + offset;
        }
    }
}
