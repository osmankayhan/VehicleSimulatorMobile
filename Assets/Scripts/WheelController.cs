using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    public float acceleration = 500f;
    public float breakingForce = 350f;
    public float maxTurnAngle = 20f;

    private float currentAccceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    bool leftButton;
    bool rightButton;
    public bool frontButton;
    public bool backButton;
    public bool handBreakButton;

    private void FixedUpdate()
    {
        //currentAccceleration = acceleration * Input.GetAxisRaw("Vertical");

        //El Freni Sistemi
        //if (Input.GetKey(KeyCode.Space))
        //    currentBreakForce = breakingForce;
        //else
        //    currentBreakForce = 0f;

        // Tekerleklerin pozisyon ve rotasyon güncellemesini yap
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(backRight, backRightTransform);
        UpdateWheel(backLeft, backLeftTransform);

        // Right ve Left butonlarýna göre dönüþ iþlemlerini yap
        if (rightButton)
            SteerRight();
        else if (leftButton)
            SteerLeft();
        else
            ResetSteering();

        if (frontButton)
            SteerFront();
        else if (backButton)
            SteerBack();
        else
            GasOff();

        if (handBreakButton)
            HandBreakOn();
    }

    void UpdateWheel(WheelCollider _collider, Transform _transform)
    {
        Vector3 position;
        Quaternion rotation;
        _collider.GetWorldPose(out position, out rotation);
        _transform.position = position;
        _transform.rotation = rotation;
    }

    public void DefaultRotation()
    {
        transform.rotation = Quaternion.Euler(0f, transform.position.y, 0f);
    }

    public void FrontButtonDown()
    {
        frontButton = true;
    }

    public void FrontButtonUp()
    {
        frontButton = false;
    }

    public void BackButtonDown()
    {
        backButton = true;
    }

    public void BackButtonUp()
    {
        backButton = false;
    }

    public void RightButtonDown()
    {
        rightButton = true;
    }

    public void RightButtonUp()
    {
        rightButton = false;
        ResetSteering();
    }

    public void LeftButtonDown()
    {
        leftButton = true;
    }

    public void LeftButtonUp()
    {
        leftButton = false;
        ResetSteering();
    }

    public void HandBreakButtonDown()
    {
        handBreakButton = true;
    }

    public void HandBreakButtonUp()
    {
        handBreakButton = false;
        HandBreakOff();
    }

    public void ResetSteering()
    {
        currentTurnAngle = 0f;
        frontRight.steerAngle = currentTurnAngle;
        frontLeft.steerAngle = currentTurnAngle;
    }

    public void SteerLeft()
    {
        currentTurnAngle = -maxTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
        frontLeft.steerAngle = currentTurnAngle;
    }

    public void SteerRight()
    {
        currentTurnAngle = maxTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
        frontLeft.steerAngle = currentTurnAngle;
    }

    public void SteerFront()
    {
        currentAccceleration = acceleration;
        frontRight.motorTorque = currentAccceleration;
        frontLeft.motorTorque = currentAccceleration;
    }

    public void SteerBack()
    {
        currentAccceleration = -acceleration;
        frontRight.motorTorque = currentAccceleration;
        frontLeft.motorTorque = currentAccceleration;
    }

    public void HandBreakOn()
    {
        currentBreakForce = breakingForce;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;

        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
    }

    public void HandBreakOff()
    {
        currentBreakForce = 0f;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;

        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
    }

    public void GasOff()
    {
        currentAccceleration = 0f;
        frontRight.motorTorque = currentAccceleration;
        frontLeft.motorTorque = currentAccceleration;
    }
}