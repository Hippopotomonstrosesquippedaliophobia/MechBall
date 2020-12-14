using UnityEngine;

// What must every player have?
public interface HealthInterface
{
    // All entities can potentially heal
    void Heal(float heal);

    // All players can take damage
    void TakeDamage(Transform whoHitYou, Transform whoTakesDmg, float dmg);


    // All players Die
    bool IsDead();

    void Die(GameObject whatToDestroy);

}
