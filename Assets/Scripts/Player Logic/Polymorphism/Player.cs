using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, PlayerInterface
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] public Transform cam;
    [SerializeField] public Transform player, playerBody, mechSuit;
    [SerializeField] public Transform orientationOfPlayer;
    [SerializeField] public ParticleSystem dust;

    [SerializeField] public Vector2 movement;
    [SerializeField] public Vector2 lastMovement = new Vector2(0,0);
    public Vector3 velocity = Vector3.zero;
    [SerializeField] public bool jumping = false;
    [SerializeField] public bool readyToJump = true;
    [SerializeField] public bool grounded = false;
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

    public void MechLook()
    {
        // Where to tell turret to look
        Quaternion lookhere = Quaternion.LookRotation(cam.forward);

        // Execute Rotation Smoothly instead of snapping to location
        mechSuit.rotation = Quaternion.RotateTowards(mechSuit.rotation, lookhere, 100f);
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
        rb.AddForce(cam.transform.forward * movement.y * speed * Time.deltaTime * multiplier * multiplierV);

        // Left and right movement
        rb.AddForce(cam.transform.right * movement.x * speed * Time.deltaTime * multiplier);

        if (grounded)
            if ((lastMovement.x != movement.x && lastMovement.x != 0) || (lastMovement.y != movement.y && lastMovement.y != 0))
                CreateDust();

        lastMovement = new Vector2(movement.x, movement.y);
    }

    public bool IsGrounded()
    {
        bool grounded = (Physics.Raycast(orientationOfPlayer.position,
                                         Vector3.down, 
                                         0.65f, 
                                         ground_mask)); 
       
        Debug.DrawRay(orientationOfPlayer.position,
                      Vector3.down * 0.65f,
                      Color.green);

        return grounded; //Physics.CheckSphere(playerBody.position, groundDistance, ground_mask);
    }

    public void Jump(float strength)
    {
        readyToJump = false; // RESET

        //Clamps jump strength between 0.9 multiplier and 2.7 time multiplier
        strength = Mathf.Clamp(strength, 0.9f, 2.7f);

        //Add jump forces
        rb.AddForce(Vector2.up * (strength * jumpForce) * 1.5f);

        //Power Jump
        if (strength > (2.7 - 0.9) / 2)
        {
            CreateDust(); 
        }

        Debug.Log("Strength of jump: " + strength);
    }

    // Player's attack
    public void Attack()
    {
        //todo
    }

    public void CreateDust()
    {
        dust.Play();
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
