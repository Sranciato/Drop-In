using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DropInBegin : MonoBehaviour
{
    public GameObject vrRig, rail, platform, ballForPhysics;
    public Transform ballSpawnArea;
    bool onRail, grabbing;
    [HideInInspector]
    public bool followBall, droppingIn;
    public GetContactPoints getContactPoints;
    Vector3 contactPoints;
    float sizeOfPlatform, closeSideToRail, farSideToRail, centerOfPlatform, longSideOfPlatform, correctContactPoints, prevPercentOfBoard;
    Rigidbody rb;
    Collider platformCollider;
    GameObject col;
    string quarterPipeName;
    // rotate the rig around the ballPhysics based on a raycast from ballPhysics! If ballphysics hits something on the side then rotate vrrig 90 degrees
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        platformCollider = platform.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onRail)
        {
            platform.transform.position = transform.position;
            platform.transform.rotation = transform.rotation;
            contactPoints = getContactPoints.GetContactsOfPlayerAndBoard();
            if (contactPoints != Vector3.zero)
                DoLean();
            else
                vrRig.GetComponent<DeviceBasedContinuousMoveProvider>().enabled = true;

        }
    }

    void DoLean()
    {
        // Disable movement on vr rig when on skateboard
        vrRig.GetComponent<DeviceBasedContinuousMoveProvider>().enabled = false;

        // Get real world coordinates of end points on skateboard depending on which way ramp is facing
        SetCloseAndFarPoints();

        // Determine where you are standing on the board
        float percentOfBoard = ((closeSideToRail - correctContactPoints) * 100 / longSideOfPlatform) / 100;
        float amountToRotate = percentOfBoard * 40;
        float amountToPosition = percentOfBoard * .2f;

        // Lean skateboard foward or backward
        RotateBoard(amountToRotate);

        // Drop in on ramp if you lean far enough foward
        if (percentOfBoard > .5f || percentOfBoard < -.5f)
        {
            DropInOnRamp();
        }
    }

    void RotateBoard(float amountToRotate)
    {
        if (quarterPipeName == "0")
        {
            transform.eulerAngles = new Vector3(18 - amountToRotate, 0, 0);
        }
        else if (quarterPipeName == "180")
        {
            ballForPhysics.transform.eulerAngles = new Vector3(0, 180, 0);
            transform.eulerAngles = new Vector3(-18 - amountToRotate, 0, 0);
        }
    }

    void DropInOnRamp()
    {
        onRail = false;
        grabbing = true;
        droppingIn = true;
        GetComponent<BoxCollider>().enabled = false;
        vrRig.GetComponent<CharacterController>().enabled = false;
        ballForPhysics.transform.position = ballSpawnArea.position;
        transform.SetParent(ballForPhysics.transform, true);
        transform.position = ballForPhysics.transform.position;
        ballForPhysics.GetComponent<Rigidbody>().useGravity = true;
        ballForPhysics.GetComponent<Rigidbody>().isKinematic = false;
        followBall = true;
        Destroy(platform);
    }

    void SetCloseAndFarPoints()
    {
        longSideOfPlatform = (platformCollider.bounds.size.x > platformCollider.bounds.size.z) ? platformCollider.bounds.size.x : platformCollider.bounds.size.z;
        if (longSideOfPlatform == platformCollider.bounds.size.x)
        {
            centerOfPlatform = platformCollider.bounds.center.x;
            correctContactPoints = contactPoints.x;
        }
        else if (longSideOfPlatform == platformCollider.bounds.size.z)
        {
            centerOfPlatform = platformCollider.bounds.center.z;
            correctContactPoints = contactPoints.z;
        }
        // MIGHT NEED TO CHANGE IF ON Z AXIS
        if (quarterPipeName == "0" || quarterPipeName == "Quarter Pipe 90")
        {
            closeSideToRail = centerOfPlatform + (longSideOfPlatform / 2f);
            farSideToRail = centerOfPlatform - (longSideOfPlatform / 2f);
        }
        else if (quarterPipeName == "180" || quarterPipeName == "Quarter Pipe 360")
            closeSideToRail = centerOfPlatform - (longSideOfPlatform / 2f);
            farSideToRail = centerOfPlatform + (longSideOfPlatform / 2f);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!grabbing && !onRail && collision.gameObject.name == "Rail")
        {
            col = collision.gameObject;
            rb.useGravity = false;
            rb.isKinematic = true;
            CheckDirectionOfBoard();
            onRail = true;
        }
    }

    void CheckDirectionOfBoard()
    {
        RaycastHit hit;
        Physics.Raycast(vrRig.transform.position, Vector3.down,out hit, 100f);
        quarterPipeName = hit.collider.transform.parent.tag;
        if (quarterPipeName == "0")
        {
            transform.position = new Vector3(transform.position.x, col.transform.position.y + .1f, col.transform.position.z - .3f);
            transform.rotation = Quaternion.Euler(18, 0, 0);
        }
        else if (quarterPipeName == "180")
        {
            transform.position = new Vector3(transform.position.x, col.transform.position.y + .1f, col.transform.position.z + .3f);
            transform.rotation = Quaternion.Euler(-18, 0, 0);
        }

        
    }
    public void GrabbingSkateboard()
    {
        grabbing = true;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void ReleasedSkateboard()
    {
        GetComponent<BoxCollider>().enabled = true;
        grabbing = false;
    }
}
