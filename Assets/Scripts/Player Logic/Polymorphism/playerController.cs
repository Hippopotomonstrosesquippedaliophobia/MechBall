using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : Player
{
    // Jump Mechanic
    float startTime;
    float releaseTime;
    float timeTaken;

    // Start is called before the first frame update
    private void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        // Check for Mech Activation - Toggle
        if (Input.GetKeyUp(KeyCode.Mouse2) && !wearingMech) 
            AttachMech();  
        else if (Input.GetKeyUp(KeyCode.Mouse2) && wearingMech) 
            DetachMech();  
    }

    // Update is called every fixed framerate frame - Good for Physics
    private void FixedUpdate()
    {
        // Check for Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            startTime = Time.time;
            //Get time now to see how high to jump
        }
        if (Input.GetKeyUp(KeyCode.Space) && grounded)
        {
            //Time it was released
            releaseTime = Time.time;
            timeTaken = releaseTime - startTime;

            //Call jump with strength parameter
            Jump(timeTaken);
        }
        // Check for movement of character
        MoveCharacter(movement);
    }

    // Functions where they should be called last for smoother feel
    private void LateUpdate()
    {
        // Check if player is grounded - LEAVE HERE IT IS SMOOTH
        grounded = IsGrounded();

        // Behaviour whilst wearing Mech Suit - LEAVE HERE IT IS SMOOTH
        if (wearingMech)
        {
            AttachedToPlayer(mechSuit);
        }
    }

    // Get any object to be attached to the player. Call this in Last update for no lag
    private void AttachedToPlayer(Transform obj)
    {
        Vector3 desiredPosition = orientationOfPlayer.position;
        desiredPosition.y -= 0.5f;
        Vector3 smoothedPosition = Vector3.SmoothDamp(obj.transform.position, desiredPosition, ref velocity, 0f);
        obj.transform.position = smoothedPosition;
    }

    private void AttachMech()
    {
        wearingMech = true; // Let the game know you're wearing the mech suit
    }

    private void DetachMech()
    {
        wearingMech = false; // Let the game know you're not wearing the mech suit
    }
}
