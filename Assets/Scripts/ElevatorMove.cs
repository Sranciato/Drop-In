using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    public Transform target, vrRig;
    Vector3 startPos;
    public float speed = 1f, t = 1f;
    public bool lift;

    void Start()
    {
        startPos = transform.position;
    }
    void FixedUpdate()
    {
        if (lift)
        {
            Lift();
        }
    }

    void Lift()
    {
        Debug.Log("LIFTOFF");
        if (transform.position.y >= target.transform.position.y)
            return;
        transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, new Vector3(transform.position.x, target.position.y, transform.position.z), t), speed);
        vrRig.position = Vector3.MoveTowards(vrRig.position, Vector3.Lerp(vrRig.position, new Vector3(vrRig.position.x, target.position.y, vrRig.position.z), t), speed);
    }
}
