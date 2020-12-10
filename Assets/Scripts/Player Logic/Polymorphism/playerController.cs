using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : Player
{
    // Start is called before the first frame update
    private void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        // Check for Mech Activation - Toggle
        if (Input.GetKeyUp(KeyCode.LeftAlt) && !wearingMech)
        {
            Debug.Log("Alt Pressed");
            putOnMech = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt) && wearingMech)
        {
            wearingMech = false;
        }
    }

    // Update is called every fixed framerate frame - Good for Physics
    private void FixedUpdate()
    {
        // Check if player is grounded
        grounded = IsGrounded();

        // Check for Jumping
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // Check for movement of character
        MoveCharacter(movement);

        // Put on the mech?
        if (putOnMech)
        {
            AttachMech();
            putOnMech = false; // reset so this doesnt occur all the time
        }
        // Behaviour whilst wearing Mech Suit
        if (wearingMech)
        {
            //RayCastTurretLook();
        }
        // Remove Mech
        if (!wearingMech)
        {
            DetachMech();
        }
    }

    private void AttachMech()
    {
        Collider mechCollider = mechSuit.GetComponent<Collider>();

        //Disable mech suit collider whilst on player
        mechCollider.enabled = !mechCollider.enabled;

        Vector3 attachedPos = player.transform.position;

        attachedPos.y -= 0.5f;

        mechSuit.transform.position = attachedPos;
        mechSuit.transform.SetParent(player);

        wearingMech = true; // Let the game know you're wearing the mech suit
    }

    private void DetachMech()
    {
        //Check if the first child of this object has children.
        if (player.transform.GetChild(0) == null)
        {

        }
        else // If it does then that means we detatch it
        {
            //Wait a while to enable mech collider again
            StartCoroutine(ReenableMechCollider());
            mechSuit.transform.SetParent(null);
        }

    }

    public IEnumerator ReenableMechCollider()
    {
        yield return new WaitForSeconds(3000); //3 sec wait
        //Reenable mech suit collider
        Collider mechCollider = mechSuit.GetComponent<Collider>();
        mechCollider.enabled = mechCollider.enabled;
    }




    ////Check for collision with item
    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Hi");
    //    Debug.Log(collision.gameObject.layer);

    //    //if (collision.collider.gameObject.layer == itemMask)
    //    //{
    //    //    putOnMech = true;
    //    //    Debug.Log("Mech time");
    //    //}

    //}

}
