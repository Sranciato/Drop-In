using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCorrectHand : MonoBehaviour
{
    public Transform pivot;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "LeftHand Controller")
        {
            pivot.localPosition = new Vector3(pivot.localPosition.x, pivot.localPosition.y, -0.4f);
        }
        else if (collider.name == "RightHand Controller")
        {
            pivot.localPosition = new Vector3(pivot.localPosition.x, pivot.localPosition.y, 0.43f);
        }
    }
}
