using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{

    WheelCollider wheelCollider;
    bool moving;
    // Start is called before the first frame update
    void Awake()
    {
        wheelCollider = GetComponent<WheelCollider>();
        wheelCollider.motorTorque = 0.000001f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Go(float xMov, float triggerSpeed)
    {
        float thrustTorque;
        thrustTorque = -triggerSpeed * 400f;
        wheelCollider.motorTorque = thrustTorque;
        Steer(xMov);
    }

    void Steer(float xMov)
    {
        if (transform.tag != "RearWheels")
        {
            xMov = xMov / 3;
            float steerAngle = 30 * xMov;
            wheelCollider.steerAngle = steerAngle;
        }
        
    }
}
