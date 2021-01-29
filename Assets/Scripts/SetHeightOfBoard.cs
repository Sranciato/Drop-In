using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHeightOfBoard : MonoBehaviour
{
    public CharacterController characterController;
    public DropInBegin dropInBeginScript;
    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!dropInBeginScript.followBall)
            transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y - characterController.height, cameraTransform.position.z);
    }
}
