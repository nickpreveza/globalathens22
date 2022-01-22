using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    public bool canWalk = true;
    [Header("Running")]
    public bool canRun = true;
    public bool inVechile;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    [Header("Vehicle Controls")]
    public bool touchedGround;
    [SerializeField] GameObject vehicleTarget;
    [SerializeField] float turnSpeed = 50f;
    [SerializeField] float vehicleVel = 0.0f;      // Current Travelling Velocity
    [SerializeField] float vehicleMaxVel = 1.0f;   // Max Velocity
    [SerializeField] float vehicleAcceleration = 0.0f;           // Current Acceleration
    [SerializeField] float vehicleSpeed = 0.1f;      // Amount to increase Acceleration with.
    [SerializeField] float vehicleMaxAcc = 1.0f;        // Max Acceleration
    [SerializeField] float vehicleMinAcc = -0.5f;       // Min Acceleration
    [SerializeField] float vehicleRotateSpeed = 1f;
    [SerializeField] Transform boatPosition;
    [SerializeField] Transform boatDeport;
    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
     
        if (inVechile && vehicleTarget != null)
        {
            VehicleControls();
           
            return;
        }

        if (!canWalk)
        {
            return;
        }
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    void VehicleControls()
    {
        if (touchedGround)
        {
            vehicleTarget.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.transform.position = boatDeport.position;
            this.gameObject.transform.parent = null;
            inVechile = false;
            canWalk = true;
            //teleport player
            //remove vehicle controls
            return;
        }
        vehicleAcceleration += Input.GetAxis("Vertical") * vehicleSpeed * Time.deltaTime;
        vehicleTarget.transform.Rotate(0.0f, Input.GetAxis("Horizontal") * vehicleRotateSpeed, 0.0f);
        
        
        if (vehicleAcceleration > vehicleMaxAcc)
            vehicleAcceleration = vehicleMaxAcc;
        else if (vehicleAcceleration < vehicleMinAcc)
            vehicleAcceleration = vehicleMinAcc;

        vehicleVel += vehicleAcceleration;

        if (vehicleVel > vehicleMaxVel)
            vehicleVel = vehicleMaxVel;
        else if (vehicleVel < -vehicleMaxVel)
            vehicleVel = -vehicleMaxVel;

        vehicleTarget.transform.Translate(Vector3.forward * vehicleVel * Time.deltaTime);


    }
}