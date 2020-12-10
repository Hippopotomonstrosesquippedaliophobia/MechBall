using UnityEngine;

// What must every player have?
public interface EnemyInterface 
{
    // All must track player movement
    void FollowPlayer();

    // All must attack player
    void AttackPlayer();
}
