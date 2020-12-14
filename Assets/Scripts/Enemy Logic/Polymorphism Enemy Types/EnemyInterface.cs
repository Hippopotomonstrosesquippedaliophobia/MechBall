using UnityEngine;

// What must every player have?
public interface EnemyInterface 
{
    // All must track player movement
    void FollowPlayer();

    // Check proximity to player
    void CloseToPlayer();

    // All must attack player
    void AttackPlayer();
}
