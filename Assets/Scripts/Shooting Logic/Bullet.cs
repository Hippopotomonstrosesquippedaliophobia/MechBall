using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletPool, BulletInterface
{
    public string nameOfItem;
    public GameObject spawner;
    public BulletPool bulletPool;

    public float bulletSpeed;
    public float bulletDmg;
    public float lifetime;

    //[SerializeField] private LayerMask enemyLayer;

    public void OnCollisionEnter(Collision collision)
    {
        GameObject whatGotHit = collision.gameObject;

        if (whatGotHit.CompareTag("Enemy"))
        {
            Debug.Log(whatGotHit.name + " tag: " + whatGotHit.tag);

            ApplyDamage(whatGotHit);
        }

        Destroy(gameObject);
        //this.gameObject.SetActive(false); // repool bullet
    }

    public void ApplyDamage(GameObject whatToHurt)
    {
        Health hp = whatToHurt.GetComponentInParent<Health>();
        hp.TakeDamage(transform, bulletDmg);
    }
}