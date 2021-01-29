using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.SceneManagement;


public class SkateboardController : MonoBehaviour
{
    public DropInBegin dropInBeginScript;
    public Drive driveWheel1, driveWheel2, driveWheel3, driveWheel4;
    public GameObject vrRig, skateboard, vrCamera, Canvas;
    Rigidbody rb;
    public float maxVelocity = -50f;
    public LayerMask layerMask;
    bool droppedIn, isGrounded, setRigRotation, cantRotate, rightHandSecondaryButton, haveClickedButton, rightHandPrimaryButton;
    float distanceToGround, distanceToGroundSkateboard, offset, rightHandTrigger;
    Vector2 rightHandJoystick, leftHandJoystick;
    float xRotation = 0f;
    Vector2 xMov, zMov, velocity;
    List<InputDevice> leftHandDevices = new List<InputDevice>();
    List<InputDevice> rightHandDevices = new List<InputDevice>();

    // Start is called before the first frame update
    void Start()
    {
        distanceToGround = GetComponent<BoxCollider>().bounds.extents.y;
        distanceToGroundSkateboard = skateboard.GetComponent<BoxCollider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
        GetInputDevice();
    }

    // Update is called once per frame
    void Update()
    {
        readInputs();
        if (rb.velocity.y < maxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxVelocity, rb.velocity.z);
        }
        droppedIn = dropInBeginScript.droppingIn;
        if (droppedIn)
        {
            if (!setRigRotation)
            {
                offset = vrRig.transform.eulerAngles.x - transform.eulerAngles.x;
                setRigRotation = true;
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround + 0.3f, layerMask))
                isGrounded = true;
            else
                isGrounded = false;

            if (!isGrounded && !cantRotate)
            {
                if (!Physics.Raycast(skateboard.transform.position, skateboard.transform.TransformDirection(Vector3.down), distanceToGroundSkateboard + .1f, layerMask))
                {
                    skateboard.transform.RotateAround(transform.position, -Vector3.left, 50 * Time.deltaTime);
                    vrRig.transform.RotateAround(transform.position, -Vector3.left, 50 * Time.deltaTime);
                }
            }
            else if (isGrounded && !cantRotate)
            {
                if (vrRig.transform.rotation.x > 0)
                {
                    skateboard.transform.RotateAround(transform.position, Vector3.left, 200 * Time.deltaTime);
                    vrRig.transform.RotateAround(transform.position, Vector3.left, 200 * Time.deltaTime);
                }
            }


        }
    }
    void FixedUpdate()
    {
        if (droppedIn)
        {
            driveWheel1.Go(xMov.x, rightHandTrigger);
            driveWheel2.Go(xMov.x, rightHandTrigger);
            driveWheel3.Go(xMov.x, rightHandTrigger);
            driveWheel4.Go(xMov.x, rightHandTrigger);
        }
    }
    void readInputs()
    {
        if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.primary2DAxis, out rightHandJoystick))
        {
            if (droppedIn)
            {
                if (rb.velocity.magnitude < 2f)
                {
                    // Vector2 xMov = new Vector2(rightHandJoystick.x * transform.right.x, rightHandJoystick.x * transform.right.z);
                    // Vector2 zMov = new Vector2(rightHandJoystick.y * transform.forward.x, rightHandJoystick.y * transform.forward.z);
                    // Vector2 velocity = (xMov + zMov).normalized;
                    // rb.AddForce(new Vector3(velocity.x, 0, velocity.y) * 2f, ForceMode.Force);

                    // xMov = new Vector2(rightHandJoystick.x * transform.right.x, rightHandJoystick.x * transform.right.z);
                    // xMov *= rightHandJoystick.x;

                    // xRotation -= 

                    // transform.localRotation = Quaternion.Euler
                }
            }
                
        }
        if (leftHandDevices[0].TryGetFeatureValue(CommonUsages.primary2DAxis, out leftHandJoystick))
        {
            if (droppedIn)
            {
                if (rb.velocity.magnitude < 15f)
                {
                    xMov = new Vector2(leftHandJoystick.x, leftHandJoystick.x);
                    // xMov = new Vector2(leftHandJoystick.x * transform.right.x, leftHandJoystick.x * transform.right.z);
                    // Debug.Log(transform.right + " TRANSFORMRIGHT " + leftHandJoystick + "  LEFTHAND" + xMov + "  XMOV");
                    // Vector2 zMov = new Vector2(leftHandJoystick.y * transform.forward.x, leftHandJoystick.y * transform.forward.z);
                    // Vector2 velocity = (xMov + zMov).normalized;
                    // rb.AddForce(new Vector3(velocity.x, 0, -velocity.y) * 2f, ForceMode.Force);
                    zMov = new Vector2(leftHandJoystick.y * transform.forward.x, leftHandJoystick.y * transform.forward.z);
                    Vector3 direction = new Vector3(xMov.x, 0f, zMov.y);
                    velocity.y = zMov.y;
                    // if (direction != Vector3.zero)
                    //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
                    // Debug.Log(xMov + " XMOV " + zMov + " ZMOV " + velocity + " VELOC ");
                }
            }
                
        }

        if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.trigger, out rightHandTrigger))
        {
            if (droppedIn)
            {
                if (rb.velocity.magnitude < 2f)
                {
                    // Vector2 xMov = new Vector2(rightHandJoystick.x * transform.right.x, rightHandJoystick.x * transform.right.z);
                    // Vector2 zMov = new Vector2(rightHandJoystick.y * transform.forward.x, rightHandJoystick.y * transform.forward.z);
                    // Vector2 velocity = (xMov + zMov).normalized;
                    // rb.AddForce(new Vector3(velocity.x, 0, velocity.y) * 2f, ForceMode.Force);
                    
                    // xMov = new Vector2(rightHandJoystick.x * transform.right.x, rightHandJoystick.x * transform.right.z);
                    // xMov *= rightHandJoystick.x;

                    // xRotation -= 

                    // transform.localRotation = Quaternion.Euler
                }
            }
        }
        if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.secondaryButton, out rightHandSecondaryButton))
        {
            if (rightHandSecondaryButton && !haveClickedButton)
            {
                MenuScreen();
            }
            haveClickedButton = rightHandSecondaryButton;
        }
        if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.primaryButton, out rightHandPrimaryButton))
        {
            if (rightHandPrimaryButton && Canvas.activeSelf)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    void MenuScreen()
    {
        if (Canvas.activeSelf)
        {
            Canvas.SetActive(false);
        }
        else if(!Canvas.activeSelf)
        {
            Canvas.SetActive(true);
        }
    }

    void GetInputDevice()
    {
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);
    }
}
