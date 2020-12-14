using UnityEngine;

// What must every Bullet have?
public interface BulletInterface
{
    void OnCollisionEnter(Collision collision);

    void ApplyDamage(GameObject whatToHurt);

}
