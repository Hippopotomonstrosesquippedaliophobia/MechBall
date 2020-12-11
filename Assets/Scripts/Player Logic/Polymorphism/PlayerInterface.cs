using UnityEngine;

// What must every player have?
public interface PlayerInterface
{
    // All players must have movement
    void MoveCharacter(Vector3 direction);

    // All players must have attacks
    void Attack();

    // All players must check if they are grounded
    bool IsGrounded();

    // All players can potentially jump
    void Jump(float strength);

    void CreateDust();

 
}
