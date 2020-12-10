using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, PlayerInterface
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] public Transform cam;
    [SerializeField] public Transform player, playerBody, mechSuit;
    [SerializeField] public Transform orientationOfPlayer;

    [SerializeField] public Vector2 movement;
    [SerializeField] public bool jumping = false;
    [SerializeField] public bool readyToJump = true;
    [SerializeField] public bool grounded = false;
    [SerializeField] public bool putOnMech = false;
    [SerializeField] public bool wearingMech = false;

    [SerializeField] public float groundDistance = 30f;
    [SerializeField] public float sizeOfPlayer = 15f;

    [SerializeField] public LayerMask layerMask, itemMask;
    [SerializeField] public LayerMask ground_mask;

    [SerializeField] public float speed = 35f;
    [SerializeField] public float jumpForce = 75f;
    [SerializeField] public float normalTopSpeed = 55f;
    [SerializeField] public float boostedTopSpeed = 75f;

    [SerializeField] public RaycastHit hit;

    public void RayCastTurretLook()
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
    public void MoveCharacter(Vector3 direction)
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
        rb.AddForce(orientationOfPlayer.transform.forward * movement.y * speed * Time.deltaTime * multiplier * multiplierV);

        // Left and right movement
        rb.AddForce(cam.transform.right * movement.x * speed * Time.deltaTime * multiplier);

    }

    public bool IsGrounded()
    {
        //Debug.DrawRay(playerBody.position, -Vector3.up * 50f, Color.white);
        return Physics.CheckSphere(playerBody.position, groundDistance, ground_mask);
    }

    public void Jump()
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

    // Player's attack
    public void Attack()
    {
        //todo
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
