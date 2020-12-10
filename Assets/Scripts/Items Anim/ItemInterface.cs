using UnityEngine;

// What must every player have?
public interface ItemInterface
{
    // All Items must bob and rotate on the ground
    void OnFloor();

    // All Items must check if picked up
    void PickedUp();

}
