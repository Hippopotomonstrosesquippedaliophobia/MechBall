using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    [SerializeField] public ParticleSystem deathExplode;

    // All entities can potentially heal
    public void Heal(float heal)
    {

    }
    // All players can take damage
    public void TakeDamage(Transform whoHitYou, float dmg)
    {
        health -= dmg;

        Debug.Log(whoHitYou.name + " Killed you");

        // Check if they are dead after taking damage
        if (IsDead())
            Die();
    }

    public bool IsDead()
    {
        return (health <= 0); // Returns true if health lower or equal to zero, false if not
    }

    // All players Die
    public void Die()
    {
        // KILL Character    
        deathExplode.Play();
        Destroy(gameObject, 0.5f); // , time to set delay
    }
}
