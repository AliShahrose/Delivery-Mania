using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Adjustable factors from editor
    public float maxSpeedForward = 20f;
    public float maxSpeedReverse = 10f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float dragFactor = 5.0f;

    // Internal factors
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float rotate = 0f;
    float upVelocity = 0f;

    Rigidbody2D carRigidbody2D;

    public GameObject map;
    public GameObject minimap;
    public GameObject admin;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            minimap.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            minimap.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            map.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            map.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            admin.SetActive(true);
        }
        
        if (Input.GetKeyUp(KeyCode.H))
        {
            admin.SetActive(false);
        }

    }

    void FixedUpdate()
    {
        applyEngineForce();
        slowSideVelocity();
        applySteering();
    }

    void applyEngineForce()
    {
        // Apply force to move
        Vector2 engineForce = transform.up * accelerationInput * accelerationFactor;
        carRigidbody2D.AddForce(engineForce, ForceMode2D.Force);

        upVelocity = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        // Cap out maximum speed
        if (upVelocity > maxSpeedForward && accelerationInput > 0)
        {
            return;
        }
        else if (upVelocity < -maxSpeedReverse && accelerationInput < 0)
        {
            return;
        }

        // Stop eventually if not accelerating
        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, dragFactor, Time.fixedDeltaTime * 3);
        }
        else {
            carRigidbody2D.drag = 0;
        }

        if (upVelocity > 0 && accelerationInput < 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, dragFactor * 10, Time.fixedDeltaTime * 3);
        }
        else if (upVelocity < 0 && accelerationInput > 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, dragFactor * 10, Time.fixedDeltaTime * 3);
        }
    }
    void applySteering()
    {
        // Don't turn if stationary
        if (carRigidbody2D.velocity != Vector2.zero)
        {
            rotate = 1f;
        }
        else
        {
            rotate = 0f;
        }

        // Apply rotation
        rotationAngle -= steeringInput * turnFactor * rotate;
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void slowSideVelocity()
    {

        // Movement physics while turning
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        // Drift factor determines the floatiness of movement
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void setInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public bool isBraking(out bool isBrakingPressed)
    {
        isBrakingPressed = false;

        if (upVelocity > 0 && accelerationInput < 0)
        {
            isBrakingPressed = true;
            return true;
        }
        return false;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }

}
