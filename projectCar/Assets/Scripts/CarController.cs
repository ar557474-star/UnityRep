using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider frontLeftWheelCollider;

    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform frontLeftWheelTransform;

    float verticalInput;
    float horizontalInput;
    [SerializeField] private float motorForce = 20f;
    [SerializeField] private float steeringAngle = 30f;
    [SerializeField] private float brakeForce = 100f;
    
    [SerializeField] Transform carCenterOfMass;
    private Rigidbody rigidbody;

    UiManager uiManager; 

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        //uiManager = GameObject.FindGameObjectWithTag("UiManager");
    }
    void Start()
    {
        rigidbody.centerOfMass = carCenterOfMass.localPosition;

        GameObject managerObj = GameObject.FindWithTag("UiManager");
        if (managerObj != null)
        {
            uiManager = managerObj.GetComponent<UiManager>();
            //Debug.Log("found");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        MotorForce();
        Steering();
        PowerSteering();
        ApplyBrakes();
        UpdateWheels();
    }
    void GetInput()
    {
        verticalInput =Input.GetAxis("Vertical");
        horizontalInput =Input.GetAxis("Horizontal");
    }
    void ApplyBrakes()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            frontLeftWheelCollider.brakeTorque = brakeForce;
            backLeftWheelCollider.brakeTorque = brakeForce;
            backRightWheelCollider.brakeTorque = brakeForce;
            frontRightWheelCollider.brakeTorque = brakeForce;
            // rigidbody.drag = 1f;
            rigidbody.linearDamping = 1f;
        } else
        {
            frontLeftWheelCollider.brakeTorque = 0f;
            backLeftWheelCollider.brakeTorque = 0f;
            backRightWheelCollider.brakeTorque = 0f;
            frontRightWheelCollider.brakeTorque = 0f;
            rigidbody.linearDamping = 0f;
           // rigidbody.drag = 0f;
        }
    }
    void MotorForce()
    {
        frontRightWheelCollider.motorTorque = motorForce * verticalInput;
        frontLeftWheelCollider.motorTorque = motorForce *verticalInput;
    }
    void Steering()
    {
        frontRightWheelCollider.steerAngle = steeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steeringAngle * horizontalInput;
    }
    void PowerSteering()
    {
        if (horizontalInput == 0)
        {
           // transform.rotation = Quaternion.Euler(0, 0, 0);
           transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,0),Time.deltaTime);
        }
    }
    void UpdateWheels()
    {
        RotateWheel(frontRightWheelCollider, frontRightWheelTransform);
        RotateWheel(backRightWheelCollider, backRightWheelTransform);
        RotateWheel(backLeftWheelCollider, backLeftWheelTransform);
        RotateWheel(frontLeftWheelCollider, frontLeftWheelTransform);
    }
    void RotateWheel(WheelCollider wheelCollider,Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
    public float CarSpeed()
    {
        float speed = rigidbody.linearVelocity.magnitude * 2.23693629f;
        return speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "TrafficVehicle")
        {
            AudioManager.instance.StopLevel01Music();
            //StartCoroutine(pause());
            uiManager.GameOver();
        }
    }
    IEnumerator pause()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0f;
    }
}
