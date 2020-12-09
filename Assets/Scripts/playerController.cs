using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player, playerBody;
    [SerializeField] private Transform orientationOfPlayer;

    [SerializeField] private Vector2 movement;
    [SerializeField] private bool jumping = false;    
    [SerializeField] private bool readyToJump = true;
    [SerializeField] private bool grounded = false;

    [SerializeField] private float groundDistance = 30f;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask ground_mask;

    [SerializeField] private float speed = 35f;
    [SerializeField] private float jumpForce = 75f;
    [SerializeField] private float normalTopSpeed = 55f;
    [SerializeField] private float boostedTopSpeed = 75f;

    [SerializeField] RaycastHit hit;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }


    // Update is called once per frame
    void Update()
    {

    }

    // Update is called every fixed framerate frame - Good for Physics
    private void FixedUpdate()
    {
        grounded = IsGrounded();
        RayCastTurretLook();
        MoveCharacter(movement);
    }

    //Initializing Variables
    private void Init()
    {
        //rb = this.GetComponent<Rigidbody>();
    }

    private void RayCastTurretLook()
    {
        // Reconstruct the vector to eliminate up and down of y axis
        Vector3 reconstruct = playerBody.position;
        reconstruct.y = 0 + 3f;

        //RayCast out to check for looking direction
        Physics.Raycast(reconstruct, orientationOfPlayer.forward, out hit, Mathf.Infinity, layerMask);

        //Debugging Ray Cast
        Debug.Log(hit.point);
        Debug.DrawRay(reconstruct, cam.forward * hit.distance, Color.yellow);

        // Where to tell turret to look
        Quaternion lookhere = Quaternion.LookRotation(hit.point);

        // Execute Rotation Smoothly instead of snapping to location
        player.rotation = Quaternion.RotateTowards(player.rotation, lookhere, 100f);
    }

    //Movement of character
    private void MoveCharacter(Vector3 direction)
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Set max speed
        float maxSpeed = normalTopSpeed;

        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (movement.x > 0 && xMag > maxSpeed) movement.x = 0;
        if (movement.x < 0 && xMag < -maxSpeed) movement.x = 0;
        if (movement.y > 0 && yMag > maxSpeed) movement.y = 0;
        if (movement.y < 0 && yMag < -maxSpeed) movement.y = 0;

        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;

        // Movement in air
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        //Apply forces to move player 
        float testBackward= -0.5f;

        Debug.Log(cam.transform.forward);

        // Tests if the camera is tilted beyond a certain degree and adjusts the axis used for moving forward and back to avoid flying using arrow keys
        rb.AddForce(orientationOfPlayer.transform.forward * movement.y * speed * Time.deltaTime * multiplier * multiplierV);     

        // Left and right movement
        rb.AddForce(cam.transform.right * movement.x * speed * Time.deltaTime * multiplier);

        //Also check for jumping whilst moving
        if (Input.GetKey(KeyCode.Space) && grounded) Jump();
    }

    private bool IsGrounded()
    {
        //Debug.DrawRay(playerBody.position, -Vector3.up * 50f, Color.white);
        return Physics.CheckSphere(playerBody.position, groundDistance, ground_mask);
    }

    private void Jump()
    {
        readyToJump = false;

        //Add jump forces
        rb.AddForce(Vector2.up * jumpForce * 1.5f);

        //If jumping while falling, reset y velocity.
        Vector3 vel = rb.velocity;
        if (rb.velocity.y < 0.5f)
            rb.velocity = new Vector3(vel.x, 0, vel.z);
        else if (rb.velocity.y > 0)
            rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

        Debug.Log("JUMPED");
    }

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = player.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }
}
