using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetContactPoints : MonoBehaviour
{
    Vector3 contactPoints = Vector3.zero;
    public LayerMask layerMask, layerMask2;
    public ElevatorMove elevatorMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f, layerMask))
        {
            contactPoints = hit.point;
        }
        else
            contactPoints = Vector3.zero;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f, layerMask2))
        {
                elevatorMove.lift = true;
        }
        else
            elevatorMove.lift = false;
    }

    public Vector3 GetContactsOfPlayerAndBoard()
    {
        return contactPoints;
    }
}
